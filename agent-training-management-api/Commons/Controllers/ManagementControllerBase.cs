namespace AtmAPI.Commons.Controllers;

[Authorize]
[RoutePrefix("management")]
public abstract class ManagementControllerBase<TEntity> : ApiControllerBase where TEntity : class
{
	internal readonly GenericRepository<TEntity> Repo;

	protected ManagementControllerBase(
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(context, uow, mapper, sieveProc) =>
		Repo = new GenericRepository<TEntity>(context, sieveProc);
}
