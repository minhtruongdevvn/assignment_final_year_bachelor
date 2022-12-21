namespace AAM.Application;

public abstract class AuditableDTO
{
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}

