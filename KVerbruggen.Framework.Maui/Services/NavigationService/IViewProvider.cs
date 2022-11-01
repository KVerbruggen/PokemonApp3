using KVerbruggen.Framework.Maui.ViewModels;

namespace KVerbruggen.Framework.Services.NavigationService
{
    /// <summary>
    /// A class to resolve a Maui page based on a view model type
    /// </summary>
    /// <typeparam name="TViewModel">The view model type</typeparam>
    public interface IViewProvider<TViewModel>
        where TViewModel : IViewModel
    {
        /// <summary>
        /// Resolve the related Maui page
        /// </summary>
        /// <returns>The resolved page</returns>
        public Page ResolvePage();
    }
}
