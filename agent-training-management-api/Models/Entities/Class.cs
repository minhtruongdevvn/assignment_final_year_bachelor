namespace AtmAPI.Models.Entities;

public class Class : EntityBase
{
	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string Name { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.DescriptionLength)]
	public string? Description { get; set; }

	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string Placement { get; set; } = default!;
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }

	// [true] opening | [false] closed | [null] future
	public bool? Available { get; set; }
	public bool EnableAutomation { get; set; }
	public bool IsExternal { get; set; } = false;
	public int? MaxLearner { get; set; }
	public int SkillId { get; set; }

	public virtual Skill Skill { get; set; } = default!;
	public virtual ICollection<Schedule>? Schedules { get; set; }
	public virtual ICollection<SkillReport>? SkillReports { get; set; }
	public virtual ICollection<ClassLecturer>? ClassLecturers { get; set; }
}
