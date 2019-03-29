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
    /// Represent an EF Core ValueConverter that convert <see cref="object"/> to Json
    /// and vice versa.
    /// Implements the <see cref="ValueConverter{TKey, TValue}" />
    /// </summary>
    /// <seealso cref="ValueConverter{TKey, TValue}" />
    public class ObjectToJsonConverter<TObject> : ValueConverter<TObject, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectToJsonConverter{TObject}"/> class.
        /// </summary>
        public ObjectToJsonConverter() : base(
            input => SerializeObject(input),
            input => DeserializeObject(input),
            null
        )
        { }

        /// <summary>
        /// Serializes the object to JSON.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.String.</returns>
        private static string SerializeObject(TObject obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserializes the object from JSON.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        private static TObject DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<TObject>(json);
        }
    }
}
