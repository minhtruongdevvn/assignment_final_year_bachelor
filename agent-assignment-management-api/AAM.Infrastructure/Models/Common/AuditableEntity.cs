namespace AAM.Infrastructure.Models;

public abstract class AuditableEntity
{
    public string ModifiedBy { get; set; } = "system";
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
