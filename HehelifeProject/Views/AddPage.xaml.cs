
using HehelifeProject.ViewModels;


namespace HehelifeProject.Views;


public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
		BindingContext = new AddPageViewModel(AppShell.carService);
	}
}