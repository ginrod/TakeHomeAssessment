using ContactSystem.Application.Entities;
using ContactSystem.Application.Repositories.Interfaces;
using ContactSystem.Application.Services.Interfaces;

namespace ContactSystem.Application.Services
{
    public class OfficeService : IOfficeService
    {
        public Guid CurrentOfficeId { get; set; }

        private IOfficesRepository _officesRepository;

        public OfficeService(IOfficesRepository officesRepository)
        {
            CurrentOfficeId = new Guid("ff0c022e-1aff-4ad8-2231-08db0378ac98");
            _officesRepository = officesRepository;
        }
        public async Task<IEnumerable<OfficeEntity>> GetAllOfficesAsync()
        {
            return await _officesRepository.GetAllAsync();
        }

        public async Task<OfficeEntity> GetOfficeByIdAsync(Guid id)
        {
            return await _officesRepository.GetByIdAsync(id);
        }

        public async Task AddOfficeAsync(OfficeEntity office)
        {
            await _officesRepository.AddAsync(office);
        }

        public async Task UpdateOfficeAsync(OfficeEntity office)
        {
            await _officesRepository.UpdateAsync(office);
        }

        public async Task DeleteOfficeAsync(Guid id)
        {
            await _officesRepository.DeleteAsync(id);
        }
    }
}
