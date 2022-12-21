using AAM.AgentSuggestion.Interfaces;
using AAM.API.Authorization;
using AAM.Application;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace AAM.API.Controllers;

[ApiController]
[IdentityAuthorization()]
[Route("[controller]s")]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;
    private readonly ITrainer _trainer;
    public SkillController(ISkillService skillService, ITrainer trainer)
    {
        _trainer = trainer;
        _skillService = skillService;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(SkillDTO dto)
    {
        await _skillService.AddAsync(dto);
        await _skillService.SaveChangeAsync();
        BackgroundJob.Enqueue(() => _trainer.TrainAsync(false));
        return Ok();
    }

    [HttpPut()]
    public async Task<IActionResult> Update(SkillDTO dto)
    {
        await _skillService.UpdateAsync(dto);
        await _skillService.SaveChangeAsync();
        return Ok();
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        await _skillService.DeleteAsync(name);
        await _skillService.SaveChangeAsync();
        BackgroundJob.Enqueue(() => _trainer.TrainAsync(false));
        return Ok();
    }
}
