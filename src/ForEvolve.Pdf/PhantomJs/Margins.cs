using System;

namespace ForEvolve.Pdf.PhantomJs
{
    /// <summary>
    /// Represent page margins.
    /// </summary>
    public sealed class Margins
    {
        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.Margins class.
        /// </summary>
        public Margins()
            : this(new Size())
        {

        }

        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.Margins class.
        /// </summary>
        /// <param name="all">The Size object used for all four margins.</param>
        public Margins(Size all)
            : this(all, all, all, all)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.Margins class.
        /// </summary>
        /// <param name="topBottom">The Size object used for both top and bottom margins.</param>
        /// <param name="rightLeft">The Size object used for both right and left margins.</param>
        public Margins(Size topBottom, Size rightLeft)
            : this(topBottom, rightLeft, topBottom, rightLeft)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.Margins class.
        /// </summary>
        /// <param name="top">The Size object used for the top margin.</param>
        /// <param name="rightLeft">The Size object used for both right and left margins.</param>
        /// <param name="bottom">The Size object used for the bottom margin.</param>
        public Margins(Size top, Size rightLeft, Size bottom)
            : this(top, rightLeft, bottom, rightLeft)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.Margins class.
        /// </summary>
        /// <param name="top">The Size object used for the top margin.</param>
        /// <param name="right">The Size object used for the right margin.</param>
        /// <param name="bottom">The Size object used for the bottom margin.</param>
        /// <param name="left">The Size object used for the left margin.</param>
        public Margins(Size top, Size right, Size bottom, Size left)
        {
            Top = top ?? throw new ArgumentNullException(nameof(top));
            Right = right ?? throw new ArgumentNullException(nameof(right));
            Bottom = bottom ?? throw new ArgumentNullException(nameof(bottom));
            Left = left ?? throw new ArgumentNullException(nameof(left));
        }

        /// <summary>
        /// Gets or sets the top margin size.
        /// </summary>
        public Size Top { get; }

        /// <summary>
        /// Gets or sets the right margin size.
        /// </summary>
        public Size Right { get; }

        /// <summary>
        /// Gets or sets the bottom margin size.
        /// </summary>
        public Size Bottom { get; }

        /// <summary>
        /// Gets or sets the left margin size.
        /// </summary>
        public Size Left { get; }

        /// <summary>
        /// Gets a normal page margins object that is 1 inch on all sides.
        /// </summary>
        public static Margins Normal => 
            new Margins(new Size(1F, Unit.Inch));

        /// <summary>
        /// Gets a narrow page margins object that is 0.5 inch on all sides.
        /// </summary>
        public static Margins Narrow => 
            new Margins(new Size(0.5F, Unit.Inch));

        /// <summary>
        /// Gets a moderate page margins object that is 1 inch vertically and 0.75 inch horizontally.
        /// </summary>
        public static Margins Moderate => 
            new Margins(new Size(1F, Unit.Inch), new Size(0.75F, Unit.Inch));

        /// <summary>
        /// Gets a wide page margins object that is 1 inch vertically and 2 inches horizontally.
        /// </summary>
        public static Margins Wide => 
            new Margins(new Size(1F, Unit.Inch), new Size(2F, Unit.Inch));
    }
}
