namespace ContactSystem.Application.Entities;

public class ContactOfficeRelation
{
    public Guid ContactId { get; set; }

    public Guid OfficeId { get; set; }

    public ContactEntity Contact { get; set; } = null!;

    public OfficeEntity Office { get; set; } = null!;
}