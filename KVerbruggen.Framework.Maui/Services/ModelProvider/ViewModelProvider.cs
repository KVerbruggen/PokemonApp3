using Autofac;
using KVerbruggen.Framework.Maui.Properties;
using KVerbruggen.Framework.Maui.ViewModels;

namespace KVerbruggen.Framework.Maui.Services.ViewModelProvider
{
    /// <summary>
    /// A service that provides instances of models
    /// </summary>
    public class ViewModelProvider : IViewModelProvider
    {
        /// <summary>
        /// The lifetimeScope that provides the view models
        /// </summary>
        private ILifetimeScope _lifetimeScope;

        /// <summary>
        /// Initializes an instance of the ViewModelProvider class
        /// </summary>
        /// <param name="lifetimeScope">The lifetimeScope to get the views from</param>
        public ViewModelProvider(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// Provide an instance of a viewModel type
        /// </summary>
        /// <typeparam name="TViewModel">The type of the viewModel to provide</typeparam>
        /// <returns>An instance of the model type</returns>
        public TViewModel Provide<TViewModel>()
            where TViewModel : IViewModel
        {
            TViewModel result = _lifetimeScope.Resolve<TViewModel>()
                ?? throw new ArgumentException(ErrorResources.ViewModelTypeNotResolved, typeof(TViewModel).Name);

            return result;
        }
    }
}
