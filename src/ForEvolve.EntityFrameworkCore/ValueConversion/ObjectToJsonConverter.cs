using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.EntityFrameworkCore.ValueConversion
{
    /// <summary>
    /// Represent an EF Core ValueConverter that convert <see cref="object"/> to and from Json
    /// and vice versa.
    /// Implements the <see cref="ValueConverter{TKey, TValue}" />
    /// </summary>
    /// <seealso cref="ValueConverter{TKey, TValue}" />
    public class ObjectToJsonConverter : ValueConverter<object, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectToJsonConverter"/> class.
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
        /// <returns>The serialized <see cref="object"/> in the JSON format.</returns>
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>The deserialized <see cref="object"/>.</returns>
        public static object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
    }
}
