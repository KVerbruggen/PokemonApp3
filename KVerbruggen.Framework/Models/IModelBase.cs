using System.ComponentModel;

namespace KVerbruggen.Framework.Models
{
    /// <summary>
    /// A base implementation for all Models, implementing INotifyPropertyChanged and a custom async variant of IChangeTracking
    /// </summary>
    public interface IModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets a bool indicating whether the object has unsaved changes
        /// </summary>
        bool HasUnsavedChanges { get; }

        /// <summary>
        /// Gets or sets a bool indicating whether the view model is currently busy
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Cancel and wait for any currently running tasks
        /// </summary>
        void FinishTasks();

        /// <summary>
        /// Mark the object as changed
        /// </summary>
        void SetIsChanged();

        /// <summary>
        /// Indicate the unsaved changes have been accepted
        /// </summary>
        void AcceptChanges();
    }
}
