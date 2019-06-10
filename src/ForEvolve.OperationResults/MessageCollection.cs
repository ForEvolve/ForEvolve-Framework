using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.OperationResults
{
    /// <summary>
    /// Represents a collection of <see cref="IMessage" />.
    /// Implements the <see cref="System.Collections.Generic.IList{T}" /> where T is a <see cref="IMessage"/>.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IList{T}" />
    public class MessageCollection : IList<IMessage>
    {
        private readonly List<IMessage> _items = new List<IMessage>();

        /// <inheritdoc />
        public IMessage this[int index] { get => _items[index]; set => _items[index] = value; }

        /// <inheritdoc />
        public int Count => _items.Count;

        /// <inheritdoc />
        public bool IsReadOnly => ((IList<IMessage>)_items).IsReadOnly;

        /// <inheritdoc />
        public void Add(IMessage item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the current <see cref="ForEvolve.OperationResults.MessageCollection" />.
        /// </summary>
        /// <param name="collection">
        /// The collection whose elements should be added to the end of the <see cref="ForEvolve.OperationResults.MessageCollection" />.
        /// The collection itself cannot be null, but it can contain elements that are null.
        /// </param>
        /// <exception cref="System.ArgumentNullException">collection is null.</exception>
        public void AddRange(IEnumerable<IMessage> collection)
        {
            _items.AddRange(collection);
        }

        /// <inheritdoc />
        public void Clear()
        {
            _items.Clear();
        }

        /// <inheritdoc />
        public bool Contains(IMessage item)
        {
            return _items.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(IMessage[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public IEnumerator<IMessage> GetEnumerator()
        {
            return ((IList<IMessage>)_items).GetEnumerator();
        }

        /// <inheritdoc />
        public int IndexOf(IMessage item)
        {
            return _items.IndexOf(item);
        }

        /// <inheritdoc />
        public void Insert(int index, IMessage item)
        {
            _items.Insert(index, item);
        }

        /// <inheritdoc />
        public bool Remove(IMessage item)
        {
            return _items.Remove(item);
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<IMessage>)_items).GetEnumerator();
        }

        /// <summary>
        /// Determines whether this instance contains error messages.
        /// </summary>
        /// <returns><c>true</c> if this instance contains error messages; otherwise, <c>false</c>.</returns>
        public virtual bool HasError()
        {
            return HasLevel(OperationMessageLevel.Error);
        }

        /// <summary>
        /// Determines whether this instance contains warning messages.
        /// </summary>
        /// <returns><c>true</c> if this instance contains warning messages; otherwise, <c>false</c>.</returns>
        public virtual bool HasWarning()
        {
            return HasLevel(OperationMessageLevel.Warning);
        }

        /// <summary>
        /// Determines whether this instance contains information messages.
        /// </summary>
        /// <returns><c>true</c> if this instance contains information messages; otherwise, <c>false</c>.</returns>
        public virtual bool HasInformation()
        {
            return HasLevel(OperationMessageLevel.Information);
        }

        //
        // TODO: those next methods could maybe become extensions instead?
        //
        /// <summary>
        /// Determines whether this instance contains a message of type <typeparamref name="TMessage"/>.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to look for.</typeparam>
        /// <returns><c>true</c> if this instance contains a message of the specified type; otherwise, <c>false</c>.</returns>
        public bool Contains<TMessage>() 
            where TMessage : IMessage
        {
            return _items.Any(x => x is TMessage);
        }

        /// <summary>
        /// Gets the single message of type <typeparamref name="TMessage"/>.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to look for.</typeparam>
        /// <returns>The single message.</returns>
        public TMessage GetSingle<TMessage>()
            where TMessage : IMessage
        {
            return (TMessage)_items.Single(x => x is TMessage);
        }

        /// <summary>
        /// Gets the first message of type <typeparamref name="TMessage"/>.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to look for.</typeparam>
        /// <returns>The first message.</returns>
        public TMessage GetFirst<TMessage>()
            where TMessage : IMessage
        {
            return (TMessage)_items.First(x => x is TMessage);
        }

        /// <summary>
        /// Gets the last message of type <typeparamref name="TMessage"/>.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to look for.</typeparam>
        /// <returns>The last message.</returns>
        public TMessage GetLast<TMessage>()
            where TMessage : IMessage
        {
            return (TMessage)_items.Last(x => x is TMessage);
        }

        /// <summary>
        /// Gets all messages of type <typeparamref name="TMessage"/>.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to look for.</typeparam>
        /// <returns>The all messages.</returns>
        public IEnumerable<TMessage> GetAll<TMessage>()
            where TMessage : IMessage
        {
            return _items.Where(x => x is TMessage).Select(x => (TMessage)x);
        }

        private bool HasLevel(OperationMessageLevel level)
        {
            return _items.Any(x => x.Severity == level);
        }
    }

    /// <summary>
    /// Extensions to help handles OperationResults messages.
    /// </summary>
    public static class OperationResultsMessageExtensions
    {
        /// <summary>
        /// Filters the messages and returns only those that <see cref="IMessage.Details"/> are of the specified <typeparamref name="TMessageDetails"/> type.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="TMessageDetails"></typeparam>
        /// <param name="messages"></param>
        /// <returns>The messages that <see cref="IMessage.Details"/> are of the specified <typeparamref name="TMessageDetails"/> type.</returns>
        public static IEnumerable<TMessage> HavingDetailsOfType<TMessage, TMessageDetails>(this IEnumerable<TMessage> messages)
            where TMessage : IMessage
        {
            return messages.Where(x => x.Is<TMessageDetails>());
        }

        /// <summary>
        /// Determines whether this instance contains a message having its <see cref="IMessage.Details"/> of type <typeparamref name="TMessageDetails"/>.
        /// </summary>
        /// <typeparam name="TMessage">The message type that is inputted and outputted back.</typeparam>
        /// <typeparam name="TMessageDetails">The type of <see cref="IMessage.Details"/> to search for.</typeparam>
        /// <param name="messages"></param>
        /// <returns><c>true</c> if the messages instance contains at least a message having its <see cref="IMessage.Details"/> of the specified type; otherwise, <c>false</c>.</returns>
        public static bool ContainsDetails<TMessage, TMessageDetails>(this IEnumerable<TMessage> messages)
            where TMessage : IMessage
        {
            return messages.Any(x => x.Is<TMessageDetails>());
        }

        /// <summary>
        /// Filters the messages and returns their <see cref="IMessage.Details"/> that are of the specified <typeparamref name="TMessageDetails"/> type.
        /// </summary>
        /// <typeparam name="TMessageDetails">The type of <see cref="IMessage.Details"/> to search for.</typeparam>
        /// <param name="messages"></param>
        /// <returns>The filtered messages <see cref="IMessage.Details"/>.</returns>
        public static IEnumerable<TMessageDetails> HavingDetailsOfTypeAs<TMessageDetails>(this IEnumerable<IMessage> messages)
        {
            return messages
                .Where(x => x.Is<TMessageDetails>())
                .Select(x => x.As<TMessageDetails>());
        }

        /// <summary>
        /// Filters exception messages and returns their <see cref="ExceptionMessage.Exception"/> that are of the specified <typeparamref name="TException"/> type.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to search for.</typeparam>
        /// <param name="messages"></param>
        /// <returns>The filtered messages <see cref="ExceptionMessage.Exception"/>.</returns>
        public static IEnumerable<TException> GetExceptionsOfType<TException>(this MessageCollection messages)
            where TException : Exception
        {
            return messages
                .GetAll<ExceptionMessage>()
                .HavingDetailsOfTypeAs<TException>();
        }
    }
}
