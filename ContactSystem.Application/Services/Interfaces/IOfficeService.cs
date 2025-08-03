using ContactSystem.Application.Entities;

namespace ContactSystem.Application.Services.Interfaces
{
    public interface IOfficeService
    {
        Guid CurrentOfficeId { get; set; }

        Task<OfficeEntity> GetOfficeByIdAsync(Guid id);
        Task<IEnumerable<OfficeEntity>> GetAllOfficesAsync();
        Task AddOfficeAsync(OfficeEntity contact);
        Task UpdateOfficeAsync(OfficeEntity contact);
        Task DeleteOfficeAsync(Guid id);
    }
}
