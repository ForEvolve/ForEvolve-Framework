using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForEvolve.OperationResults.Standardizer
{
    /// <summary>
    /// Represents the default <see cref="IOperationResult"/> standardizer.
    /// Implements the <see cref="IOperationResultStandardizer" />
    /// </summary>
    /// <seealso cref="IOperationResultStandardizer" />
    public class DefaultOperationResultStandardizer : IOperationResultStandardizer
    {
        private readonly IPropertyNameFormatter _propertyNameFormatter;
        private readonly IPropertyValueFormatter _propertyValueFormatter;
        private readonly DefaultOperationResultStandardizerOptions _options;
        private readonly ILogger<DefaultOperationResultStandardizer> _logger;

        public DefaultOperationResultStandardizer(
            IPropertyNameFormatter propertyNameFormatter, 
            IPropertyValueFormatter propertyValueFormatter, 
            IOptionsMonitor<DefaultOperationResultStandardizerOptions> options,
            ILogger<DefaultOperationResultStandardizer> logger)
        {
            _propertyNameFormatter = propertyNameFormatter ?? throw new ArgumentNullException(nameof(propertyNameFormatter));
            _propertyValueFormatter = propertyValueFormatter ?? throw new ArgumentNullException(nameof(propertyValueFormatter));
            _options = options.CurrentValue ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public object Standardize(IOperationResult operationResult)
        {
            if (operationResult == null) { throw new ArgumentNullException(nameof(operationResult)); }

            var dictionary = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(_options.OperationName))
            {
                dictionary.Add(_options.OperationName, new
                {
                    operationResult.Messages,
                    operationResult.Succeeded
                });
            }
            try
            {
                AddValueToDictionary(operationResult, dictionary);
            }
            catch (RuntimeBinderException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return dictionary;
        }

        private void AddValueToDictionary(IOperationResult operationResult, Dictionary<string, object> dictionary)
        {
            var value = FindValueProperty(operationResult);
            if (value != null)
            {
                foreach (var property in value.GetType().GetProperties())
                {
                    var formattedName = _propertyNameFormatter.Format(property.Name);
                    var formattedValue = _propertyValueFormatter.Format(property.GetValue(value));
                    dictionary.Add(formattedName, formattedValue);
                }
            }
        }

        private static object FindValueProperty(IOperationResult operationResult)
        {
            const string valuePropertyName = nameof(IOperationResult<object>.Value);
            var valueProperty = operationResult.GetType()
                .GetProperties()
                .SingleOrDefault(x => x.Name == valuePropertyName);
            var value = valueProperty?.GetValue(operationResult);
            return value;
        }
    }
}
