using System.Text;

namespace KVerbruggen.Framework.Events
{
    /// <summary>
    /// Arguments for an error event thrown by a model, to show an error message in the view
    /// </summary>
    public class ErrorEventArgs
    {
        /// <summary>
        /// The error message
        /// </summary>
        public IReadOnlyCollection<string> Messages { get; }

        /// <summary>
        /// Initializes a new instance of the ErrorEventArgs class
        /// </summary>
        /// <param name="message">The error message</param>
        public ErrorEventArgs(string message)
        {
            Messages = new[] { message };
        }

        /// <summary>
        /// Initializes a new instance of the ErrorEventArgs class
        /// </summary>
        /// <param name="messages">The error messages</param>
        public ErrorEventArgs(IEnumerable<string> messages)
        {
            Messages = Array.AsReadOnly(messages.ToArray());
        }

        /// <summary>
        /// Convert the error messages to a single multi-line string
        /// </summary>
        /// <returns></returns>
        public string ToMultilineString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string message in Messages)
                stringBuilder.AppendLine(message);

            return stringBuilder.ToString();
        }
    }
}
