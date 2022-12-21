namespace AtmAPI.Models.Entities;

public class Schedule : EntityBase
{
	public int? InitialSlotId { get; set; }
	public int SlotId { get; set; }
	public int ClassId { get; set; }

	public virtual Slot Slot { get; set; } = default!;
	public virtual Class Class { get; set; } = default!;
	public virtual ICollection<Absence>? Absences { get; set; }
	public virtual List<ScheduleCheckIn> ScheduleCheckIns { get; set; } = new();
}
