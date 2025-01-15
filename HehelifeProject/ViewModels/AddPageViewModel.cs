using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HehelifeProject.Data;
using HehelifeProject.Models;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

namespace HehelifeProject.ViewModels
{
    public partial class AddPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string output;

        [ObservableProperty]
        string brand;

        [ObservableProperty]
        string model;

        [ObservableProperty]
        int year;

        [ObservableProperty]
        int mileage;

        [ObservableProperty]
        decimal price;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string ImageUrl;

        [ObservableProperty]
        public CarService carService;

        public AddPageViewModel(CarService service)
        {
            myCarsViewModel = carsVM;
            Output = "Output goes here";

        }

        [RelayCommand]
        private void AddCar()
        {
            Car newCar = new Car(Brand, model,year, mileage, price,description, imageUrl);
            AddNewCar(newCar);
        }

        [RelayCommand]
        private void ClearAll()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Year = 0;
            Mileage = 0;
            Price = 0;
            Description = string.Empty;
            ImageUrl = string.Empty;
        }


        private async void AddNewCar(Car newcar)
        {
            try
            {
                (bool, string) validityCheck = newcar.Validate();
                if (validityCheck.Item1)
                {
                    await MyCarsViewModel.AddCarAsync(newCar);
                    ClearAll();
                    output = newcar.Year.ToString() + " " + newcar.Make + " " + newcar.Model + " was added. ";
                }
                else 
                {
                    output = "problem with the data entry. \n" + ex.Message;

                }
            }
            catch (Exception ex)
            {
                output = "something went wrong. \n" + ex.Message;
            }

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
                Description = Description,
                ImageUrl = ImageUrl
            };

            try
            {
                var isSuccess = await carService.AddCarAsync(newCar);
                if (isSuccess)
                {
                    ClearAll();
                    Output = $"{newCar.Year} {newCar.Brand} {newCar.Model} is succesvol toegevoegd.";
                }
                else
                {
                    Output = "Het toevoegen van de auto is mislukt. Probeer opnieuw.";
                }
            }
            catch (Exception ex)
            {
                Output = $"Er is een fout opgetreden: {ex.Message}";
            }
        }

    }
}
