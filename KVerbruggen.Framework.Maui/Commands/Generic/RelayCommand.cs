using KVerbruggen.Framework.Services.SynchronizationHelper;
using System.Windows.Input;

namespace KVerbruggen.Framework.Maui.Commands.Generic
{
    /// <summary>
    /// A command to execute a provided action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private declarations

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        #endregion

        #region Public declarations

        /// <summary>
        /// An event that fires when the value of 'CanExecute' changes - Has to be called manually
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the RelayCommand class
        /// </summary>
        /// <param name="execute">The function to execute when this command is invoked</param>
        /// <param name="canExecute">The function indicating whether this command can be executed - If null, CanExecute will always return true</param>
        public RelayCommand(
            Action<object> execute,
            Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Execute the command action
        /// </summary>
        /// <param name="parameter">The parameter for the action</param>
        public void Execute(object parameter) => _execute?.Invoke(parameter);

        /// <summary>
        /// Check whether this command can execute
        /// </summary>
        /// <param name="parameter">The canExecute function parameter</param>
        /// <returns>A bool indicating whether this command can execute</returns>
        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Throw an event to indicate the value of 'CanExecute' changed
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1030:Use events where appropriate", Justification = "The point of this method is to be able to call the actual event in a public context")]
        public void RaiseCanExecuteChanged()
        {
            void raiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            SynchronizationHelper.Instance.ExecuteSynchronized(raiseCanExecuteChanged);
        }

        #endregion
    }
}
