using ContactSystem.Application.Entities;

namespace ContactSystem.Application.Repositories.Interfaces;

public interface IContactsRepository : IEntitiesRepository<ContactEntity, Guid>
{
    Task<(IEnumerable<ContactEntity>, int)> SearchContactsAsync(Guid officeId, string searchTerm, int page, int pageSize);
    Task<(IEnumerable<ContactEntity>, int)> SearchAllContactsInOfficeAsync(Guid officeId, int page, int pageSize);
}