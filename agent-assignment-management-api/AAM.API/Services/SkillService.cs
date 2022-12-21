using AAM.Application;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAM.API;

public class SkillService : ISkillService
{
    readonly ISkillRepository _skillRepository;
    readonly IMapper _mapper;
    public SkillService(ISkillRepository skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository;
        _mapper = mapper;
    }

    public async Task AddAsync(SkillDTO skill)
    {
        await _skillRepository.AddAsync(_mapper.Map<Skill>(skill));
    }

    public async Task UpdateAsync(SkillDTO skill)
    {
        var updateSkill = await _skillRepository.GetEntities(false).FirstOrDefaultAsync(x => x.Name == skill.OldName);
        if (updateSkill == null)
            throw new ClientException("Skill not found", skill.Id!, ErrorType.EntityNotFound);
        skill.Id = updateSkill.Id;
        _skillRepository.Update(_mapper.Map(skill, updateSkill));
    }

    public async Task DeleteAsync(string skillName)
    {
        var deleteSkill = await _skillRepository.GetEntities(false).FirstOrDefaultAsync(x => x.Name == skillName);
        if (deleteSkill == null) return;
        _skillRepository.Delete(deleteSkill);
    }

    public Task SaveChangeAsync()
    {
        return _skillRepository.UnitOfWork.SaveChangesAsync();
    }
}

