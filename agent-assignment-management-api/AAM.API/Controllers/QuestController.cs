using AAM.AgentSuggestion.Entities;
using AAM.AgentSuggestion.Interfaces;
using AAM.API.Authorization;
using AAM.Application;
using AAM.Infrastructure.Enumerations;
using AAM.Infrastructure.Models;
using Fluorite.Strainer.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AAM.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]s")]
public class QuestController : ControllerBase
{
    private readonly IQuestService _questService;
    private readonly IAgentQuestService _agentQuestService;
    private readonly IAgentService _agentService;
    private readonly IPredictor _predictor;
    IHubContext<HubService> _hubcontext;
    public QuestController(
        IQuestService questService, 
        IPredictor predictor, 
        IAgentService agentService,
        IAgentQuestService agentQuestService,
        IHubContext<HubService> hubcontext
    )
    {
        _agentService = agentService;
        _questService = questService;
        _predictor = predictor;
        _agentQuestService = agentQuestService;
        _hubcontext = hubcontext;
    }

    [AppAuthorization()]
    [HttpPost()]
    public async Task<IActionResult> Create(QuestDTO dto)
    {
        var quest = await _questService.AddAsync(dto);
        await _questService.SaveChangeAsync();
        return Ok(quest);
    }

    [AppAuthorization()]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, QuestDTO dto)
    {
        await LockIfQuestIsCompletedAsync(id);

        var questCode = await _questService.UpdateAsync(id, dto);
        await _questService.SaveChangeAsync();

        await NotifyRelatedAgentAsync(id, HubAction.Update, questCode);
        return Ok();
    }

    [AppAuthorization()]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _questService.GetByIdAsync(id));
    }

    [AppAuthorization()]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        // todo: handle on UI
        var locked = await _questService.GetQuestLockStatusAsync(id, true);
        if (locked)
            throw new ClientException("Quest is locked", ErrorType.LockedQuest);

        await _questService.DeleteAsync(id);
        await _questService.SaveChangeAsync();

        await NotifyRelatedAgentAsync(id, HubAction.Delete);
        return Ok();
    }

    [AppAuthorization()]
    [HttpGet("categories")]
    public async Task<IActionResult> GetCatrgories()
    {
        return Ok(await _questService.GetCategoriesAsync());
    }

    [AppAuthorization()]
    [HttpGet()]
    public async Task<IActionResult> GetPaging([FromQuery] StrainerModel model)
    {
        return Ok(await _questService.GetPageResultAsync(model));
    }

    [AppAuthorization()]
    [HttpGet("{id}/agents/suggested")]
    public async Task<IActionResult> GetSuggestedAgents(Guid id)
    {
        await LockIfQuestIsCompletedAsync(id);

        var predictResult = (await _predictor.GetAgentPredictResultAsync(id.ToString(), false))
            .OrderByDescending(x => x.Probability)
            .Take(15)
            .ToList();

        var agents = await _agentService.GetByIdsAsync(predictResult.Select(x => (Guid)x.AgentId!));
        object result = predictResult.Select((result) => new
        {
            agent = agents.FirstOrDefault(x => x.Id == result.AgentId),
            predict = result
        });

        return Ok(result);
    }

    [AppAuthorization()]
    [HttpGet("code/{questCode}/agents/{agentId}/suggested")]
    public async Task<IActionResult> GetSuggestedAgentWithCode(string questCode, Guid agentId)
    {
        var quest = await _questService.GetByCodeAsync(questCode);
        LockIfQuestIsCompleted(quest);

        if(quest!.AgentQuests != null && quest.AgentQuests.Any(x => x.AgentId == agentId))
        {
            throw new ClientException("Agent is assigned to this quest", ErrorType.Assigned);
        }

        var predictResult = 
            await _predictor.GetPredictResultByAgentIdAsync(quest.Id.ToString(),agentId.ToString());

        return Ok(predictResult);
    }

    [AppAuthorization()]
    [HttpPost("{id}/agents/{agentId}")]
    public async Task<IActionResult> AddAgentToQuest(Guid id, Guid agentId, PredictResult predictResult)
    {
        await LockIfQuestIsCompletedAsync(id);

        var questCode = await _agentQuestService.AddAsync(id, agentId, predictResult);
        await _agentQuestService.SaveChangeAsync();

        await NotifyRelatedAgentAsync(id, HubAction.Add, questCode);
        return Ok();
    }

    [AppAuthorization()]
    [HttpPost("code/{questCode}/agents/{agentId}")]
    public async Task<IActionResult> AddAgentToQuestWithCode(string questCode, Guid agentId, PredictResult predictResult)
    {
        var quest = await _questService.GetByCodeAsync(questCode);
        LockIfQuestIsCompleted(quest);

        await _agentQuestService.AddAsync(quest!.Id, agentId, predictResult);
        await _agentQuestService.SaveChangeAsync();

        await NotifyRelatedAgentAsync(quest!.Id, HubAction.Add, questCode);
        return Ok();
    }

    [AppAuthorization()]
    [HttpDelete("{id}/agents/{agentId}")]
    public async Task<IActionResult> DeleteAgentFromQuest(Guid id, Guid agentId)
    {
        await LockIfQuestIsCompletedAsync(id);

        await _agentQuestService.DeleteAsync(id, agentId);
        await _agentQuestService.SaveChangeAsync();

        await NotifyRelatedAgentAsync(id, HubAction.Delete);
        return Ok();
    }

    [AppAuthorization()]
    [HttpGet("{id}/agents")]
    public async Task<IActionResult> GetAgentFromQuest(Guid id)
    {
        return Ok(await _agentQuestService.GetPredictAgentInQuestAsync(id));
    }

    [AgentAppAuthorization()]
    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateQuestStatus(Guid id, int status)
    {
        await _questService.UpdateStatusAsync(id, (QuestStatus)status);
        await _questService.SaveChangeAsync();
        return Ok();
    }

    private async Task LockIfQuestIsCompletedAsync(Guid id) {
        var locked = await _questService.GetQuestLockStatusAsync(id);
        if (locked)
            throw new ClientException("Quest is complete", ErrorType.LockedQuest);
    }

    private void LockIfQuestIsCompleted(Quest? quest)
    {
        var locked = _questService.GetQuestLockStatus(quest);
        if (locked)
            throw new ClientException("Quest is complete", ErrorType.LockedQuest);
    }

    private async Task NotifyRelatedAgentAsync(Guid questId, string hubAction = HubAction.Update, object? arg1 = null) {
        var agents = await _agentQuestService.GetAgentInQuestAsync(questId);

        // send message to agents
        await _hubcontext.Clients
            .Groups(agents.Select(a => a.IdentityReference!))
            .SendAsync(hubAction, arg1);
    }
}
