using KVerbruggen.Framework.Services.SynchronizationHelper;

namespace KVerbruggen.Framework.Maui.Commands.Generic
{
    /// <summary>
    /// A command that executes an asynchronous function when called
    /// </summary>
    public class AsyncCommand : IAsyncCommand
    {
        #region Private declarations

        private bool _isExecuting;
        private readonly Func<object, Task> _execute;
        private readonly Func<object, Task<bool>> _canExecute;

        #endregion

        #region Public declarations

        /// <summary>
        /// An event that fires when the value of 'CanExecute' changes - Has to be called manually
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the AsyncCommand class
        /// </summary>
        /// <param name="execute">The function to execute when this command is invoked</param>
        /// <param name="canExecute">The function indicating whether this command can be executed</param>
        public AsyncCommand(
            Func<object, Task> execute,
            Func<object, Task<bool>> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the AsyncCommand class
        /// </summary>
        /// <param name="execute">The function to execute when this command is invoked</param>
        /// <param name="canExecute">The function indicating whether this command can be executed</param>
        public AsyncCommand(
            Func<object, Task> execute,
            Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = (parameter) => Task.FromResult(canExecute == null || canExecute(parameter));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Check whether this command is allowed to execute
        /// </summary>
        /// <returns>A bool indicating whether this command is allowed to execute</returns>
        public virtual async Task<bool> CanExecuteAsync(object parameter) =>
            !_isExecuting && (_canExecute == null || await _canExecute(parameter).ConfigureAwait(true));

        /// <summary>
        /// Execute the function for this command
        /// </summary>
        /// <param name="parameter">The input for the command</param>
        /// <returns>A Task representing the action</returns>
        public virtual async Task ExecuteAsync(object parameter)
        {
            if (await CanExecuteAsync(parameter).ConfigureAwait(false))
            {
                _isExecuting = true;
                RaiseCanExecuteChanged();
                try
                {
                    await _execute(parameter).ConfigureAwait(false);
                }
                finally
                {
                    _isExecuting = false;
                    RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Throw an event to indicate the value of 'CanExecute' changed
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            void raiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            SynchronizationHelper.Instance.ExecuteSynchronized(raiseCanExecuteChanged);
        }

        #endregion

        #region Explicit implementations

        /// <summary>
        /// The implementation of ICommand.CanExecute.
        /// This executes the asynchronous variant of CanExecute synchronously
        /// </summary>
        /// <param name="parameter">The parameter for the implementation of CanExecute</param>
        /// <returns>A bool indicating whether this command is allowed to execute</returns>
        public bool CanExecute(object parameter) =>
            CanExecuteAsync(parameter)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        /// <summary>
        /// The implementation of ICommand.Execute
        /// Warning: This invokes the asynchronous variant of ExecuteAsync, without awaiting the result
        /// </summary>
        /// <param name="parameter">The parameter for the implementation of Execute</param>
        public void Execute(object parameter) => ExecuteAsync(parameter).ConfigureAwait(false); // Deliberately do NOT await the execution, to not block any calling (UI) threads

        #endregion
    }
}
