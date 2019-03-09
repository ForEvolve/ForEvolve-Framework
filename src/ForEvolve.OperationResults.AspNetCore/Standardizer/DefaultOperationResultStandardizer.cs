using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ForEvolve.OperationResults.Standardizer
{
    /// <summary>
    /// Represents the default <see cref="IOperationResult"/> standardizer.
    /// Implements the <see cref="ForEvolve.OperationResults.IOperationResultStandardizer" />
    /// </summary>
    /// <seealso cref="ForEvolve.OperationResults.IOperationResultStandardizer" />
    public class DefaultOperationResultStandardizer : IOperationResultStandardizer
    {
        private readonly IPropertyNameFormatter _propertyNameFormatter;
        private readonly IPropertyValueFormatter _propertyValueFormatter;
        private readonly DefaultOperationResultStandardizerOptions _options;

        public DefaultOperationResultStandardizer(
            IPropertyNameFormatter propertyNameFormatter, 
            IPropertyValueFormatter propertyValueFormatter, 
            IOptionsMonitor<DefaultOperationResultStandardizerOptions> options)
        {
            _propertyNameFormatter = propertyNameFormatter ?? throw new ArgumentNullException(nameof(propertyNameFormatter));
            _propertyValueFormatter = propertyValueFormatter ?? throw new ArgumentNullException(nameof(propertyValueFormatter));
            _options = options.CurrentValue ?? throw new ArgumentNullException(nameof(options));
        }

        public object Standardize(IOperationResult operationResult)
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add(_options.OperationName, new
            {
                operationResult.Messages,
                operationResult.Succeeded
            });

            try
            {
                var result = operationResult as dynamic;
                var value = result.Value;
                if (value != null)
                {
                    foreach (var property in (value as object).GetType().GetProperties())
                    {
                        var formattedName = _propertyNameFormatter.Format(property.Name);
                        var formattedValue = _propertyValueFormatter.Format(property.GetValue(value));
                        dictionary.Add(formattedName, formattedValue);
                    }
                }
            }
            catch (RuntimeBinderException) { }
            return dictionary;
        }
    }
}
