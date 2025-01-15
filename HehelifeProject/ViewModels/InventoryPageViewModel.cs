using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HehelifeProject.Models;
using SQLite;

namespace HehelifeProject.CarService

{
    public partial class CarService
    {
        private const string DbName = "Cars.db3";
        private static readonly string DbPath = Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection dbConn;
        private SQLiteAsyncConnection Database => dbConn ??= new SQLiteAsyncConnection(DbPath,
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

        public static ObservableCollection<Car> Cars { get; private set; }

        public CarService()
        {
            Task.Run(async () =>
            {
                await CreateTableIfNotExists<Car>();
                Cars = new ObservableCollection<Car>(await GetInventoryAsync());
                if (Cars.Count == 0)
                {
                    await PopulateCars();
                }
            });
        }

        private async Task CreateTableIfNotExists<T>() where T : class, new()
        {
            await Database.CreateTableAsync<T>();
        }

        public async Task<ObservableCollection<Car>> GetInventoryAsync()
        {
            var carList = await Database.Table<Car>().ToListAsync();
            return new ObservableCollection<Car>(carList.OrderBy(c => c.Brand).ThenBy(c => c.Model).ThenBy(c => c.Year));
        }

        public async Task AddCarAsync(Car car)
        {
            await Database.InsertAsync(car);
            Cars.Add(car);
        }

        public async Task UpdateCarAsync(Car car)
        {
            await Database.UpdateAsync(car);
            var existingCar = Cars.FirstOrDefault(c => c.Id == car.Id);
            if (existingCar != null)
            {
                Cars[Cars.IndexOf(existingCar)] = car;
            }
        }

        public async Task DeleteCarAsync(Car car)
        {
            await Database.DeleteAsync(car);
            Cars.Remove(car);
        }

        private async Task PopulateCars()
        {
            var sampleCars = new[]
            {
                new Car { Brand = "Ford", Model = "Mustang", Year = 2016, Mileage = 67203, Price = 32999, ImageUrl = "https://images.pexels.com/photos/9334968/pexels-photo-9334968.jpeg" },
                new Car { Brand = "Chevrolet", Model = "Camaro", Year = 2017, Mileage = 109441, Price = 26999, ImageUrl = "https://images.pexels.com/photos/575386/pexels-photo-575386.jpeg" },
                new Car { Brand = "Toyota", Model = "Tacoma", Year = 2018, Mileage = 89175, Price = 27999, ImageUrl = "https://images.unsplash.com/photo-1603598154505-0192e5363a53" }
            };

            foreach (var car in sampleCars)
            {
                await AddCarAsync(car);
            }
        }
    }
}
