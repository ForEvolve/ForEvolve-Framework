namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Represents a builder class that can be extended with functionalities.
    /// </summary>
    public interface IForEvolveSeederBuilder
    {
        /// <summary>
        /// Gets the services populated by the builder.
        /// </summary>
        /// <value>The services.</value>
        IServiceCollection Services { get; }
    }
}
