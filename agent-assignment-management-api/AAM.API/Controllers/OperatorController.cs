using AAM.API.Authorization;
using AAM.Application;
using Microsoft.AspNetCore.Mvc;
namespace AAM.API.Controllers;

[ApiController]
[AppAuthorization()]
[Route("[controller]s")]
public class OperatorController : ControllerBase
{
    private readonly IOperatorService _operatorService;
    public OperatorController(IOperatorService operatorService)
    {
        _operatorService = operatorService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OperatorDTO request)
    {
        var identityResponse = await _operatorService.AddAsync(request);
        if ((int)identityResponse.StatusCode <= 300) 
            throw new Exception("Fail to request Identity Server");
        
        
        return Ok(identityResponse);
    }
}
