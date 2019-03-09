namespace ForEvolve.OperationResults.Standardizer
{
    /// <summary>
    /// Represents a property value formatter, used by <see cref="DefaultOperationResultStandardizer"/>.
    /// </summary>
    public interface IPropertyValueFormatter
    {
        /// <summary>
        /// Formats the specified object.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <returns>System.Object.</returns>
        object Format(object @object);
    }
}
