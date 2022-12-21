namespace AAM.Application;
public interface ISkillService
{
    Task AddAsync(SkillDTO skill);
    Task UpdateAsync(SkillDTO skill);
    Task DeleteAsync(string skillName);
    Task SaveChangeAsync();

}

