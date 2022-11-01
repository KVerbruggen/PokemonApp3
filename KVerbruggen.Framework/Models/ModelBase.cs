using KVerbruggen.Framework.Services.SynchronizationHelper;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KVerbruggen.Framework.Models
{
    /// <summary>
    /// A base implementation for all ViewModels, implementing PropertyChanged notifications
    /// </summary>
    public abstract class ModelBase : IModelBase, IDisposable
    {
        #region Private declarations

        /// <summary>
        /// Gets a value indicating whether the object has uncommitted changes
        /// </summary>
        private bool _hasUnsavedChanges;

        /// <summary>
        /// Bool indicating whether the view model is currently busy
        /// </summary>
        private bool _isBusy;

        /// <summary>
        /// A list of child view models managed by the current model, including a null check
        /// </summary>
        private IEnumerable<IModelBase> _childModels => ChildModels.Where(item => item != null);

        /// <summary>
        /// A list of child view models managed by the current model - Can be empty
        /// </summary>
        protected abstract IEnumerable<IModelBase> ChildModels { get; }

        #endregion

        #region Public declarations

        /// <summary>
        /// An event that will fire whenever a property's value changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets a value indicating whether the object has uncommitted changes
        /// </summary>
        public virtual bool HasUnsavedChanges
        {
            get => _hasUnsavedChanges || ChildModels.Any(childModel => (childModel?.HasUnsavedChanges) == true); // childModel is not null and HasUnsavedChanges
            protected set => SetProperty(ref _hasUnsavedChanges, value, countAsUnsavedChange: false);
        }

        /// <summary>
        /// Gets or sets a bool indicating whether the view model is currently busy
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            protected set => SetProperty(ref _isBusy, value, countAsUnsavedChange: false);
        }

        #endregion

        #region Protected declarations

        /// <summary>
        /// The cancellation token source for the currently running tasks
        /// </summary>
        protected CancellationTokenSource CancellationTokenSource { get; }

        #endregion

        #region Constructor

        /// Initialize an instance of the ViewModelBase class
        /// </summary>
        protected ModelBase()
        {
            CancellationTokenSource = new CancellationTokenSource();
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Set a new value for a property and fire a PropertyChanged event for the property.
        /// </summary>
        /// <typeparam name="TProperty">The property type</typeparam>
        /// <param name="backingField">The backing field of the property to update</param>
        /// <param name="newValue">The new value of the property</param>
        /// <param name="countAsUnsavedChange">A bool indicating whether the property should update the value of 'HasUnsavedChanges'</param>
        /// <param name="propertyName">The name of the changed property</param>
        protected void SetProperty<TProperty>(ref TProperty backingField, TProperty newValue, bool countAsUnsavedChange, [CallerMemberName] string? propertyName = null)
        {
            if ((backingField == null && newValue != null) ||
                (backingField != null && !backingField.Equals(newValue)))
            {
                backingField = newValue;
                InvokePropertyChanged(propertyName ?? string.Empty, countAsUnsavedChange);
            }
        }

        /// <summary>
        /// Invoke the PropertyChanged event for a specific property
        /// </summary>
        /// <param name="propertyName">The name of the property to invoke the event for</param>
        /// <param name="countAsUnsavedChange">A bool indicating whether the property should update the value of 'HasUnsavedChanges'</param>
        protected void InvokePropertyChanged(string propertyName, bool countAsUnsavedChange)
        {
            if (countAsUnsavedChange && !HasUnsavedChanges && !propertyName.Equals(nameof(HasUnsavedChanges)))
                HasUnsavedChanges = true;

            ExecuteSynchronized(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }

        /// <summary>
        /// Execute an action on the calling app's synchronization context
        /// </summary>
        /// <param name="actionToExecute">The action to execute on the synchronization context</param>
        protected void ExecuteSynchronized(Action actionToExecute) => SynchronizationHelper.Instance.ExecuteSynchronized(actionToExecute);

        /// <summary>
        /// Execute an action on the calling app's synchronization context and wait for it to complete
        /// </summary>
        /// <param name="actionToExecute">The action to execute on the synchronization context</param>
        protected void ExecuteSynchronizedAndWait(Action actionToExecute) => SynchronizationHelper.Instance.ExecuteSynchronizedAndWait(actionToExecute);

        #endregion

        #region Public methods

        /// <summary>
        /// Cancel and wait for any currently running tasks
        /// </summary>
        public void FinishTasks()
        {
            CancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Mark the object as changed
        /// </summary>
        public void SetIsChanged()
        {
            HasUnsavedChanges = true;
        }

        /// <summary>
        /// Indicate the unsaved changes have been accepted
        /// </summary>
        public void AcceptChanges()
        {
            HasUnsavedChanges = false;
            if (ChildModels != null)
            {
                foreach (IModelBase modelBase in _childModels)
                    modelBase.AcceptChanges();
            }
        }

        #endregion

        #region Dispose pattern + deconstructor

        /// <summary>
        /// Dispose pattern
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose pattern
        /// </summary>
        /// <param name="disposing">A bool indicating whether to release unmanaged resources</param>
        protected virtual void Dispose(bool disposing)
        {
            FinishTasks();

            if (disposing)
            { }

            CancellationTokenSource.Dispose();
        }

        /// <summary>
        /// Deconstructs the <see cref="ViewModelBase"/> instance
        /// </summary>
        ~ModelBase()
        {
            Dispose(false);
        }

        #endregion
    }
}
