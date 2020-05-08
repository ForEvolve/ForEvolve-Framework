using System;
using System.Text.Json.Serialization;

namespace ForEvolve.OperationResults
{
    /// <summary>
    /// Represents a wrapper message around an <see cref="Exception"/>.
    /// </summary>
    public class ExceptionMessage : ProblemDetailsMessage
    {
        /// <summary>
        /// Get the exception represented by this message.
        /// </summary>
        [JsonIgnore]
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessage"/> class.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> that represents the message.</param>
        public ExceptionMessage(Exception exception)
            : base(OperationMessageLevel.Error)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
            LoadProblemDetails(new ProblemDetails
            {
                Title = exception.GetType().Name,
                Detail = exception.Message
            });
        }

        /// <inheritdoc />
        [JsonIgnore]
        public override Type Type => Exception.GetType();

        /// <inheritdoc />
        [JsonIgnore]
        public override object OriginalObject => Exception;
    }
}
