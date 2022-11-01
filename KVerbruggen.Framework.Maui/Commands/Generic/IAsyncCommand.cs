using System.Windows.Input;

namespace KVerbruggen.Framework.Maui.Commands.Generic
{
    /// <summary>
    /// A command that executes an asynchronous function when called
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Execute the function for this command
        /// </summary>
        /// <param name="parameter">The input for the command</param>
        /// <returns>A Task representing the action</returns>
        Task ExecuteAsync(object parameter);

        /// <summary>
        /// Check whether this command is allowed to execute
        /// </summary>
        /// <returns>A bool indicating whether this command is allowed to execute</returns>
        Task<bool> CanExecuteAsync(object parameter);

        /// <summary>
        /// Throw an event to indicate the value of 'CanExecute' changed
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1030:Use events where appropriate", Justification = "The point of this method is to be able to call the actual event in a public context")]
        void RaiseCanExecuteChanged();
    }
}
