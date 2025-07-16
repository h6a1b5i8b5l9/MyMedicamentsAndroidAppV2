using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MauiApp1.Infrastructure.Database
{
    public class InMemoryMedicamentRepository : IMedicamentRepository
    {
        private readonly List<Medicament> _medicaments = new();

        public Task<IEnumerable<Medicament>> GetAllAsync()
        {
            return Task.FromResult(_medicaments.AsEnumerable());
        }

        public Task<Medicament?> GetByIdAsync(Guid id)
        {
            var medicament = _medicaments.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(medicament);
        }

        public Task AddAsync(Medicament medicament)
        {
            medicament.Id = Guid.NewGuid();
            _medicaments.Add(medicament);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Guid id, Medicament medicament)
        {
            var index = _medicaments.FindIndex(m => m.Id == id);
            if (index >= 0)
            {
                medicament.Id = id;
                _medicaments[index] = medicament;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var index = _medicaments.FindIndex(m => m.Id == id);
            if (index >= 0)
            {
                _medicaments.RemoveAt(index);
            }
            return Task.CompletedTask;
        }
    }
} 