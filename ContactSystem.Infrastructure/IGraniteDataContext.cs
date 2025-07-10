using ContactSystem.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure;

public interface IGraniteDataContext
{
    DbSet<ContactEntity> Contacts { get; set; }

    DbSet<OfficeEntity> Offices { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}