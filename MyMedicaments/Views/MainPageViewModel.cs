using MauiApp1.Infrastructure.Database;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.Views
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly MedicamentDatabaseService _databaseService;
        public ObservableCollection<Medicament> Medicaments { get; } = new();

        public ICommand OpenNewViewCommand { get; }

        public MainPageViewModel(MedicamentDatabaseService databaseService)
        {
            _databaseService = databaseService;
            OpenNewViewCommand = new Command(async () => await OnOpenNewViewClicked());
        }

        private async Task OnOpenNewViewClicked()
        {
            await Shell.Current.GoToAsync("///AddMedicament");
        }

        public async Task LoadMedicamentsAsync()
        {
            Medicaments.Clear();
            var medicaments = await _databaseService.GetAllMedicamentsAsync();
            foreach (var medicament in medicaments)
                Medicaments.Add(medicament);
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 