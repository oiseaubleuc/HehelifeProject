using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm;
using System.Collections.ObjectModel;


namespace HehelifeProject.ViewModels
{
    public partial class TitlePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string whyHehelife;

        // Constructor
        public TitlePageViewModel()
        {
            Task t = LoadAssetsForPC;

        }

        // Command om naar de CarList te gaan
        [RelayCommand]
        private async Task GoInventory()
        {
            await Shell.Current.GoToAsync("//CarListPage");
        }

        // Command om naar de AddCar-pagina te gaan
        [RelayCommand]
        private async Task GoAddVehicle()
        {
            await Shell.Current.GoToAsync("//CarEditPage");
        }

        // Laad tekstbestand op Windows
        private async Task LoadAssetsForPC()
        {
            try
            {
                // Zorg ervoor dat het bestand correct is toegevoegd aan het project
                using var stream = await FileSystem.OpenAppPackageFileAsync("why_hehelife.txt");
                using var reader = new StreamReader(stream);
                var contents = await reader.ReadToEndAsync();
                WhyHehelife = contents;
            }
            catch (Exception ex)
            {
                WhyHehelife = "Failed to load content. Please check your file setup.";
                // Log eventueel de fout
            }
        }

        // Laad tekstbestand op Mac
        private void LoadAssetsForMac()
        {
            try
            {
                // Zorg ervoor dat het bestand correct is toegevoegd aan het project
                using StreamReader sr = File.OpenText("why_hehelife.txt");
                var contents = sr.ReadToEnd();
                WhyHehelife = contents;
            }
            catch (Exception ex)
            {
                WhyHehelife = "Failed to load content. Please check your file setup.";
                // Log eventueel de fout
            }
        }
    }
}
