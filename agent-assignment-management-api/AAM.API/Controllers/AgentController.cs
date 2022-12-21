using AAM.API.Authorization;
using AAM.Application;
using Fluorite.Strainer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AAM.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]s")]
public class AgentController : ControllerBase
{
    private readonly IAgentQuestService _agentQuestService;
    private readonly IAgentService _agentService;
    private readonly IAgentSkillService _agentSkillService;
    public AgentController(
        IAgentService agentService, 
        IAgentSkillService agentSkillService,
        IAgentQuestService agentQuestService
    )
    {
        _agentService = agentService;
        _agentSkillService = agentSkillService;
        _agentQuestService = agentQuestService;
    }

    [IdentityAuthorization()]
    [HttpPost()]
    public async Task<IActionResult> Create(AgentDTO dto)
    {
        await _agentService.AddAsync(dto);
        await _agentService.SaveChangeAsync();
        return Ok();
    }

    [IdentityAuthorization()]
    [HttpPost("skills")]
    public async Task<IActionResult> UpsertAgentSkill(AgentSkillDTO dto)
    {
        await _agentSkillService.UpsertAsync(dto);
        await _agentSkillService.SaveChangeAsync();
        return Ok();
    }

    [IdentityAuthorization()]
    [HttpPut("list")]
    public async Task<IActionResult> AddRange(IEnumerable<AgentDTO> dtos)
    {
        await _agentService.AddRangeAsync(dtos);
        await _agentService.SaveChangeAsync();
        return Ok();
    }

    [IdentityAuthorization()]
    [HttpPut()]
    public async Task<IActionResult> Update(AgentDTO dto)
    {
        await _agentService.UpdateAsync(dto);
        await _agentService.SaveChangeAsync();
        return Ok();
    }

    [IdentityAuthorization()]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _agentService.DeleteAsync(id);
        await _agentService.SaveChangeAsync();
        return Ok();
    }

    [AppAuthorization()]
    [HttpGet()]
    public async Task<IActionResult> GetPaging([FromQuery] StrainerModel model)
    {
        return Ok(await _agentService.GetPageResultAsync(model));
    }

    [AppAuthorization()]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var agent = await _agentService.GetByIdAsync(id);
        if(agent == null)
        {
            throw new ClientException(
                "Agent not found",
                ErrorType.EntityNotFound
            );
        }

        return Ok(agent.ToDynamic(new[] { new ObjectField("isBusy", await _agentService.BusyAsync(id)) }));
    }

    [AgentAppAuthorization()]
    [HttpGet("{refId}/quests/{filter?}")]
    public async Task<IActionResult> GetQuestsOfAgent(string refId, string? filter = null)
    {
        string? code = null;
        int? status = null;

        if (!string.IsNullOrEmpty(filter)) {
            var codeAndStatusList = filter.Split(',');
            code = codeAndStatusList.FirstOrDefault(x => x.Contains("code"))?.Split('=')[1];
            var statusString = codeAndStatusList.FirstOrDefault(x => x.Contains("status"))?.Split('=')[1];
            if (int.TryParse(statusString, out var parsedStatus))
                status = parsedStatus;         
        }

        return Ok(await _agentQuestService.GetQuestsOfAgentAsync(refId, code, status));
    }
    
}
