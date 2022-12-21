using Hangfire;

namespace AtmAPI.Controllers.Managements;

public class StudentController : ManagementControllerBase<Student>
{
	private readonly IdentityService _identityService;
	private readonly AssignmentService _assignmentService;

	public StudentController(
		AssignmentService assignmentService,
		IdentityService identityService,
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(sieveProc, context, mapper, uow)
	{
		_identityService = identityService;
		_assignmentService = assignmentService;
	}

	/// <summary>
	/// 	Get students
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] SieveModel request) =>
		await HandleGetAsync<Student, StudentResponse>(request);

	/// <summary>
	/// 	Get unassigned students by class id
	/// </summary>
	[HttpGet("classes/unassigned/{classId}")]
	public async Task<IActionResult> GetUnassignedStudent(
		[FromQuery] SieveModel request,
		int classId
	) =>
		await HandleGetAsync<Student, StudentResponse>(
			request,
			includeProperties: "SkillReports",
			predicate: _ =>
				_.SkillReports == null
				|| _.SkillReports.Count == 0
				|| !_.SkillReports.Any(cl => cl.ClassId == classId)
		);

	/// <summary>
	/// 	Get assigned students by class id
	/// </summary>
	[HttpGet("classes/assigned/{classId}")]
	public async Task<IActionResult> GetAssignedStudent(
		[FromQuery] SieveModel request,
		int classId
	) =>
		await HandleGetAsync<Student, StudentResponse>(
			request,
			includeProperties: "SkillReports",
			predicate: _ =>
				_.SkillReports != null
				&& _.SkillReports.Count != 0
				&& _.SkillReports.Any(cl => cl.ClassId == classId)
		);

	/// <summary>
	/// 	Get a student by id
	/// </summary>
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var student = await Repo.GetByIdAsync(id);
		student.ThrowIfNullHttpStatus();
		return Ok(Mapper.Map<StudentResponse>(student));
	}

	/// <summary>
	/// 	Update a student
	/// </summary>
	[HttpPut("{id}")]
	public async Task<IActionResult> Put([FromBody] StudentUpdateRequest request, int id)
	{
		await UpdateStudentAsync(request, id);
		return Ok();
	}

	/// <summary>
	/// 	Update agent's AAM info
	/// </summary>
	[HttpPut("{id}/verified")]
	[RoleAuthorization(ModelConstants.Lecturer)]
	public async Task<IActionResult> VerifyStudentAAMInfo(
		[FromBody] AgentUpdateVerifiedRequest request,
		int id
	)
	{
		var studentToUpdate = await Repo.GetByIdAsync(id);
		studentToUpdate.ThrowIfNullHttpStatus();
		Mapper.Map(request, studentToUpdate);
		await Repo.UpdateAsync(studentToUpdate);

		// sync to assignment system
		var agent = Mapper.Map<AgentUpsertRequest>(studentToUpdate);
		BackgroundJob.Enqueue(() => _assignmentService.UpdateAgentAsync(agent));

		return Ok();
	}

	/// <summary>
	/// 	Add a student
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] StudentInsertRequest request) =>
		Ok(await AddStudentAsync(request));

	/// <summary>
	/// 	Delete a student
	/// </summary>
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var entityToDelete = await Repo.GetByIdAsync(id);
		entityToDelete.ThrowIfNullHttpStatus(status: HttpStatusCode.OK);

		if (string.IsNullOrEmpty(entityToDelete.IdentityReference))
		{
			await Repo.DeleteAsync(entityToDelete);
			return Ok();
		}

		using (var trans = await Repo.BeginTransactionAsync())
		{
			var syncTask = await Repo.DeleteAsync(entityToDelete)
				.ContinueWith(t =>
				{
					return t.Exception != null
						? throw t.Exception
						: _identityService.DeleteUserAsync(
							int.Parse(entityToDelete.IdentityReference)
						);
				});
			//todo: test
			try
			{
				await syncTask;
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				throw new Exception(
					$"Delete user ID:{entityToDelete.IdentityReference} failed, trying to revert on ATM with student ID:{id}",
					innerException: ex
				);
			}

			await trans.CommitAsync();
		}

		// sync to assignment system
		BackgroundJob.Enqueue(
			() => _assignmentService.DeleteAgentAsync(int.Parse(entityToDelete.IdentityReference!))
		);

		return Ok();
	}

	/// <summary>
	/// 	Get attendances by student id
	/// </summary>
	[HttpGet("{id}/attendances")]
	public async Task<IActionResult> GetAttendances(
		[FromQuery] SieveModel sieveRequest,
		[FromQuery] RangeRequest rangeRequest,
		int id
	)
	{
		var studentAttendances = await Repo.GetAttendancesByStudentAsync(id, rangeRequest, Mapper);
		var sieveAttendances = SieveProc.Apply(sieveRequest, studentAttendances.AsQueryable());

		return Ok(
			ResponseSieve.With(
				totalCount: studentAttendances.Count(),
				list: sieveAttendances,
				model: sieveRequest
			)
		);
	}

	/// <summary>
	/// 	Get an attendance by attend date and schedule id
	/// </summary>
	[HttpGet("{id:int}/attendances/{attendDate}/{scheduleId:int}")]
	public async Task<IActionResult> GetAttendanceByDate(
		int id,
		DateTime attendDate,
		int scheduleId
	)
	{
		var attendance = await Repo.GetAttendanceByKeysAsync(id, scheduleId, attendDate, Mapper);
		return Ok(attendance);
	}

	/// <summary>
	/// 	Get skill reports by student id
	/// </summary>
	[HttpGet("{id}/reports")]
	public async Task<IActionResult> GetSkillReportsByStudent(
		[FromQuery] SieveModel request,
		int id
	)
	{
		request.Filters += $",student_id=={id}";
		return await HandleGetAsync<SkillReport, SkillReportResponse>(request, "Class.Skill");
	}

	/// <summary>
	/// 	Update student's skill report
	/// </summary>
	[HttpPut("{id}/reports")]
	[RoleAuthorization(ModelConstants.Lecturer)]
	public async Task<IActionResult> VerifyStudentSkillReport(
		[FromBody] SkillReportUpdateVerifiedRequest request,
		int id
	)
	{
		await UpdateSkillReportAsync(request, id);
		return Ok();
	}

	#region EXTERNAL INSTITUTION

	/// <summary>
	/// 	Add a student by external
	/// </summary>
	[AllowAnonymous]
	[HttpPost("externals")]
	public async Task<IActionResult> AddStudentByExternal(
		[FromBody] ExternalStudentInsertRequest request
	)
	{
		var verifiedExternal = await VerifyExternalAsync(request);

		// check if the student exist
		var existingStudent = Repo.EntitySet.FirstOrDefaultAsync(
			_ => _.IdentifyNumber == request.Student.IdentifyNumber
		);
		if (existingStudent != null)
		{
			await AddStudentExternalAsync(existingStudent.Id, verifiedExternal.Id);
			return Ok();
		}

		var studentResult = await AddStudentAsync(request.Student);
		await AddStudentExternalAsync(studentResult.Id, verifiedExternal.Id);

		var skillIds = verifiedExternal.SkillIds.Split(';').Select(_ => int.Parse(_));
		var externalClasses = await Uow.Class.EntitySet
			.Where(_ => _.Name == verifiedExternal.Name && skillIds.Contains(_.SkillId))
			.ToListAsync();

		// enrolled
		await Uow.SkillReport.InsertRangeAsync(
			externalClasses.Select(
				externalClass =>
					new SkillReport { ClassId = externalClass.Id, StudentId = studentResult.Id, }
			)
		);

		return Ok(studentResult);
	}

	/// <summary>
	/// 	Update a student by external
	/// </summary>
	[AllowAnonymous]
	[HttpPut("{id}/externals")]
	public async Task<IActionResult> Put([FromBody] ExternalStudentUpdateRequest request, int id)
	{
		var verifiedExternal = await VerifyExternalAsync(request);

		// Check if external can edit the student
		var externalStudent = await Uow.ExternalInstitutionStudent.EntitySet.FirstOrDefaultAsync(
			_ => _.StudentId == id && _.ExternalInstitutionId == verifiedExternal.Id
		);
		externalStudent.ThrowIfNullHttpStatus(
			"External does not have permission to update this student"
		);

		await UpdateStudentAsync(request.Student, id);
		return Ok();
	}

	/// <summary>
	/// 	Get students by external
	/// </summary>
	[AllowAnonymous]
	[HttpGet("externals")]
	public async Task<IActionResult> GetStudentsByExternal([FromBody] ExternalSieveRequest request)
	{
		var verifiedExternal = await VerifyExternalAsync(request);

		var result = await HandleGetAsync<ExternalInstitutionStudent, ExternalStudentResponse>(
			request.Sieve,
			includeProperties: "Student,ExternalInstitution",
			predicate: _ => _.ExternalInstitutionId == verifiedExternal.Id
		);

		return Ok(result);
	}

	/// <summary>
	/// 	Update student's skill report by external
	/// </summary>
	[AllowAnonymous]
	[HttpPut("{id}/reports/externals")]
	public async Task<IActionResult> VerifyStudentSkillReportByExternal(
		[FromBody] ExternalSkillReportUpdateVerifiedRequest request,
		int id
	)
	{
		var verifiedExternal = await VerifyExternalAsync(request);
		await UpdateSkillReportAsync(request.SkillReport, id, verifiedExternal.Name);
		return Ok();
	}

	/// <summary>
	/// 	Update agent's AAM info
	/// </summary>
	[AllowAnonymous]
	[HttpPut("{id}/verified/externals")]
	public async Task<IActionResult> VerifyStudentAAMInfoByExternal(
		[FromBody] ExternalAgentUpdateVerifiedRequest request,
		int id
	)
	{
		var verifiedExternal = await VerifyExternalAsync(request);

		// Check if external can edit the student
		var externalStudent = await Uow.ExternalInstitutionStudent.EntitySet.FirstOrDefaultAsync(
			_ => _.StudentId == id && _.ExternalInstitutionId == verifiedExternal.Id
		);
		externalStudent.ThrowIfNullHttpStatus(
			"External does not have permission to update this student"
		);

		var studentToUpdate = await Repo.GetByIdAsync(id);
		studentToUpdate.ThrowIfNullHttpStatus();
		Mapper.Map(request.Agent, studentToUpdate);
		await Repo.UpdateAsync(studentToUpdate);

		// sync to assignment system
		var agent = Mapper.Map<AgentUpsertRequest>(studentToUpdate);
		BackgroundJob.Enqueue(() => _assignmentService.UpdateAgentAsync(agent));

		return Ok();
	}

	#endregion

	#region Extension
	private async Task<StudentResponse> AddStudentAsync(StudentInsertRequest request)
	{
		var identityResponse = await _identityService.CreateUserAsync(
			Mapper.Map<UserUpsertRequest>(request),
			ModelConstants.Student
		);

		try
		{
			var student = Mapper.Map<Student>(request);
			student.IdentityReference = ((int)identityResponse.Content!.id).ToString();
			var createdStudent = await Repo.InsertAsync(student);

			// sync to assignment system
			var agent = Mapper.Map<AgentUpsertRequest>(createdStudent);
			BackgroundJob.Enqueue(() => _assignmentService.CreateAgentAsync(agent));

			return Mapper.Map<StudentResponse>(createdStudent);
		}
		catch (Exception ex)
		{
			await _identityService.DeleteUserAsync((int)identityResponse.Content!.id);
			throw new Exception(
				$"Create student failed, trying to revert on IDS with user ID:{identityResponse.Content!.id}",
				innerException: ex
			);
		}
	}

	private async Task UpdateStudentAsync(StudentUpdateRequest request, int id)
	{
		var studentToUpdate = await Repo.GetByIdAsync(id);
		studentToUpdate.ThrowIfNullHttpStatus();

		var revertTmp = new StudentUpdateRequest { Email = studentToUpdate.Email };

		Mapper.Map(request, studentToUpdate);

		if (string.IsNullOrEmpty(studentToUpdate.IdentityReference))
		{
			await Repo.UpdateAsync(studentToUpdate);
			return;
		}

		var identityResponse = await _identityService.UpdateUserAsync(
			int.Parse(studentToUpdate.IdentityReference!),
			Mapper.Map<UserUpsertRequest>(request)
		);

		try
		{
			await Repo.UpdateAsync(studentToUpdate);

			// sync to assignment system
			var agent = Mapper.Map<AgentUpsertRequest>(studentToUpdate);
			BackgroundJob.Enqueue(() => _assignmentService.UpdateAgentAsync(agent));
		}
		catch (Exception ex)
		{
			await _identityService.UpdateUserAsync(
				int.Parse(studentToUpdate.IdentityReference),
				Mapper.Map<UserUpsertRequest>(revertTmp)
			);

			throw new Exception(
				$"Update student with ID:{id} failed, trying to revert on IDS with user ID:{studentToUpdate.IdentityReference}",
				innerException: ex
			);
		}
	}

	private async Task UpdateSkillReportAsync(
		SkillReportUpdateVerifiedRequest request,
		int studentId,
		string externalName = ""
	)
	{
		// fields to update must be verified
		var report = await Uow.SkillReport
			.GetQueryable(
				report => report.ClassId == request.ClassId && report.StudentId == studentId,
				includeProperties: "Student,Class.Skill"
			)
			.SingleOrDefaultAsync();
		report.ThrowIfNullHttpStatus();

		// check if external can edit the report
		if (!string.IsNullOrWhiteSpace(externalName))
			externalName
				.ThrowHttpStatus("Permission denied")
				.IfFalse(name => name == report.Class.Name);

		report.Score = request.Score;
		await Uow.SkillReport.UpdateAsync(report);

		// sync to assignment system
		BackgroundJob.Enqueue(
			() =>
				_assignmentService.UpsertAgentSkillAsync(
					new()
					{
						IdentityReference = report.Student.IdentityReference,
						SkillName = report.Class.Skill.Name,
						Score = report.Score
					}
				)
		);
	}

	private async Task AddStudentExternalAsync(int studentId, int externalId)
	{
		var existingExternalStudent =
			await Uow.ExternalInstitutionStudent.EntitySet.FirstOrDefaultAsync(
				_ => _.ExternalInstitutionId == externalId && _.StudentId == studentId
			);
		if (existingExternalStudent != null)
			return;

		await Uow.ExternalInstitutionStudent.InsertAsync(
			new ExternalInstitutionStudent
			{
				StudentId = studentId,
				ExternalInstitutionId = externalId
			}
		);
	}

	private async Task<ExternalInstitution> VerifyExternalAsync(ExternalRequest request)
	{
		var external = await Uow.ExternalInstitution.EntitySet.FirstOrDefaultAsync(
			_ => _.Code == request.Code
		);
		external.ThrowIfNullHttpStatus($"Cannot find external with code: {request.Code}");
		external.PassCode
			.ThrowHttpStatus("Invalid pass code")
			.IfFalse(passCode => passCode == request.PassCode);

		return external;
	}
	#endregion
}
