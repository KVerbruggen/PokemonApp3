using KVerbruggen.Framework.Maui.ViewModels;

namespace KVerbruggen.Framework.Services.NavigationService
{
    /// <summary>
    /// A class to resolve a Maui page based on a view model type
    /// </summary>
    /// <typeparam name="TViewModel">The view model type</typeparam>
    /// <typeparam name="TView">The view type</typeparam>
    public class ViewProvider<TViewModel, TView> : IViewProvider<TViewModel>
        where TViewModel : IViewModel
        where TView : Page
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Resolve the related Maui page
        /// </summary>
        /// <returns>The resolved page</returns>
        public Page ResolvePage()
        {
            return _serviceProvider.GetRequiredService<TView>();
        }
    }
}
