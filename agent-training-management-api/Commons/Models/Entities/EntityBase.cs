namespace AtmAPI.Commons.Models.Entities;

public abstract class EntityBase : EntityMetadataBase
{
	[Key, Column(Order = 0)]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
}
