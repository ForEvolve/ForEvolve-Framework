using System;
namespace ForEvolve.OperationResults.Standardizer
{
    /// <summary>
    /// Represents the default property name formatter, used by <see cref="DefaultOperationResultStandardizer"/>.
    /// Implements the <see cref="IoTCore.Api.Devices.IPropertyNameFormatter" />
    /// </summary>
    /// <seealso cref="IoTCore.Api.Devices.IPropertyNameFormatter" />
    public class DefaultPropertyNameFormatter : IPropertyNameFormatter
    {
        /// <inheritdoc />
        public string Format(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentNullException(nameof(name)); }

            var firstChar = name.Substring(0, 1).ToLowerInvariant();
            var rest = name.Substring(1);
            return firstChar + rest;
        }
    }
}
