using Microsoft.Maui.Controls;
using System;

namespace MauiApp1.Views
{
    public partial class AddMedicament : ContentPage
    {
        private readonly AddMedicamentViewModel _viewModel;
        public AddMedicament(AddMedicamentViewModel viewModel)
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
    }
} 