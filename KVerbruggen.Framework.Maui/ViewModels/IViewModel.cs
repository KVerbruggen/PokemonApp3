using KVerbruggen.Framework.Models;

namespace KVerbruggen.Framework.Maui.ViewModels
{
    /// <summary>
    /// Interface for a view model
    /// </summary>
    /// <typeparam name="TModel">The view model's data model</typeparam>
    public interface IViewModel<TModel> : IViewModel
        where TModel : IModelBase
    {
        public new TModel Model { get; set; }
    }
}
