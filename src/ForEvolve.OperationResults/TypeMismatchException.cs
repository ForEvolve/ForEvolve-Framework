using System;

namespace ForEvolve.OperationResults
{
    /// <summary>
    /// The exception that is thrown when the <see cref="IMessage.Details"/> is not loadable as the specified type.
    /// </summary>
    public class TypeMismatchException : TypeLoadException
    {
        public TypeMismatchException(IMessage sourceMessage, Type type)
            : base($"Type mismatch; cannot convert '{sourceMessage?.Type?.Name ?? "null"}' to '{type?.Name ?? "null"}'.")
        {
            SourceMessage = sourceMessage ?? throw new ArgumentNullException(nameof(sourceMessage));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Gets the source message that generated the exception.
        /// </summary>
        public IMessage SourceMessage { get; }

        /// <summary>
        /// Gets the type that the message was supposed to be converted into.
        /// </summary>
        public Type Type { get; }
    }
}
