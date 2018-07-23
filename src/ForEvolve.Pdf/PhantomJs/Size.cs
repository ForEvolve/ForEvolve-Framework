namespace ForEvolve.Pdf.PhantomJs
{
    /// <summary>
    /// Represent a size.
    /// </summary>
    public sealed class Size
    {
        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.Size class.
        /// </summary>
        public Size()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.Size class.
        /// </summary>
        /// <param name="value">The size's value.</param>
        /// <param name="unit">The size's unit.</param>
        public Size(float value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Gets or sets the size's value.
        /// Default: 0.
        /// </summary>
        public float Value { get; set; } = 0F;

        /// <summary>
        /// Gets or sets the size's unit.
        /// Default: <c>Unit.Inch</c>. 
        /// </summary>
        public Unit Unit { get; set; } = Unit.Inch;

        public override string ToString()
        {
            return $"{Value}{UnitFormatter.Format(Unit)}";
        }
    }
}
