namespace ForEvolve.OperationResults.Standardizer
{
    /// <summary>
    /// Represents a property name formatter, used by <see cref="DefaultOperationResultStandardizer"/>.
    /// </summary>
    public interface IPropertyNameFormatter
    {
        /// <summary>
        /// Formats the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        string Format(string name);
    }
}
