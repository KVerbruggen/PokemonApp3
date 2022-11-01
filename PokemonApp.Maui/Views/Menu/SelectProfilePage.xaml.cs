using PokemonApp.Client.Shared.ViewModels;

namespace PokemonApp.Maui.Views.Menu;

public partial class SelectProfilePage : ContentPage
{
	public SelectProfilePage(ProfileViewModel viewModel)
	{
        BindingContext = viewModel;

        InitializeComponent();
	}
}