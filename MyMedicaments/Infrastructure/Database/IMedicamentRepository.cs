using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp1.Infrastructure.Database
{
    public interface IMedicamentRepository
    {
        Task<IEnumerable<Medicament>> GetAllAsync();
        Task<Medicament?> GetByIdAsync(Guid id);
        Task AddAsync(Medicament medicament);
        Task UpdateAsync(Guid id, Medicament medicament);
        Task DeleteAsync(Guid id);
    }
} 