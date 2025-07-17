using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace MauiApp1.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel != null)
                await _viewModel.LoadMedicamentsAsync();
        }

        private async void OnMedicamentSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var selectedMedicament = e.CurrentSelection[0] as Infrastructure.Database.Medicament;
                if (selectedMedicament != null)
                {
                    var navigationParameter = new Dictionary<string, object>
                    {
                        { "Medicament", selectedMedicament }
                    };
                    await Shell.Current.GoToAsync("ViewMedicament", navigationParameter);
                }
            }
            // Deselect item
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
