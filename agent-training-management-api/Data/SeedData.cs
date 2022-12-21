namespace AtmAPI.Data;

public static class SeedData
{
	private static readonly DateTime seedDay = new DateTime(
		2022,
		10,
		3,
		10,
		25,
		10,
		961,
		DateTimeKind.Utc
	);

	public static void Run(ModelBuilder builder)
	{
		SeedSlots(builder);
		SeedSkills(builder);
	}

	private static void SeedSlots(ModelBuilder builder)
	{
		var id = 0;
		var slots = new List<Slot>();
		const int timeStarted = 8;

		for (var day = 1; day <= 6; day++)
			for (var i = 0; i < 4; i++)
			{
				id += 1;
				var it = timeStarted + (i * 2);
				var t = i >= 2 ? it + 1 : it;
				slots.Add(
					new()
					{
						Id = id,
						DayOfWeek = (DayOfWeek)day,
						StartAt = new TimeOnly(t, 0).ToTimeSpan(),
						EndAt = new TimeOnly(t + 2, 0).ToTimeSpan(),
						CreatedBy = "system",
						UpdatedBy = "system",
						CreatedAt = seedDay,
						UpdatedAt = seedDay
					}
				);
			}

		builder.Entity<Slot>().HasData(slots);
	}

	private static void SeedSkills(ModelBuilder builder)
	{
		var id = 0;
		var skillCate = new List<Category>();
		var skill = new List<Skill>();

		GenerateData(
			ref id,
			skillCate,
			new()
			{
				"ranged_attack",
				"melee_fight",
				"body_movement",
				"vehicle",
				"mindset",
				"criminal_exploration",
				"personal_neat",
				"basic",
				"human_interaction",
				"technology"
			}
		);

		id = 0; // reset for new entity
		GenerateData(
			ref id,
			skill,
			relationalId: 1,
			names: new() { "pistol", "rifle", "sniper_rifle" }
		);

		GenerateData(
			ref id,
			skill,
			relationalId: 2,
			names: new() { "taekwondo", "karatedo", "melee_combat" }
		);

		GenerateData(ref id, skill, relationalId: 3, names: new() { "running", "swimming" });

		GenerateData(ref id, skill, relationalId: 4, names: new() { "driving", "piloting" });

		GenerateData(
			ref id,
			skill,
			relationalId: 5,
			names: new()
			{
				"thinking_out_of_the_box",
				"critical_thinking",
				"problem_solving",
				"planning",
				"calming",
				"leadership"
			}
		);

		GenerateData(
			ref id,
			skill,
			relationalId: 6,
			names: new() { "investigate", "judgment", "details_inspecting", "tracking", "tracing" }
		);

		GenerateData(
			ref id,
			skill,
			relationalId: 7,
			names: new() { "visibility", "adaptability", "stealing" }
		);

		GenerateData(ref id, skill, relationalId: 8, names: new() { "first_aid" });

		GenerateData(
			ref id,
			skill,
			relationalId: 9,
			names: new()
			{
				"collaboration",
				"communication",
				"interviewing",
				"interrogation",
				"convince"
			}
		);

		GenerateData(
			ref id,
			skill,
			relationalId: 10,
			names: new() { "networking", "security", "computing" }
		);

		static void GenerateData<T>(
			ref int currentId,
			List<T> data,
			List<string> names,
			int? relationalId = null
		) where T : new()
		{
			foreach (var name in names)
			{
				currentId += 1;
				var record = new T();
				TypeExtensions.SetProperty(record, "Id", currentId);
				TypeExtensions.SetProperty(record, "CreatedBy", "system");
				TypeExtensions.SetProperty(record, "UpdatedBy", "system");

				TypeExtensions.SetProperty(record, "CreatedAt", seedDay);
				TypeExtensions.SetProperty(record, "UpdatedAt", seedDay);

				TypeExtensions.SetProperty(record, "Name", name);
				if (relationalId != null)
					TypeExtensions.SetProperty(record, "CategoryId", relationalId);

				data.Add(record);
			}
		}

		builder.Entity<Category>().HasData(skillCate);
		builder.Entity<Skill>().HasData(skill);
	}
}
