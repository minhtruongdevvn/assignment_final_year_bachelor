namespace AtmAPI.Extensions;

public class AtmSieveProcessor : SieveProcessor
{
	public AtmSieveProcessor(IOptions<SieveOptions> options) : base(options) { }

	protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
	{
		mapper.MapBaseIdentityProps<Student>();
		mapper.MapBaseIdentityProps<Lecturer>();
		mapper.MapBaseIdentityProps<Operator>();

		mapper.MapBaseEntityProps<Operator>();
		mapper.MapBaseEntityProps<Lecturer>();
		mapper.MapBaseEntityProps<Student>();
		mapper.MapBaseEntityProps<Schedule>();
		mapper.MapBaseEntityProps<Category>();
		mapper.MapBaseEntityProps<Absence>();
		mapper.MapBaseEntityProps<Class>();
		mapper.MapBaseEntityProps<Skill>();
		mapper.MapBaseEntityProps<Slot>();
		mapper.MapBaseEntityProps<ExternalInstitution>();

		mapper.MapBaseMetadataProps<ClassLecturer>();
		mapper.MapBaseMetadataProps<ExternalInstitutionStudent>();

		mapper.MapProps<Skill>(
			(p => p.Name, null),
			(p => p.Category.Id, "category_id"),
			(p => p.Category.Name, "category_name")
		);

		mapper.MapProps<Category>((p => p.Name, null));

		mapper.MapProps<SkillReport>(
			(p => p.ClassId, "class_id"),
			(p => p.StudentId, "student_id"),
			(p => p.Student.FamilyName, "student_family_name"),
			(p => p.Student.GivenName, "student_given_name"),
			(p => p.Student.Code, "student_code")
		);

		mapper.MapProps<Class>(
			(p => p.Available, null),
			(p => p.Name, null),
			(p => p.Placement, null),
			(p => p.SkillId, "skill_id"),
			(p => p.StartDate, "start_date"),
			(p => p.EndDate, "end_date"),
			(p => p.EnableAutomation, "enable_automation"),
			(p => p.MaxLearner, "max_learner")
		);

		mapper.MapProps<Student>(
			(p => p.SkillReports, "reports"),
			(p => p.SelfDiscipline, "self_discipline"),
			(p => p.ReactionTime, "reaction_time"),
			(p => p.Strength, null),
			(p => p.Stamina, null),
			(p => p.IQ, null),
			(p => p.EQ, null),
			(p => p.Sex, null)
		);

		mapper.MapProps<ClassLecturer>(
			(p => p.LecturerId, "lecturer_id"),
			(p => p.ClassId, "class_id")
		);

		mapper.MapProps<Schedule>(
			(p => p.ClassId, "class_id"),
			(p => p.SlotId, "slot_id"),
			(p => p.Class.Name, "class_name"),
			(p => p.Slot.DayOfWeek, "slot_day_of_week")
		);

		mapper.MapProps<Slot>(
			(p => p.DayOfWeek, "day_of_week"),
			(p => p.StartAt, "start_at"),
			(p => p.EndAt, "end_at")
		);

		mapper.MapProps<Absence>(
			(p => p.AbsenceDate, "absence_date"),
			(p => p.ScheduleId, "schedule_id"),
			(p => p.StudentId, "student_id")
		);

		mapper.MapProps<AttendanceResponse>(
			(p => p.IsAttended, "is_attended"),
			(p => p.AttendDate, "slot_id"),
			(p => p.@Class!.Id, "class_id"),
			(p => p.Slot!.Id, "slot_id"),
			(p => p.ScheduleId, "schedule_id")
		);

		mapper.MapProps<ExternalInstitution>(
			(p => p.Name, null),
			(p => p.Code, null),
			(p => p.PassCode, null),
			(p => p.SkillIds, null)
		);

		mapper.MapProps<ExternalInstitutionStudent>(
			(p => p.Student.Code, null),
			(p => p.Student.Email, null),
			(p => p.Student.UserName, "user_name"),
			(p => p.Student.GivenName, "given_name"),
			(p => p.Student.FamilyName, "family_name"),
			(p => p.Student.BirthDate, "birth_date"),
			(p => p.Student.SkillReports, "reports")
		);

		return mapper;
	}
}
