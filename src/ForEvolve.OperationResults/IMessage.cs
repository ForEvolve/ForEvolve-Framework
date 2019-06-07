using System.Collections.Generic;

namespace ForEvolve.OperationResults
{
    /// <summary>
    /// Represents a generic operation result message.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets the severity associated with the message.
        /// </summary>
        /// <value>The severity.</value>
        OperationMessageLevel Severity { get; }

        /// <summary>
        /// Gets the message details.
        /// </summary>
        /// <value>The details.</value>
        IDictionary<string, object> Details { get; }

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The type of message.</value>
        string Type { get; }
    }
}
