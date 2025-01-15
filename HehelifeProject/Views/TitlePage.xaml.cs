using HehelifeProject.ViewModels;

namespace HehelifeProject.Views;

public partial class TitlePage : ContentPage
{
    public TitlePage()
    {
        InitializeComponent();
        BindingContext = new TitlePageViewModel();
    }
}