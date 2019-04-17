using Newtonsoft.Json;
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

        protected void LoadDetails(object details, bool ignoreNull)
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

    /// <summary>
    /// Represents an operation result message build around [RFC3986] <see cref="OperationResults.ProblemDetails"/>.
    /// Implements the <see cref="ForEvolve.OperationResults.Message" />
    /// </summary>
    /// <seealso cref="ForEvolve.OperationResults.Message" />
    public class ProblemDetailsMessage : Message
    {
        /// <summary>
        /// Gets the problem details.
        /// </summary>
        /// <value>The problem details.</value>
        public ProblemDetails ProblemDetails { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDetailsMessage"/> class.
        /// </summary>
        /// <param name="problemDetails">The problem details.</param>
        /// <param name="severity">The severity.</param>
        /// <exception cref="ArgumentNullException">problemDetails</exception>
        public ProblemDetailsMessage(ProblemDetails problemDetails, OperationMessageLevel severity)
            : base(severity)
        {
            ProblemDetails = problemDetails ?? throw new ArgumentNullException(nameof(problemDetails));
            LoadProblemDetails(problemDetails);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDetailsMessage"/> class.
        /// Sub-classes must manually call the <see cref="LoadProblemDetails"/> method.
        /// </summary>
        /// <param name="severity">The severity.</param>
        protected ProblemDetailsMessage(OperationMessageLevel severity)
            : base(severity)
        {
        }

        /// <summary>
        /// Loads the specified problem details into the <see cref="Details"/> dictionary.
        /// </summary>
        /// <param name="problemDetails">The problem details to load.</param>
        protected void LoadProblemDetails(ProblemDetails problemDetails)
        {
            if (problemDetails.Type != null)
            {
                Details.Add(nameof(problemDetails.Type).ToLowerInvariant(), problemDetails.Type);
            }
            if (problemDetails.Title != null)
            {
                Details.Add(nameof(problemDetails.Title).ToLowerInvariant(), problemDetails.Title);
            }
            if (problemDetails.Status != null)
            {
                Details.Add(nameof(problemDetails.Status).ToLowerInvariant(), problemDetails.Status);
            }
            if (problemDetails.Detail != null)
            {
                Details.Add(nameof(problemDetails.Detail).ToLowerInvariant(), problemDetails.Detail);
            }
            if (problemDetails.Instance != null)
            {
                Details.Add(nameof(problemDetails.Instance).ToLowerInvariant(), problemDetails.Instance);
            }
            foreach (var item in problemDetails.Extensions)
            {
                Details.Add(item);
            }
        }
    }

    public class ExceptionMessage : ProblemDetailsMessage
    {
        [JsonIgnore]
        public Exception Exception { get; }

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
    }


    /// <summary>
    /// A machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807.
    /// </summary>
    public class ProblemDetails
    {
        /// <summary>
        /// A URI reference [RFC3986] that identifies the problem type. This specification encourages that, when
        /// dereferenced, it provide human-readable documentation for the problem type
        /// (e.g., using HTML [W3C.REC-html5-20141028]).  When this member is not present, its value is assumed to be
        /// "about:blank".
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// A short, human-readable summary of the problem type.It SHOULD NOT change from occurrence to occurrence
        /// of the problem, except for purposes of localization(e.g., using proactive content negotiation;
        /// see[RFC7231], Section 3.4).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The HTTP status code([RFC7231], Section 6) generated by the origin server for this occurrence of the problem.
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// A URI reference that identifies the specific occurrence of the problem.It may or may not yield further information if dereferenced.
        /// </summary>
        public string Instance { get; set; }

        /// <summary>
        /// Gets the <see cref="IDictionary{TKey, TValue}"/> for extension members.
        /// <para>
        /// Problem type definitions MAY extend the problem details object with additional members. Extension members appear in the same namespace as
        /// other members of a problem type.
        /// </para>
        /// </summary>
        /// <remarks>
        /// The round-tripping behavior for <see cref="Extensions"/> is determined by the implementation of the Input \ Output formatters.
        /// In particular, complex types or collection types may not round-trip to the original type when using the built-in JSON or XML formatters.
        /// </remarks>
        public IDictionary<string, object> Extensions { get; } = new Dictionary<string, object>(StringComparer.Ordinal);
    }
}
