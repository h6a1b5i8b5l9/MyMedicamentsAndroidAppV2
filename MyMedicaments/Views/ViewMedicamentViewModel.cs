using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Views
{
    public class ViewMedicamentViewModel : INotifyPropertyChanged
    {
        private string name;
        private string description;
        private DateTime expirationDate;
        private string category;
        private string photoPath;

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); }
        }

        public DateTime ExpirationDate
        {
            get => expirationDate;
            set { expirationDate = value; OnPropertyChanged(); }
        }

        public string Category
        {
            get => category;
            set { category = value; OnPropertyChanged(); }
        }

        public string PhotoPath
        {
            get => photoPath;
            set { photoPath = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 