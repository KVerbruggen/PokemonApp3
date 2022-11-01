namespace KVerbruggen.Framework.Models
{
    /// <summary>
    /// An interface describing a base class for an editor model
    /// </summary>
    public interface IDetailsModelBase : IModelBase
    {
        /// <summary>
        /// The key of the item to load
        /// </summary>
        Guid Key { get; set; }

        /// <summary>
        /// Refresh the details of the item
        /// </summary>
        /// <returns>A task representing the action</returns>
        Task LoadDetails();

        /// <summary>
        /// Event thrown when loading the model has failed
        /// </summary>
        event EventHandler<Events.ErrorEventArgs>? LoadError;
    }
}
