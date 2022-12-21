namespace AtmAPI.Helpers;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		MapToResponses();
		MapToDomains();
	}

	private void MapToDomains()
	{
		// Category
		CreateMap<CategoryUpsertRequest, Category>();

		// Category
		CreateMap<SlotUpsertRequest, Slot>();

		// Skill
		CreateMap<SkillUpsertRequest, Skill>();

		// Student
		CreateMap<StudentInsertRequest, Student>();
		CreateMap<StudentUpdateRequest, Student>();
		CreateMap<AgentUpdateVerifiedRequest, Student>();
		CreateMap<StudentInsertRequest, UserUpsertRequest>();
		CreateMap<StudentUpdateRequest, UserUpsertRequest>();
		CreateMap<StudentInsertRequest, AgentUpsertRequest>();
		CreateMap<AgentUpdateVerifiedRequest, AgentUpsertRequest>();
		CreateMap<Student, AgentUpsertRequest>();
		CreateMap<Student, UserUpsertRequest>().BeforeMap((src, dest) => dest.Password = "ignore");

		// Lecturer
		CreateMap<LecturerInsertRequest, Lecturer>();
		CreateMap<LecturerUpdateRequest, Lecturer>();
		CreateMap<LecturerInsertRequest, UserUpsertRequest>();
		CreateMap<LecturerUpdateRequest, UserUpsertRequest>()
			.BeforeMap((src, dest) => dest.Password = "ignore");

		// Operator
		CreateMap<OperatorInsertRequest, Operator>();
		CreateMap<OperatorUpdateRequest, Operator>();
		CreateMap<OperatorInsertRequest, UserUpsertRequest>();
		CreateMap<OperatorUpdateRequest, UserUpsertRequest>()
			.BeforeMap((src, dest) => dest.Password = "ignore");

		// Class
		CreateMap<ClassUpsertRequest, Class>()
			.AfterMap(
				(src, dest) =>
				{
					// NOTE StartDate must be UTC and is handled by front end
					if (dest.EnableAutomation != true)
						return;
					var utcNow = DateTime.UtcNow;
					if (dest.StartDate <= utcNow)
						dest.Available = true;
					if (dest.EndDate < utcNow)
						dest.Available = false;
				}
			);

		// Schedule
		CreateMap<ScheduleUpsertRequest, Schedule>();
	}

	private void MapToResponses()
	{
		// External
		CreateMap<ExternalInstitution, ExternalInstitutionResponse>();
		CreateMap<ExternalInstitutionStudent, ExternalStudentResponse>()
			.ForMember(_ => _.Student, opt => opt.MapFrom(_ => _.Student))
			.ForMember(_ => _.ExternalCode, opt => opt.MapFrom(_ => _.ExternalInstitution.Code));

		// Student
		CreateMap<Student, StudentResponse>();

		// Lecturer
		CreateMap<Lecturer, LecturerResponse>();

		// Operator
		CreateMap<Operator, OperatorResponse>();

		// Category
		CreateMap<Category, CategoryResponse>();

		// Skill
		CreateMap<Skill, SkillResponse>(); // .ForMember(dto => dto.Category, conf => conf.MapFrom(e => e.Category));

		// SkillReport
		CreateMap<SkillReport, SkillReportResponse>()
			.ForMember(dto => dto.Skill, conf => conf.MapFrom(e => e.Class.Skill))
			.ForMember(dto => dto.Status, conf => conf.MapFrom(e => e.Status.GetDescription()));

		// Class
		CreateMap<Class, ClassResponse>()
			.ForMember(
				dto => dto.Lecturers,
				conf =>
					conf.MapFrom(
						e =>
							e.ClassLecturers != null
								? e.ClassLecturers.Select(_ => _.Lecturer)
								: null
					)
			);

		// ClassLecturer
		CreateMap<ClassLecturer, ClassLecturerResponse>()
			.ForMember(dto => dto.Lecturer, conf => conf.MapFrom(e => e.Lecturer))
			.ForMember(dto => dto.Class, conf => conf.MapFrom(e => e.Class));

		// Schedule
		CreateMap<Schedule, ScheduleResponse>()
			.ForMember(dto => dto.Slot, conf => conf.MapFrom(e => e.Slot))
			.ForMember(dto => dto.Class, conf => conf.MapFrom(e => e.Class));

		// Slot
		CreateMap<Slot, SlotResponse>()
			.ForMember(dto => dto.Schedules, conf => conf.MapFrom(e => e.Schedules))
			.ForMember(dto => dto.DayOfWeek, conf => conf.MapFrom(e => e.DayOfWeek.ToString()));

		// Absence
		CreateMap<Absence, AbsenceResponse>()
			.ForMember(dto => dto.Slot, conf => conf.MapFrom(e => e.Schedule.Slot))
			.ForMember(dto => dto.Student, conf => conf.MapFrom(e => e.Student));
	}
}
