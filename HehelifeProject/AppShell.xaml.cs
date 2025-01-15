using HehelifeProject.Data;
using HehelifeProject.Views;


namespace HehelifeProject;

    public partial class AppShell : Shell
    {
         public static CarService carService;

        public AppShell()
        {

            InitializeComponent();
        carService = new CarService();
        Routing.RegisterRoute("carsInventory", typeof(InventoryPage));
        Routing.RegisterRoute("addVehicle", typeof(AddPage));
       // Routing.RegisterRoute("details", typeof(DetailsDialog));

    }
}

