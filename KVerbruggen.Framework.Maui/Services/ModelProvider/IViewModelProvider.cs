using KVerbruggen.Framework.Maui.ViewModels;
using KVerbruggen.Framework.Models;

namespace KVerbruggen.Framework.Maui.Services.ViewModelProvider
{
    /// <summary>
    /// A service that can provide instances of view models
    /// </summary>
    public interface IViewModelProvider
    {
        /// <summary>
        /// Provide an instance of a model type
        /// </summary>
        /// <typeparam name="TViewModel">The type of the model to provide</typeparam>
        /// <returns>An instance of the viewModel type</returns>
        TViewModel Provide<TViewModel>()
            where TViewModel : IViewModel;
    }
}
