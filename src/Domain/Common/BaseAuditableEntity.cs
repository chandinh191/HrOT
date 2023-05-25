namespace hrOT.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity, ISoftDelete
{
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    protected BaseAuditableEntity()
    {
        Created = DateTime.Now;
        IsDeleted = false;
    }
}