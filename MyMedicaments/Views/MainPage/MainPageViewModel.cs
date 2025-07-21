using MauiApp1.Infrastructure.Database;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Generic;

namespace MauiApp1.Views
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly MedicamentDatabaseService _databaseService;
        public ObservableCollection<Medicament> Medicaments { get; } = new();

        public ICommand OpenNewViewCommand { get; }
        public ICommand ViewMedicamentCommand { get; }

        public MainPageViewModel(MedicamentDatabaseService databaseService)
        {
            _databaseService = databaseService;
            OpenNewViewCommand = new Command(async () => await OnOpenNewViewClicked());
            ViewMedicamentCommand = new Command<Medicament>(async (medicament) => await OnViewMedicament(medicament));
        }

        private async Task OnOpenNewViewClicked()
        {
            await Shell.Current.GoToAsync("///AddMedicament");
        }

        private async Task OnViewMedicament(Medicament medicament)
        {
            if (medicament != null)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "MedicamentId", medicament.Id.ToString() }
                };
                await Shell.Current.GoToAsync("///ViewMedicament", navigationParameter);
            }
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