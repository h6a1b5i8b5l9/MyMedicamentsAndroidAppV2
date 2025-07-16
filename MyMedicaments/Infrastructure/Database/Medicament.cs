using System;

namespace MauiApp1.Infrastructure.Database
{
    public class Medicament
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public MedicamentCategory Category { get; set; }
        public string? PhotoPath { get; set; }
    }

    public enum MedicamentCategory
    {
        Painkiller,
        Stomach,
        Antibiotic,
        Allergy,
        Vitamins,
        Heart,
        Diabetes,
        Other
    }
} 