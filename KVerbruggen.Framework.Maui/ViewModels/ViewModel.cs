using KVerbruggen.Framework.Models;

namespace KVerbruggen.Framework.Maui.ViewModels
{
    /// <summary>
    /// Interface for a view model
    /// </summary>
    /// <typeparam name="TModel">The view model's data model</typeparam>
    public class ViewModel<TModel> : IViewModel<TModel>
        where TModel : class, IModelBase
    {
        public TModel Model
        {
            get
            {
                return (this as IViewModel).Model as TModel;
            }
            set
            {
                (this as IViewModel<TModel>).Model = value;
            }
        }

        object IViewModel.Model => Model;

        public ViewModel(TModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}
