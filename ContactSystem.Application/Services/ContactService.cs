using ContactSystem.Application.Entities;
using ContactSystem.Application.Repositories.Interfaces;
using ContactSystem.Application.Services.Interfaces;

namespace ContactSystem.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactsRepository _contactsRepository;

    public ContactService(IContactsRepository ContactRepository)
    {
        _contactsRepository = ContactRepository;
    }

    public async Task<ContactEntity> GetContactByIdAsync(Guid id)
    {
        return await _contactsRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ContactEntity>> GetAllContactsAsync()
    {
        return await _contactsRepository.GetAllAsync();
    }

    public async Task AddContactAsync(ContactEntity Contact)
    {
        await _contactsRepository.AddAsync(Contact);
    }

    public async Task UpdateContactAsync(ContactEntity Contact)
    {
        await _contactsRepository.UpdateAsync(Contact);
    }

    public async Task DeleteContactAsync(Guid id)
    {
        await _contactsRepository.DeleteAsync(id);
    }

    public async Task<(IEnumerable<ContactEntity>, int)> SearchContactsAsync(Guid officeId, string searchTerm, int page, int pageSize)
    {
        return await _contactsRepository.SearchContactsAsync(officeId, searchTerm, page, pageSize);
    }
    public async Task<(IEnumerable<ContactEntity>, int)> SearchAllContactsInOfficeAsync(Guid officeId, int page, int pageSize)
    {
        return await _contactsRepository.SearchAllContactsInOfficeAsync(officeId, page, pageSize);
    }
}
