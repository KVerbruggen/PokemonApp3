namespace KVerbruggen.Framework.Models
{
    /// <summary>
    /// An interface describing a model that can edit items
    /// </summary>
    public interface ISubmitModelBase
    {
        /// <summary>
        /// Event thrown when submitting the data was successful
        /// </summary>
        event EventHandler? SubmitSuccessful;

        /// <summary>
        /// Event thrown to indicate editing the model has finished
        /// </summary>
        event EventHandler? Finished;

        /// <summary>
        /// Event thrown when submitting the data failed
        /// </summary>
        event EventHandler<Events.ErrorEventArgs>? SubmitError;

        /// <summary>
        /// Submit the model
        /// </summary>
        /// <returns>A bool indicating whether the submit was successful</returns>
        Task<bool> SubmitChanges();
    }
}
