﻿namespace ForEvolve.OperationResults
{
    /// <summary>
    /// Represents an operation result containing optional messages, generated by the operation.
    /// Implements the <see cref="ForEvolve.OperationResults.IOperationResult" />
    /// </summary>
    /// <seealso cref="ForEvolve.OperationResults.IOperationResult" />
    public class OperationResult : IOperationResult
    {
        /// <inheritdoc />
        public bool Succeeded => !Messages.HasError();

        /// <inheritdoc />
        public MessageCollection Messages { get; } = new MessageCollection();

        /// <inheritdoc />
        public bool HasMessages()
        {
            return Messages.Count > 0;
        }
    }

    /// <summary>
    /// Represents an operation result containing optional messages, generated by the operation, and an optional resulting object.
    /// Implements the <see cref="ForEvolve.OperationResults.OperationResult" />
    /// Implements the <see cref="ForEvolve.OperationResults.IOperationResult{TValue}" />
    /// </summary>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <seealso cref="ForEvolve.OperationResults.OperationResult" />
    /// <seealso cref="ForEvolve.OperationResults.IOperationResult{TValue}" />
    public class OperationResult<TValue> : OperationResult, IOperationResult<TValue>
    {
        /// <inheritdoc />
        public TValue Value { get; set; }

        /// <inheritdoc />
        public bool HasValue()
        {
            return Value != null;
        }
    }
}