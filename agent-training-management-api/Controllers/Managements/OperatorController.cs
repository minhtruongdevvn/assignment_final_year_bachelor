namespace AtmAPI.Controllers.Managements;

public class OperatorController : ManagementControllerBase<Operator>
{
	private readonly IdentityService _identityService;

	public OperatorController(
		IdentityService identityService,
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(sieveProc, context, mapper, uow) => _identityService = identityService;

	/// <summary>
	/// 	Get operators
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] SieveModel request) =>
		await HandleGetAsync<Operator, OperatorResponse>(request);

	/// <summary>
	/// 	Get an operator by id
	/// </summary>
	[HttpGet("{id:int}")]
	public async Task<IActionResult> Get(int id)
	{
		var @operator = await Repo.GetByIdAsync(id);
		@operator.ThrowIfNullHttpStatus();
		return Ok(Mapper.Map<OperatorResponse>(@operator));
	}

	/// <summary>
	/// 	Update an operator
	/// </summary>
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Put(int id, [FromBody] OperatorUpdateRequest request)
	{
		var operatorToUpdate = await Repo.GetByIdAsync(id);
		operatorToUpdate.ThrowIfNullHttpStatus();

		var restoreEntity = new OperatorUpdateRequest { Email = operatorToUpdate.Email, };

		Mapper.Map(request, operatorToUpdate);

		if (string.IsNullOrEmpty(operatorToUpdate.IdentityReference))
		{
			await Repo.UpdateAsync(operatorToUpdate);
			return Ok();
		}

		var identityResponse = await _identityService.UpdateUserAsync(
			int.Parse(operatorToUpdate.IdentityReference),
			Mapper.Map<UserUpsertRequest>(request)
		);

		try
		{
			await Repo.UpdateAsync(operatorToUpdate);
			return Ok();
		}
		catch (Exception ex)
		{
			await _identityService.UpdateUserAsync(
				int.Parse(operatorToUpdate.IdentityReference),
				Mapper.Map<UserUpsertRequest>(restoreEntity)
			);

			throw new Exception(
				$"Update sysOperator with ID:{id} failed, trying to revert on IDS with user ID:{operatorToUpdate.IdentityReference}",
				innerException: ex
			);
		}
	}

	/// <summary>
	/// 	Add an operator
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] OperatorInsertRequest request)
	{
		var identityResponse = await _identityService.CreateUserAsync(
			Mapper.Map<UserUpsertRequest>(request),
			ModelConstants.Operator
		);

		try
		{
			var @operator = Mapper.Map<Operator>(request);
			@operator.IdentityReference = ((int)identityResponse.Content?.id).ToString();
			var createdOperator = await Repo.InsertAsync(@operator);
			return Ok(Mapper.Map<OperatorResponse>(createdOperator));
		}
		catch (Exception ex)
		{
			await _identityService.DeleteUserAsync((int)identityResponse.Content?.id);
			throw new Exception(
				$"Create sysOperator failed, trying to revert on IDS with user ID:{identityResponse.Content?.id}",
				innerException: ex
			);
		}
	}

	/// <summary>
	/// 	Delete an operator
	/// </summary>
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var operatorToDelete = await Repo.GetByIdAsync(id);
		operatorToDelete.ThrowIfNullHttpStatus();

		if (string.IsNullOrEmpty(operatorToDelete.IdentityReference))
		{
			await Repo.DeleteAsync(operatorToDelete);
			return Ok();
		}

		await using (var trans = await Repo.BeginTransactionAsync())
		{
			var syncTask = await Repo.DeleteAsync(operatorToDelete)
				.ContinueWith(t =>
				{
					return t.Exception != null
						? throw t.Exception
						: _identityService.DeleteUserAsync(
							int.Parse(operatorToDelete.IdentityReference)
						);
				});

			var identityResponse = await syncTask;

			//todo: test
			try
			{
				await syncTask;
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				throw new Exception(
					$"Delete user ID:{operatorToDelete.IdentityReference} failed, trying to revert on ATM with operator ID:{id}",
					innerException: ex
				);
			}
			//todo: test
			try
			{
				await syncTask;
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				throw new Exception(
					$"Delete user ID:{operatorToDelete.IdentityReference} failed, trying to revert on ATM with operator ID:{id}",
					innerException: ex
				);
			}

			await trans.CommitAsync();
		}

		return Ok();
	}
}
