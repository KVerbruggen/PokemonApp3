using KVerbruggen.Framework.Maui.ViewModels;
using KVerbruggen.Framework.Services.NavigationService;

namespace KVerbruggen.Framework.Maui.Platforms.Windows
{
    public class NavigationService : INavigationService
    {
        private readonly NavigationPage _navigationPage;
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(NavigationPage navigationPage, IServiceProvider serviceProvider)
        {
            _navigationPage = navigationPage ?? throw new ArgumentNullException(nameof(navigationPage));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Navigate to a page based on a view model
        /// </summary>
        /// <typeparam name="TViewModel">The view model type</typeparam>
        public void NavigateToViewModel<TViewModel>()
            where TViewModel : IViewModel
        {
            Page page = _serviceProvider.GetRequiredService<IViewProvider<IViewModel>>().ResolvePage();
            _navigationPage.Navigation.PushAsync(page);
        }
    }
}
