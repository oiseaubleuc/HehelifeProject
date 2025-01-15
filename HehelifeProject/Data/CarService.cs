using System;
using CommunityToolkit.Mvvm.ComponentModel;
using HehelifeProject.Models;
using SQLite;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using HehelifeProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HehelifeProject.Data
{
    public  class CarService
    {
        // Database naam en pad
        private const string DbName = "Cars.db3";
        private static readonly string DbPath = Path.Combine(FileSystem.AppDataDirectory, DbName);

        // SQLite connectie
        private SQLiteAsyncConnection dbConn;
        private SQLiteAsyncConnection Database => dbConn ??= new SQLiteAsyncConnection(DbPath,
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

        // Observable collectie voor data binding
        public static Car car;
        public static Car newCar;
        public static ObservableCollection<Car> Cars { get; private set; }
        public static Car CurrentCar;


        // Constructor
        public CarService()
        {
            // Creëer de database en tabel
            var dbConn = Path.Combine(DbPath, DbName);
            Task buildTable = CreateTableIfNotExists<Car>();
        }







        // Maak de tabel als deze nog niet bestaat
        private async Task CreateTableIfNotExists<Table>() where Table : class, new()
        {
            await Database.CreateTableAsync<Table>();
        }








        // Haal alle auto's op uit de database
        public async Task<ObservableCollection<Car>> GetAllCarsAsync()
        {
            List<Car> myCars = await Database.Table<Car>().ToListAsync();
            IEnumerable<Car> query = from c in myCars
                                     orderby (c.Brand + c.Model + c.Year.ToString())
                                     select c;
            Cars ??= new ObservableCollection<Car>();
            foreach(var car in query)
            {
                Cars.Add(car);
            }
            return Cars;
        }






        private async Task<TableResult> Execute<Table, TableResult>(Func<Task<TableResult>> action)where Table : class, new() 
        {
            await CreateTableIfNotExists<Table>();
            return await action();


        }





        public async Task<Table> GetCarByIDAsync<Table>(object primaryKey) where Table : class, new()
        {
            return await Execute <Table, Table>(async () => await Database.GetAsync<Table>(primaryKey));
        }





        // Voeg een nieuwe auto toe aan de database
        public async Task<bool> AddCarAsync<Table>(Table car) where Table : class, new()
        {
           return await Execute<Table, bool>(async () => await Database.InsertAsync(car) > 0);
        }




        // Update een auto in de database
        public async Task<bool> UpdateCarAsync<Table>(Table item) where Table : class, new()
        {
            await CreateTableIfNotExists<Table>();
            return await Database.UpdateAsync(item) > 0;
        }






        // Haal een auto op op basis van ID
        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await Database.FindAsync<Car>(id);
        }






        // Verwijder een auto uit de database
        public async Task<bool> DeleteCarAsync <Table>(Table item) where Table : class, new()
        {
            await CreateTableIfNotExists<Table>();
            return await Database.DeleteAsync(item) > 0;
        }

       



        // Voeg dummy data toe
        public async Task AddDummyDataAsync()
        {
            await AddCarAsync(new Car { Brand = "Ford", Model = "Mustang", Year = 2016, Mileage = 67203, Price = 32999, ImageUrl = "https://images.pexels.com/photos/9334968/pexels-photo-9334968.jpeg" });
            await AddCarAsync(new Car { Brand = "Chevrolet", Model = "Camaro", Year = 2017, Mileage = 109441, Price = 26999, ImageUrl = "https://images.pexels.com/photos/575386/pexels-photo-575386.jpeg" });
            await AddCarAsync(new Car { Brand = "Toyota", Model = "Tacoma", Year = 2018, Mileage = 89175, Price = 27999, ImageUrl = "https://images.unsplash.com/photo-1603598154505-0192e5363a53" });
        }
    }
}
