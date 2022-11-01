using PokemonApp.Maui.ViewModels;

namespace PokemonApp.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainViewModel)
        {
            BindingContext = mainViewModel;

            InitializeComponent();
        }
    }
}