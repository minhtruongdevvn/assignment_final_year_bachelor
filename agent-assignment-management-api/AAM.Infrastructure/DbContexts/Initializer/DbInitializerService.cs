using AAM.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AAM.Infrastructure.DbContexts.Initializer;

public class DbInitializerService : IDbInitializerService
{
    private readonly ApplicationDbContext _applicationContext;
    private readonly DateTime seedDate = new(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc);

    public DbInitializerService(ApplicationDbContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public void Init()
    {
        _applicationContext.Database.Migrate();

        var existCats = _applicationContext.QuestCategories.ToList();
        foreach (var questCategory in GenerateQuestCategoryData())
        {
            if (existCats.Any(x => x.Name == questCategory.Name)) continue;
            _applicationContext.QuestCategories.Add(questCategory);
        }

        var existSkills = _applicationContext.Skills.ToList();
        foreach (var skill in GenerateSkillData())
        {
            if (existSkills.Any(x => x.Name == skill.Name)) continue;
            _applicationContext.Skills.Add(skill);
        }

        _applicationContext.SaveChanges();
    }

    IEnumerable<QuestCategory> GenerateQuestCategoryData()
    {
        // important: dont change the order
        return new List<QuestCategory>()
        {
            new() {
                Name = "arrest",
                DateCreated = seedDate,
                DateModified = seedDate,
                Description = "Using legal authority to catch and take someone to a place where the person may be accused of a crime"
            },

            new() {
                Name = "assassinate",
                DateCreated = seedDate,
                DateModified = seedDate,
                Description = "Executing someone silently"
            },

            new() {
                Name = "guard",
                DateCreated = seedDate,
                DateModified = seedDate,
                Description = "Protect the employing party's assets from a variety of hazards by enforcing preventative measures"
            },

            new() {
                Name = "investigate_crime",
                DateCreated = seedDate,
                DateModified = seedDate,
                Description = "Applying science that involves the study of facts that are then used to inform criminal trials"
            },

            new() {
                Name = "rescuse",
                DateCreated = seedDate,
                DateModified = seedDate,
                Description = "Saving someone or something from a dangerous, harmful, or difficult situation"
            },

            new() {
                Name = "spy",
                DateCreated = seedDate,
                DateModified = seedDate,
                Description = "Spying inquiry to examine targeting groups"
            },

            new() {
                Name = "suppress",
                DateCreated = seedDate,
                DateModified = seedDate,
                Description = "Using legal authority to prevent something"
            },

            new() {
                Name = "trace",
                DateCreated = seedDate,
                DateModified = seedDate,
                Description = "Tracking someone to confirm assumptions"
            },

        };
    }

    IEnumerable<Skill> GenerateSkillData()
    {
        var skillNames = new List<string>
        {
            "pistol", "rifle", "sniper_rifle", "taekwondo", "karatedo", "melee_combat",
            "running", "swimming", "driving", "piloting", "thinking_out_of_the_box",
            "critical_thinking", "problem_solving", "planning", "calming", "leadership",
            "investigate", "judgment", "details_inspecting", "tracking", "tracing", "first_aid",
            "visibility", "adaptability", "stealing", "collaboration", "communication",
            "interviewing", "interrogation", "convince", "networking", "security", "computing"
        };

        return skillNames.Select(name => new Skill
        {
            Name = name,
            DateCreated = seedDate,
            DateModified = seedDate,
        });
    }
}
