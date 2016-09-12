using Microsoft.Office.Interop.Visio;
using Platinum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using VisioShape = Microsoft.Office.Interop.Visio.Shape;

namespace Gold.Runtime.Visio
{
    /// <summary>
    /// VU/(V)isio (U)tility functions. Meant for exclusive use in
    /// the present project to ease the pain of interacting with the
    /// Visio object model.
    /// </summary>
    public static class VU
    {
        /// <summary>
        /// Minimum version of Visio supported: Office 2013.
        /// </summary>
        public const short MinVisioVersion = 15;

        /// <summary>
        /// Begin marker format used in the marker event context string.
        /// </summary>
        public const char ContextBeginMarker = '/';

        /// <summary>
        /// Marker for key value seperator.
        /// </summary>
        public const char ContextEquals = '=';

        /// <summary>
        /// Quotation symbol that is used in Visio Cell formulas.
        /// </summary>
        public const string FormulaQuote = "\"";


        /// <summary>
        /// Converts the input string to a Formula for a string by replacing each 
        /// double quotation mark (") with a pair of double quotation marks ("") 
        /// and adding double quotation marks ("") around the entire string. When 
        /// this formula is assigned to the formula property of a Visio cell it 
        /// will produce a result value equal to the string, input.
        /// </summary>
        /// <param name="value">Input string that will be processed.</param>
        /// <returns>Formula for input string</returns>
        public static string StringToFormula( string value )
        {
            string result = "";

            // Replace all (") with ("").
            result = value.Replace( FormulaQuote, (FormulaQuote + FormulaQuote) );

            // Add ("") around the entire string.
            result = FormulaQuote + result + FormulaQuote;

            return result;
        }

        /// <summary>
        /// Converts the formula into a string, by removing the starting and
        /// trailing quote marks and by replacing any double quotes with single
        /// quotes.
        /// </summary>
        /// <param name="formula">Cell formula value.</param>
        /// <returns>Regular string value.</returns>
        public static string FormulaToString( string formula )
        {
            string result = formula;

            if ( formula.StartsWith( FormulaQuote, StringComparison.Ordinal ) == true
                && formula.EndsWith( FormulaQuote, StringComparison.Ordinal ) == true )
            {
                result = formula
                    .Substring( 1, formula.Length - 2 )
                    .Replace( FormulaQuote + FormulaQuote, FormulaQuote );
            }

            return result;
        }



        /// <summary>
        /// Gets the string value of the Value attribute fom a Shape property.
        /// </summary>
        /// <param name="shape">Visio shape.</param>
        /// <param name="section">Name of the Visio shape-sheep section.</param>
        /// <param name="property">Name of the shape-sheep property (also known as section rows).</param>
        /// <returns>The string value of the Value attribute from the designated property, or
        /// null if the property/attribute does not exist.</returns>
        public static string GetProperty( IVShape shape, string section, string property )
        {
            return GetProperty( shape, section, property, "Value" );
        }


        /// <summary>
        /// Gets the string value of an attribute fom a Shape property.
        /// </summary>
        /// <param name="shape">Visio shape.</param>
        /// <param name="section">Name of the Visio shape-sheep section.</param>
        /// <param name="property">>Name of the shape-sheep property (also known as section rows).</param>
        /// <param name="attribute">Name of the shape-sheep property attribute (also known as section column).</param>
        /// <returns>The string value of the attribute from the designated property, or
        /// null if the property/attribute does not exist.</returns>
        public static string GetProperty( IVShape shape, string section, string property, string attribute )
        {
            string cellLocation = string.Format( CultureInfo.InvariantCulture, "{0}.{1}.{2}", section, property, attribute );
            Cell cell;

            try
            {
                cell = shape.get_Cells( cellLocation );
            }
            catch ( COMException )
            {
                return null;
            }

            return FormulaToString( cell.Formula );
        }


        /// <summary>
        /// Sets the string value of an attribute fom a Shape property.
        /// </summary>
        /// <param name="shape">Visio shape.</param>
        /// <param name="section">Name of the Visio shape-sheep section.</param>
        /// <param name="property">>Name of the shape-sheep property (also known as section rows).</param>
        /// <param name="value">The value to set.</param>
        public static void SetProperty( IVShape shape, string section, string property, string value )
        {
            SetProperty( shape, section, property, "Value", value );
        }


        /// <summary>
        /// Sets the string value of an attribute fom a Shape property.
        /// </summary>
        /// <param name="shape">Visio shape.</param>
        /// <param name="section">Name of the Visio shape-sheep section.</param>
        /// <param name="property">>Name of the shape-sheep property (also known as section rows).</param>
        /// <param name="attribute">Name of the shape-sheep property attribute (also known as section column).</param>
        /// <param name="value">The value to set.</param>
        public static void SetProperty( IVShape shape, string section, string property, string attribute, string value )
        {
            string cellLocation = string.Format( CultureInfo.InvariantCulture, "{0}.{1}.{2}", section, property, attribute );
            Cell cell;

            try
            {
                cell = shape.get_Cells( cellLocation );
                cell.Formula = StringToFormula( value );
            }
            catch ( COMException )
            {
            }
        }


        /*
         * Shape Operations
         */

        /// <summary>
        /// Retrieves a Shape from a Visio application based on the Marker Event context.
        /// </summary>
        /// <param name="visioApplication">Visio application instance.</param>
        /// <param name="context">Marker event context.</param>
        /// <returns>Shape that raised marker event, or null if shape is not found.</returns>
        public static VisioShape GetShape( IVApplication visioApplication, string context )
        {
            Dictionary<string, string> nvc = ParseContext( context );

            return GetShape( visioApplication, nvc );
        }


        /// <summary>
        /// Retrieves a Shape from a Visio application based on the Marker Event context.
        /// </summary>
        /// <param name="visioApplication">Visio application instance.</param>
        /// <param name="context">Parsed marker event context.</param>
        /// <returns>Shape that raised marker event, or null if shape is not found.</returns>
        public static VisioShape GetShape( IVApplication visioApplication, Dictionary<string, string> context )
        {
            #region Validations

            if ( visioApplication == null )
                throw new ArgumentNullException( nameof( visioApplication ) );

            if ( context == null )
                throw new ArgumentNullException( nameof( context ) );

            #endregion

            // Standard Visio add-on command line arguments.
            const string ArgumentDoc = "doc";
            const string ArgumentPage = "page";
            const string ArgumentShape = "shape";

            //
            string contextDoc = context[ ArgumentDoc ];
            string contextPage = context[ ArgumentPage ];
            string contextShape = context[ ArgumentShape ];

            if ( contextDoc == null || contextPage == null || contextShape == null )
                return null;

            //
            int docId = Convert.ToInt16( contextDoc, CultureInfo.InvariantCulture );
            int pageId = Convert.ToInt16( contextPage, CultureInfo.InvariantCulture );
            string shapeId = contextShape;


            VisioShape targetShape = null;

            // If the command line arguments contains document, page, and shape
            // then look up the shape.
            if ( (docId > 0) && (pageId > 0) && (shapeId.Length > 0) )
            {
                try
                {
                    Document document = visioApplication.Documents[ docId ];
                    Page page = document.Pages[ pageId ];
                    targetShape = page.Shapes[ shapeId ];
                }
                catch ( COMException )
                {
                    // Doc/Page/Shape not found. Silently ignore, return null.
                }
            }

            return targetShape;
        }


        /// <summary>
        /// Parses the context arguments into the several parts.
        /// </summary>
        /// <param name="context">Event-marker context</param>
        /// <returns>Collection of Name/Values pairs.</returns>
        public static Dictionary<string, string> ParseContext( string context )
        {
            Dictionary<string, string> nvc = new Dictionary<string, string>();

            string[] contextParts = context.Trim().Split( VU.ContextBeginMarker );

            for ( int i = 0; i < contextParts.Length; i++ )
            {
                string[] argument = contextParts[ i ].Trim().Split( new char[] { VU.ContextEquals }, 2 );

                if ( argument.Length == 1 )
                    nvc.Add( argument[ 0 ], null );
                else
                    nvc.Add( argument[ 0 ], argument[ 1 ] );
            }

            return nvc;
        }



        /*
         * PaintShape
         */
        /// <summary>
        /// Returns the color of a given Visio shape.
        /// </summary>
        /// <param name="shape">Root shape</param>
        public static VisDefaultColors ShapeColorGet( IVShape shape )
        {
            #region Validations

            if ( shape == null )
                throw new ArgumentNullException( nameof( shape ) );

            #endregion

            Cell cell = shape.get_CellsSRC( (short) VisSectionIndices.visSectionObject, (short) VisRowIndices.visRowLine, (short) VisCellIndices.visLineColor );

            if ( cell == null )
                return VisDefaultColors.visBlack;

            string formula = cell.FormulaU;

            /*
             * Since the color is a fomula, we can't be 100% sure that a color index
             * will be placed there. Of course, we could assume that the model shapes
             * are all kosher, but that's a pretty big risk! In this case, it's best
             * to assume that if it *ISN'T* a color, then return .visTransparent.
             * 
             * One of the examples of formula color is: =THEMEGUARD(THEME("Color2")) which
             * is meant to preserve the color of the shape according to the palette of
             * the current theme.
             */
            VisDefaultColors color;

            try
            {
                color = Enumerate.Parse<VisDefaultColors>( formula );
            }
            catch ( ActorException )
            {
                color = VisDefaultColors.visTransparent;
            }

            return color;
        }


        /// <summary>
        /// Paints the Shape, and it's composing sub-shapes, with the
        /// given color.
        /// </summary>
        /// <param name="shape">Root shape</param>
        /// <param name="colorIndex">Color, as per index in the Visio Color Palette</param>
        public static void ShapeColorSet( IVShape shape, VisDefaultColors color )
        {
            #region Validations

            if ( shape == null )
                throw new ArgumentNullException( nameof( shape ) );

            #endregion

            Cell cell = shape.get_CellsSRC( (short) VisSectionIndices.visSectionObject, (short) VisRowIndices.visRowLine, (short) VisCellIndices.visLineColor );

            if ( cell != null )
            {
                cell.FormulaU = color.ToString( "D" );
            }

            foreach ( Microsoft.Office.Interop.Visio.Shape subShape in shape.Shapes )
            {
                ShapeColorSet( subShape, color );
            }
        }


        /// <summary>
        /// Sets the text on the shape.
        /// </summary>
        /// <param name="shape">Root shape.</param>
        /// <param name="text">Text of shape.</param>
        public static void TextSet( IVShape shape, string text )
        {
            #region Validations

            if ( shape == null )
                throw new ArgumentNullException( nameof( shape ) );

            if ( text == null )
                throw new ArgumentNullException( nameof( text ) );

            #endregion

            shape.get_Cells( "LockTextEdit" ).ResultIU = 0;
            shape.Text = text;
            shape.get_Cells( "LockTextEdit" ).ResultIU = 1;
        }
    }
}
