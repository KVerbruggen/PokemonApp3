namespace KVerbruggen.Framework.Maui.Commands.Generic
{
    /// <summary>
    /// A command that executes an asynchronous function when called
    /// </summary>
    /// <typeparam name="TInput">The input type for the function</typeparam>
    public class AsyncCommand<TInput> : AsyncCommand
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AsyncCommand class
        /// </summary>
        /// <param name="execute">The function to execute when this command is invoked</param>
        /// <param name="canExecute">The function indicating whether this command can be executed</param>
        public AsyncCommand(Func<TInput, Task> execute, Func<object, Task<bool>> canExecute = null)
            : base(ExecuteDefinition(execute), canExecute ?? (_ => Task.FromResult(true)))
        { }

        #endregion

        #region Private methods

        /// <summary>
        /// The execution definition for this command
        /// </summary>
        /// <param name="execute">The function to execute</param>
        /// <returns>The function to execute as a function on 'object'</returns>
        private static Func<object, Task> ExecuteDefinition(Func<TInput, Task> execute) => new((parameter) => execute((TInput)parameter));

        #endregion
    }
}
