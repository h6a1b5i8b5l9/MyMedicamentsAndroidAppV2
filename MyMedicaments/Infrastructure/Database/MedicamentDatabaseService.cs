using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp1.Infrastructure.Database
{
    public class MedicamentDatabaseService
    {
        private readonly IMedicamentRepository _repository;

        public MedicamentDatabaseService(IMedicamentRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Medicament>> GetAllMedicamentsAsync() => _repository.GetAllAsync();
        public Task<Medicament?> GetMedicamentByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task AddMedicamentAsync(Medicament medicament) => _repository.AddAsync(medicament);
        public Task UpdateMedicamentAsync(Guid id, Medicament medicament) => _repository.UpdateAsync(id, medicament);
        public Task DeleteMedicamentAsync(Guid id) => _repository.DeleteAsync(id);
    }
} 