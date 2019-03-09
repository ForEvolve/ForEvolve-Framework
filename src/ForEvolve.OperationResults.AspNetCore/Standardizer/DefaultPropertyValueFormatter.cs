namespace ForEvolve.OperationResults.Standardizer
{
    /// <summary>
    /// Represents the default property value formatter, used by <see cref="DefaultOperationResultStandardizer"/>.
    /// Implements the <see cref="IoTCore.Api.Devices.IPropertyValueFormatter" />
    /// </summary>
    /// <seealso cref="IoTCore.Api.Devices.IPropertyValueFormatter" />
    public class DefaultPropertyValueFormatter : IPropertyValueFormatter
    {
        /// <inheritdoc />
        public object Format(object @object)
        {
            return @object;
        }
    }
}
