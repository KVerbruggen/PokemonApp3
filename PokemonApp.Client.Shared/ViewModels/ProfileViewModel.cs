using KVerbruggen.Framework.Maui.ViewModels;
using PokemonApp.Client.Shared.Models;

namespace PokemonApp.Client.Shared.ViewModels
{
    public class ProfileViewModel : ViewModel<ProfileModel>, IViewModel<ProfileModel>
    {
        public ProfileViewModel(ProfileModel profileModel)
            : base(profileModel)
        {
        }
    }
}
