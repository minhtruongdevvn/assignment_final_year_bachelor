using AAM.Infrastructure.Models;
using Fluorite.Strainer.Services.Modules;

namespace AAM.Application;

public class ApplicationStrainerModule : StrainerModule
{
    public override void Load(IStrainerModuleBuilder builder)
    {
        MapBaseEntityProps<Agent>(builder);
        builder.AddProperty<Agent>(p => p.FamilyName).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.GivenName).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.Code).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.IQ).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.EQ).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.Sex).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.Height).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.Stamina).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.Strength).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.ReactionTime).IsFilterable().IsSortable();
        builder.AddProperty<Agent>(p => p.SelfDiscipline).IsFilterable().IsSortable();

        MapBaseEntityProps<Quest>(builder);
        builder.AddProperty<Quest>(p => p.QuestStatus).IsFilterable();
        builder.AddProperty<Quest>(p => p.Code).IsFilterable().IsSortable();
        builder.AddProperty<Quest>(p => p.Expired).IsFilterable().IsSortable();
        builder.AddProperty<Quest>(p => p.NumberOfAgent).IsFilterable().IsSortable();
        builder.AddProperty<Quest>(p => p.Context).IsFilterable();
        builder.AddProperty<Quest>(p => p.Category.Name).IsFilterable().HasDisplayName("CategoryName");
        builder.AddProperty<Quest>(p => p.Category.Id).IsFilterable().HasDisplayName("CategoryID");
    }

    static void MapBaseEntityProps<T>(IStrainerModuleBuilder builder) where T : DataEntityBase<Guid>
    {
        builder.AddProperty<T>(p => p.Id).IsFilterable().IsSortable();
        builder.AddProperty<T>(p => p.DateCreated).IsFilterable().IsSortable();
        builder.AddProperty<T>(p => p.DateModified).IsFilterable().IsSortable();
    }
}
