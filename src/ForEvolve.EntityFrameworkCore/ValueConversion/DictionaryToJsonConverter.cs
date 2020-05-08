using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
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
        public DictionaryToJsonConverter() : base(
            input => Serialize(input),
            input => Deserialize(input),
            null
        )
        { }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized <see cref="Dictionary{TKey, TValue}"/> in the JSON format.</returns>
        public static string Serialize(Dictionary<string, object> obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>The deserialized <see cref="Dictionary{TKey, TValue}"/>.</returns>
        public static Dictionary<string, object> Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
    }

}
