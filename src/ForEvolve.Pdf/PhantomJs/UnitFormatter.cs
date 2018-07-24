using System;

namespace ForEvolve.Pdf.PhantomJs
{
    /// <summary>
    /// Internal unit formatter.
    /// </summary>
    public static class UnitFormatter
    {
        /// <summary>
        /// Format the specified unit in its string equivalent.
        /// </summary>
        /// <param name="unitToFormat">The unit to format.</param>
        /// <returns>The formatted unit. </returns>
        /// <exception cref="ArgumentException">The specified unit is not supported.</exception>
        /// <example>Inputting <c>Unit.Pixel</c> will output "px".</example>
        public static string Format(Unit unitToFormat)
        {
            switch (unitToFormat)
            {
                case Unit.Pixel:
                    return "px";
                case Unit.Millimeter:
                    return "mm";
                case Unit.Centimeter:
                    return "cm";
                case Unit.Inch:
                    return "in";
            }
            throw new ArgumentException(
                $"The specified unit is not supported: {unitToFormat}.", 
                nameof(unitToFormat)
            );
        }
    }
}
