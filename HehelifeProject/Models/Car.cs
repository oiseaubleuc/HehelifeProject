using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace HehelifeProject.Models
{
    public class Car
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2100, ErrorMessage = "Year must be a valid number")]
        public int Year { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Mileage is required")]
        public int Mileage { get; set; }

        public Car()
        {

        }

        public Car(String b, String md, int p, int y, String d, String i, int m)
        {

            Brand = string.Empty;
            Model = string.Empty;
            Price = 0;
            Year = 0;
            Description = string.Empty;
            ImageUrl = string.Empty;
            Mileage = 0;


        }

    }
}
