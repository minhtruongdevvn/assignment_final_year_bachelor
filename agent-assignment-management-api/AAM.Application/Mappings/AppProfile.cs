using AAM.Infrastructure.Models;

using AutoMapper;

namespace AAM.Application;

internal class AppProfile : Profile
{
    public AppProfile()
    {
        CreateMap<Agent, AgentDTO>().ReverseMap();
        CreateMap<QuestDTO, Quest>();
        CreateMap<Quest, QuestDTO>()
            .ForMember(
                m => m.CurrentNumberOfAgent, 
                cfg => cfg.MapFrom(x => x.AgentQuests == null?0:x.AgentQuests.Count)
            );
        CreateMap<QuestCategory, QuestCategoryDTO>().ReverseMap();
        CreateMap<QuestTransaction, QuestTransactionDTO>().ReverseMap();
        CreateMap<AgentSkill, AgentSkillDTO>()
            .ForMember(m => m.IdentityReference, cfg => cfg.Ignore())
            .ForMember(m => m.SkillName, cfg => cfg.MapFrom(x => x.Skill.Name));
        CreateMap<AgentSkillDTO, AgentSkill>();
        CreateMap<Skill, SkillDTO>().ReverseMap();
    }
}

