namespace KVerbruggen.Framework.Models
{
    /// <summary>
    /// A generic view model that can load details about an item by key
    /// </summary>
    public abstract class DetailsModelBase : ModelBase, IDetailsModelBase
    {
        #region Private declarations

        /// <summary>
        /// The entity identifier
        /// </summary>
        private Guid _key;

        #endregion

        #region Public properties

        /// <summary>
        /// The entity identifier
        /// </summary>
        public Guid Key
        {
            get => _key;
            set
            {
                _key = value;
                InvokePropertyChanged(nameof(Key), countAsUnsavedChange: false);
                LoadDetails().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the object has uncommitted changes
        /// </summary>
        public override bool HasUnsavedChanges
        {
            get => base.HasUnsavedChanges || Key == Guid.Empty;
            protected set => base.HasUnsavedChanges = value;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event thrown when loading the model has failed
        /// </summary>
        public event EventHandler<Events.ErrorEventArgs>? LoadError;

        #endregion

        #region Protected methods

        /// <summary>
        /// Invoke the LoadError event
        /// </summary>
        /// <param name="errorEventArguments">The event arguments to provide</param>
        protected void InvokeLoadError(Events.ErrorEventArgs errorEventArguments)
        {
            ExecuteSynchronized(() => LoadError?.Invoke(this, errorEventArguments));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Load the details of the item
        /// </summary>
        /// <returns>A task representing the action</returns>
        public async Task LoadDetails()
        {
            IsBusy = true;

            try
            {
                await LoadDetailsInternal();
            }
            catch (Exception exception)
            {
                Events.ErrorEventArgs errorEventArguments = new Events.ErrorEventArgs(exception.Message);
                InvokeLoadError(errorEventArguments);
            }
            finally
            {
                IsBusy = false;
            }

            AcceptChanges();
        }

        /// <summary>
        /// Load the details of the item
        /// </summary>
        /// <returns>A task representing the action</returns>
        protected abstract Task LoadDetailsInternal();

        #endregion
    }
}
