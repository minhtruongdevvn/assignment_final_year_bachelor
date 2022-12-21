namespace AAM.Application;
public interface IOperatorService
{
    Task<IdentityClientResponse> AddAsync(OperatorDTO operatorDTO);
}

