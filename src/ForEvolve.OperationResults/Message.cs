using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ForEvolve.OperationResults
{
    /// <summary>
    /// Represents a generic operation result message.
    /// Implements the <see cref="ForEvolve.OperationResults.IMessage" />
    /// </summary>
    /// <seealso cref="ForEvolve.OperationResults.IMessage" />
    public class Message : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="severity">The severity.</param>
        public Message(OperationMessageLevel severity)
            : this(severity, new Dictionary<string, object>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="details">The details.</param>
        /// <exception cref="ArgumentNullException">details</exception>
        public Message(OperationMessageLevel severity, IDictionary<string, object> details)
        {
            Severity = severity;
            Details = details ?? throw new ArgumentNullException(nameof(details));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="severity">The message severity.</param>
        /// <param name="details">The message details that will be loaded in the <see cref="System.Collections.Generic.IDictionary{string, object}"/>.</param>
        /// <param name="ignoreNull">if set to <c>true</c> null properties will be ignored (not added in the <see cref="System.Collections.Generic.IDictionary{string, object}"/>).</param>
        /// <exception cref="ArgumentNullException">details</exception>
        public Message(OperationMessageLevel severity, object details, bool ignoreNull = true)
            : this(severity)
        {
            if (details == null) { throw new ArgumentNullException(nameof(details)); }
            LoadDetails(details, ignoreNull);
        }

        private void LoadDetails(object details, bool ignoreNull)
        {
            var properties = TypeDescriptor.GetProperties(details);
            foreach (PropertyDescriptor property in properties)
            {
                var value = property.GetValue(details);
                if (!ignoreNull || value != null)
                {
                    Details.Add(property.Name, value);
                }
            }
        }

        /// <inheritdoc />
        public virtual OperationMessageLevel Severity { get; }

        /// <inheritdoc />
        public virtual IDictionary<string, object> Details { get; }
    }
}
