using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MauiApp1.Infrastructure.Database
{
    public class SqliteMedicamentRepository : IMedicamentRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public SqliteMedicamentRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MedicamentDto>().Wait();
        }

        public async Task<IEnumerable<Medicament>> GetAllAsync()
        {
            var dtos = await _database.Table<MedicamentDto>().ToListAsync();
            return dtos.Select(ToMedicament);
        }

        public async Task<Medicament?> GetByIdAsync(Guid id)
        {
            var idStr = id.ToString();
            var dto = await _database.Table<MedicamentDto>().Where(m => m.Id == idStr).FirstOrDefaultAsync();
            return dto == null ? null : ToMedicament(dto);
        }

        public async Task AddAsync(Medicament medicament)
        {
            var id = Guid.NewGuid();
            medicament.Id = id;
            await _database.InsertAsync(ToDto(medicament));
        }

        public async Task UpdateAsync(Guid id, Medicament medicament)
        {
            medicament.Id = id;
            await _database.UpdateAsync(ToDto(medicament));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _database.DeleteAsync<MedicamentDto>(id.ToString());
        }

        // DTO for SQLite
        public class MedicamentDto
        {
            [PrimaryKey]
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public DateTime ExpirationDate { get; set; }
            public int Category { get; set; }
            public string? PhotoPath { get; set; }
        }

        private static MedicamentDto ToDto(Medicament m) => new MedicamentDto
        {
            Id = m.Id.ToString(),
            Name = m.Name,
            Description = m.Description,
            ExpirationDate = m.ExpirationDate,
            Category = (int)m.Category,
            PhotoPath = m.PhotoPath
        };

        private static Medicament ToMedicament(MedicamentDto dto) => new Medicament
        {
            Id = Guid.Parse(dto.Id),
            Name = dto.Name,
            Description = dto.Description,
            ExpirationDate = dto.ExpirationDate,
            Category = (MedicamentCategory)dto.Category,
            PhotoPath = dto.PhotoPath
        };
    }
} 