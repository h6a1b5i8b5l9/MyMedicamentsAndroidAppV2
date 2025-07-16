using MauiApp1.Infrastructure.Database;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;

namespace MauiApp1.Views
{
    public class AddMedicamentViewModel : INotifyPropertyChanged
    {
        private readonly MedicamentDatabaseService _databaseService;
        public ObservableCollection<Medicament> Medicaments { get; } = new();

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        private DateTime _expirationDate = DateTime.Today;
        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set { _expirationDate = value; OnPropertyChanged(); }
        }

        private MedicamentCategory _category;
        public MedicamentCategory Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        private string? _photoPath;
        public string? PhotoPath
        {
            get => _photoPath;
            set { _photoPath = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MedicamentCategory> AvailableCategories { get; }
        private MedicamentCategory _selectedCategory;
        public MedicamentCategory SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; Category = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand GoBackCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public AddMedicamentViewModel(MedicamentDatabaseService databaseService)
        {
            _databaseService = databaseService;
            SaveCommand = new Command(async () => await SaveMedicamentAsync());
            GoBackCommand = new Command(async () => await GoBackAsync());
            AvailableCategories = new ObservableCollection<MedicamentCategory>(
                Enum.GetValues(typeof(MedicamentCategory)).Cast<MedicamentCategory>());
            SelectedCategory = MedicamentCategory.Other;
        }

        public async Task LoadMedicamentsAsync()
        {
            Medicaments.Clear();
            var medicaments = await _databaseService.GetAllMedicamentsAsync();
            foreach (var medicament in medicaments)
                Medicaments.Add(medicament);
        }

        private async Task SaveMedicamentAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return;
            var medicament = new Medicament
            {
                Name = Name,
                Description = Description,
                ExpirationDate = ExpirationDate,
                Category = SelectedCategory,
                PhotoPath = PhotoPath
            };
            await _databaseService.AddMedicamentAsync(medicament);
            Medicaments.Add(medicament);
            Name = string.Empty;
            Description = string.Empty;
            ExpirationDate = DateTime.Today;
            SelectedCategory = MedicamentCategory.Other;
            PhotoPath = string.Empty;
        }

        private async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("///MainPage", true);
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 