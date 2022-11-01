using KVerbruggen.Framework.Maui.ViewModels;

namespace KVerbruggen.Framework.Services.NavigationService
{
    /// <summary>
    /// A service implementing what to do with new view models
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Handle a new view model
        /// </summary>
        void NavigateToViewModel<TViewModel>()
            where TViewModel : IViewModel;
    }
}
