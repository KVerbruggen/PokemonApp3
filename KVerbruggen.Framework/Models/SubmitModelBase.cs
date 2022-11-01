namespace KVerbruggen.Framework.Models
{
    /// <summary>
    /// A default implementation for a model that can edit items
    /// </summary>
    public abstract class SubmitModelBase : DetailsModelBase, ISubmitModelBase
    {
        #region Events

        /// <summary>
        /// Event thrown when submitting was successful
        /// </summary>
        public event EventHandler? SubmitSuccessful;

        /// <summary>
        /// Event thrown to indicate editing the model has finished
        /// </summary>
        public event EventHandler? Finished;

        /// <summary>
        /// Event thrown when submitting failed
        /// </summary>
        public event EventHandler<Events.ErrorEventArgs>? SubmitError;

        #endregion

        #region Protected methods

        /// <summary>
        /// Invoke the SubmitError event
        /// </summary>
        /// <param name="errorEventArguments">The event arguments to provide</param>
        protected void InvokeSubmitError(Events.ErrorEventArgs errorEventArguments)
        {
            ExecuteSynchronized(() => SubmitError?.Invoke(this, errorEventArguments));
        }

        /// <summary>
        /// Invoke the SubmitSuccessful event
        /// </summary>
        /// <param name="errorEventArguments">The event arguments to provide</param>
        protected void InvokeSubmitSuccessful()
        {
            ExecuteSynchronized(() => SubmitSuccessful?.Invoke(this, new EventArgs()));
        }

        /// <summary>
        /// The method used to save changes to the model
        /// </summary>
        /// <returns>The Key of the edited/created item</returns>
        protected abstract Task<Guid> SubmitChangesImplementation();

        #endregion

        #region Public methods

        /// <summary>
        /// Submit the view model
        /// </summary>
        /// <returns>A Task representing the execution of the action</returns>
        public async Task<bool> SubmitChanges()
        {
            bool saveSuccessful;

            try
            {
                Guid key = await SubmitChangesImplementation();

                saveSuccessful = true;
                Key = key;

                AcceptChanges();
                InvokeSubmitSuccessful();
            }
            catch (Exception exception)
            {
                saveSuccessful = false;

                Events.ErrorEventArgs errorEventArguments = new(exception.Message);

                InvokeSubmitError(errorEventArguments);
            }
            finally
            {
                IsBusy = false;
            }

            return saveSuccessful;
        }

        /// <summary>
        /// Indicate editing the model has finished
        /// </summary>
        public void Finish()
        {
            ExecuteSynchronized(() => Finished?.Invoke(this, new EventArgs()));
        }

        #endregion
    }
}
