using Microsoft.Maui.Controls;
using MauiApp1.Infrastructure.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp1.Views
{
    public partial class ViewMedicament : ContentPage, IQueryAttributable
    {
        private readonly MedicamentDatabaseService _databaseService;
        private readonly ViewMedicamentViewModel _viewModel;
        public ViewMedicament(MedicamentDatabaseService databaseService, ViewMedicamentViewModel viewModel)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null && query.ContainsKey("MedicamentId"))
            {
                if (System.Guid.TryParse(query["MedicamentId"]?.ToString(), out var id))
                {
                    var medicament = await _databaseService.GetMedicamentByIdAsync(id);
                    if (medicament != null)
                    {
                        _viewModel.Name = medicament.Name;
                        _viewModel.Description = medicament.Description;
                        _viewModel.ExpirationDate = medicament.ExpirationDate;
                        _viewModel.Category = medicament.Category.ToString();
                        _viewModel.PhotoPath = medicament.PhotoPath;
                    }
                }
            }
        }
    }
} 