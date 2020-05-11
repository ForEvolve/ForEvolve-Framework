using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ForEvolve.EntityFrameworkCore.ValueConversion
{
    /// <summary>
    /// Represent an EF Core ValueConverter to convert <see cref="Dictionary{TKey, TValue}"/> to and from Json.
    /// Implements the <see cref="ValueConverter{TModel, TProvider}" />
    /// </summary>
    /// <seealso cref="ValueConverter{TModel, TProvider}" />
    public class DictionaryToJsonConverter : ValueConverter<Dictionary<string, object>, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryToJsonConverter"/> class.
        /// </summary>
        public DictionaryToJsonConverter(JsonSerializerOptions options = null) : base(
            input => Serialize(input, options),
            input => Deserialize(input, options),
            null
        )
        { }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized <see cref="Dictionary{TKey, TValue}"/> in the JSON format.</returns>
        public static string Serialize(Dictionary<string, object> obj, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Serialize(obj, options);
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>The deserialized <see cref="Dictionary{TKey, TValue}"/>.</returns>
        public static Dictionary<string, object> Deserialize(string json, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);
        }
    }

}
