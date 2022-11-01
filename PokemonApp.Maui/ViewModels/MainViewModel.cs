using KVerbruggen.Framework.Maui.Commands.Generic;
using KVerbruggen.Framework.Maui.Services.ViewModelProvider;
using KVerbruggen.Framework.Maui.ViewModels;
using PokemonApp.Client.Shared.Models;
using PokemonApp.Client.Shared.ViewModels;
using System.Windows.Input;

namespace PokemonApp.Maui.ViewModels
{
    /// <summary>
    /// The model for the main window
    /// </summary>
    public class MainViewModel : ViewModel<MainModel>
    {
        private readonly IViewModelProvider _viewModelProvider;

        /// <summary>
        /// The command to quit the app
        /// </summary>
        public ICommand QuitCommand { get; set; }

        /// <summary>
        /// The command to go the the profile selection
        /// </summary>
        public ICommand StartCommand { get; set; }

        public MainViewModel(IViewModelProvider viewModelProvider, MainModel mainModel)
            : base(mainModel)
        {
            _viewModelProvider = viewModelProvider;

            QuitCommand = new RelayCommand(_ => Application.Current.Quit(), canExecute: _ => true);
            StartCommand = new RelayCommand(_ => GetProfileViewModel());
        }

        public MainViewModel()
            : base(new MainModel())
        { }

        private ProfileViewModel GetProfileViewModel() => _viewModelProvider.Provide<ProfileViewModel>();
    }
}
