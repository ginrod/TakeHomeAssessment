using ContactSystem.Application.Entities;

namespace ContactSystem.Application.Services.Interfaces;

public interface IContactService
{
    Task<ContactEntity> GetContactByIdAsync(Guid id);
    Task<IEnumerable<ContactEntity>> GetAllContactsAsync();
    Task AddContactAsync(ContactEntity contact);
    Task UpdateContactAsync(ContactEntity contact);
    Task DeleteContactAsync(Guid id);
    Task<(IEnumerable<ContactEntity>, int)> SearchContactsAsync(Guid officeId, string searchTerm, int page, int pageSize);
    Task<(IEnumerable<ContactEntity>, int)> SearchAllContactsInOfficeAsync(Guid officeId, int page, int pageSize);
}