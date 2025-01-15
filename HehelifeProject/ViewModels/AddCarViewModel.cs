using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HehelifeProject.Data;
using HehelifeProject.Models;
using Microsoft.VisualBasic.FileIO;

namespace HehelifeProject.ViewModels
{
    public partial class AddCarViewModel : ObservableObject
    {
        [ObservableProperty]
        private string output;

        [ObservableProperty]
        private string brand;

        [ObservableProperty]
        private string model;

        [ObservableProperty]
        private int year;

        [ObservableProperty]
        private int mileage;

        [ObservableProperty]
        private decimal price;

        [ObservableProperty]
        private string ImageUrl;

        [ObservableProperty]
        private string? description;

        private readonly CarService carService;

        public AddCarViewModel(CarService service)
        {
            carService = service ?? throw new ArgumentNullException(nameof(service));
        }

        [RelayCommand]
        private async Task AddCarAsync()
        {
            var newCar = new Car
            {
                Brand = Brand,
                Model = Model,
                Year = Year,
                Mileage = Mileage,
                Price = Price,
                ImageUrl = ImageUrl,
                Description = Description
            };

            try
            {
                var isSuccess = await carService.AddCarAsync(newCar);
                if (isSuccess)
                {
                    ClearAll();
                    Output = $"{newCar.Year} {newCar.Brand} {newCar.Model} was successfully added.";
                }
                else
                {
                    Output = "Failed to add the car. Please try again.";
                }
            }
            catch (Exception ex)
            {
                Output = $"An error occurred: {ex.Message}";
            }
        }

        [RelayCommand]
        private void ClearAll()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Year = 0;
            Mileage = 0;
            Price = 0;
            ImageUrl = string.Empty;
            Description = string.Empty;
        }
    }
}
