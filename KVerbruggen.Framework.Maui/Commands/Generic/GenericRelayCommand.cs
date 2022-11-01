namespace KVerbruggen.Framework.Maui.Commands.Generic
{
    /// <summary>
    /// A command to execute an action
    /// </summary>
    /// <typeparam name="TInput">The action input</typeparam>
    public class RelayCommand<TInput> : RelayCommand
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AsyncCommand class
        /// </summary>
        /// <param name="execute">The function to execute when this command is invoked</param>
        /// <param name="canExecute">The function indicating whether this command can be executed - If null, CanExecute will always return true</param>
        public RelayCommand(
            Action<TInput> execute,
            Func<object, bool> canExecute = null)
            : base((parameter) => execute((TInput)parameter), canExecute)
        { }

        #endregion
    }
}
