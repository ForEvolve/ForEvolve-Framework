using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace ForEvolve.EntityFrameworkCore.ValueConversion
{
    /// <summary>
    /// Defines conversions from a <typeparamref name="TObject"/> in a model to a JSON string in the store.
    /// Implements the <see cref="ValueConverter{TKey, TValue}" /></summary>
    /// <typeparam name="TObject">The type of the object to convert.</typeparam>
    /// <seealso cref="ValueConverter{TKey, TValue}" />
    public class ObjectToJsonConverter<TObject> : ValueConverter<TObject, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectToJsonConverter{TObject}"/> class.
        /// </summary>
        public ObjectToJsonConverter() : base(
            input => Serialize(input),
            input => Deserialize(input),
            null
        )
        { }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized <see cref="TObject"/> in the JSON format.</returns>
        public static string Serialize(TObject obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>The deserialized <see cref="TObject"/>.</returns>
        public static TObject Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<TObject>(json);
        }
    }
}
