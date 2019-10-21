using System;
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
        Type Type { get; }

        /// <summary>
        /// Validate if the <see cref="Type"/> value match the <typeparamref name="TType"/>.
        /// </summary>
        /// <typeparam name="TType">The type to validate.</typeparam>
        /// <returns><c>true</c> is the <see cref="Type"/> matches <typeparamref name="TType"/>; otherwise <c>false</c>.</returns>
        bool Is<TType>();

        /// <summary>
        /// Validate if the <see cref="Type"/> value match the specified type.
        /// </summary>
        /// <param name="type">The type to validate.</param>
        /// <returns><c>true</c> is the <see cref="Type"/> matches the specified type; otherwise <c>false</c>.</returns>
        bool Is(Type type);

        /// <summary>
        /// Convert the <see cref="Details"/> to the specified type, assuming they are compatible.
        /// </summary>
        /// <typeparam name="TType">The type of the expected object to return.</typeparam>
        /// <returns>The converted object.</returns>
        /// <exception cref="TypeMismatchException"></exception>
        TType As<TType>();

        /// <summary>
        /// Convert the <see cref="Details"/> to the specified type, assuming they are compatible.
        /// </summary>
        /// <param name="type">The type of the expected object to return.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="TypeMismatchException"></exception>
        object As(Type type);
    }
}
