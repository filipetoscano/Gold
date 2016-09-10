using Gold.Forms;
using Gold.Runtime.Visio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Visio = Microsoft.Office.Interop.Visio;

namespace Gold.VisioAddIn
{
    public partial class ThisAddIn
    {
        private AppRibbon _ribbon;
        private bool _initializedOk;

        private void ThisAddIn_Startup( object sender, System.EventArgs e )
        {
            try
            {
                /*
                 * With regards to shapes/events, we're only interested in responding to 
                 * the MarkerEvent callback: the marker events are queued when interacting
                 * directly with the shape itself (ie, double-clicking on the shape).
                 */
                this.Application.MarkerEvent += new Visio.EApplication_MarkerEventEventHandler( Application_MarkerEvent );


                /*
                 * 
                 */
                _initializedOk = true;
            }
            catch ( Exception ex )
            {
                ExceptionMessageBox.Show( "Unhandled exception: ThisAddIn_Startup", ex );
            }
        }


        private void ThisAddIn_Shutdown( object sender, System.EventArgs e )
        {
        }



        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        * 
        * Shape commands
        * 
        * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        /// <summary>
        /// Handles marker events, as emitted by the shapes.
        /// </summary>
        /// <param name="app">Visio application.</param>
        /// <param name="sequenceNum">Sequence number.</param>
        /// <param name="contextString">Context string, as defined in shape.</param>
        private void Application_MarkerEvent( Visio.Application app, int sequenceNum, string contextString )
        {
            #region Validations

            if ( app == null )
                throw new ArgumentNullException( nameof( app ) );

            #endregion

            try
            {
                Application_MarkerEventDo( app, sequenceNum, contextString );
            }
            catch ( Exception ex )
            {
                ExceptionMessageBox.Show( "Unhandled exception: Application_MarkerEvent", ex );
            }
        }


        /// <summary>
        /// Implementation of the marker event logic.
        /// </summary>
        /// <param name="app">Visio application.</param>
        /// <param name="sequenceNum">Sequence number.</param>
        /// <param name="contextString">Context string, as defined in shape.</param>
        private void Application_MarkerEventDo( Visio.Application app, int sequenceNum, string contextString )
        {
            /*
             * 
             */
            if ( _initializedOk == false )
                return;

            if ( contextString == null )
                return;

            if ( contextString.Contains( "/au=1" ) == false )
                return;


            /*
             * Don't process any marker events that are being fired as a result
             * of a redo.
             */
            if ( this.Application.IsUndoingOrRedoing == true )
                return;



            /*
             * 
             */
            Dictionary<string,string> ctx = VU.ParseContext( contextString );

            if ( ctx.ContainsKey( "cmd" ) == false )
                return;


            /*
             * 
             */
            Visio.Shape shape = VU.GetShape( app, ctx );

            if ( shape == null )
                return;

            string modelName = VU.GetProperty( shape, "User", "ModelName" );
            string shapeName = VU.GetProperty( shape, "User", "ModelShape" );

            if ( string.IsNullOrEmpty( modelName ) == true
                || string.IsNullOrEmpty( shapeName ) == true )
            {
                return;
            }


            /*
             * Model
             */
            IModelDefinition model;

            try
            {
                model = ModelCache.Load( modelName );
            }
            catch ( Exception ex )
            {
                ExceptionMessageBox.Show( "Model not found.", ex );
                return;
            }


            /*
             * 
             */
            IShapeDefinition shapeDef = model.Shapes.Where( x => x.Name == shapeName ).FirstOrDefault();

            if ( shapeDef == null )
            {
                ExceptionMessageBox.Show( "No shape definition." );
                return;
            }


            /*
             * Drop command
             */
            if ( ctx[ "cmd" ] == "drop" )
            {
                /*
                 * .Id
                 */
                string shapeId = Guid.NewGuid().ToString();
                VU.SetProperty( shape, "Prop", "ShapeId", shapeId );


                /*
                 * .Code
                 */
                string shapeCode = null;

                if ( string.IsNullOrEmpty( shapeDef.ShapeCodePrefix ) == false )
                {
                    int sequence = PageIncrementSequence( shape, shapeDef.ShapeCodePrefix );
                    shapeCode = string.Format( CultureInfo.InvariantCulture, shapeDef.ShapeCodeFormat, shapeDef.ShapeCodePrefix, sequence );

                    VU.SetProperty( shape, "Prop", "ShapeCode", shapeCode );
                }


                /*
                 * .Text
                 */
                string shapeXml = VU.GetProperty( shape, "Prop", "ShapeXml" );
                string shapeText = shapeDef.TextGet( shapeCode, shapeXml );

                if ( string.IsNullOrEmpty( shapeText ) == false )
                    VU.TextSet( shape, shapeText );
            }


            /*
             * 
             */
            if ( ctx[ "cmd" ] == "edit" )
            {
                string mode = "analysis"; //_toolbar.CurrentMode;
                string shapeCode = VU.GetProperty( shape, "Prop", "ShapeCode" );
                string shapeXml = VU.GetProperty( shape, "Prop", "ShapeXml" );

                XmlForm form = new XmlForm();
                form.Initialize( shapeDef, mode, shapeCode, shapeXml );

                DialogResult result = form.ShowDialog();

                if ( result == DialogResult.OK )
                {
                    VU.SetProperty( shape, "Prop", "ShapeXml", form.ShapeXml );
                    VU.ShapeColorSet( shape, Visio.VisDefaultColors.visBlack );

                    string shapeText = shapeDef.TextGet( shapeCode, form.ShapeXml );

                    if ( string.IsNullOrEmpty( shapeText ) == false )
                        VU.TextSet( shape, shapeText );
                }
            }
        }



        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        * 
        * Ribbon commands
        * 
        * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        /// <summary>
        /// Executes the .ValidateCurrent command.
        /// </summary>
        internal void ValidateCurrent( ValidationMode mode )
        {
            if ( this.Application.ActivePage == null )
                return;

            // TODO
        }


        /// <summary>
        /// Executes the .ValidateAll command.
        /// </summary>
        internal void ValidateAll( ValidationMode mode )
        {
            if ( this.Application.ActivePage == null )
                return;

            // TODO
        }


        /// <summary>
        /// Executes the .ExportCurrent command.
        /// </summary>
        internal void ExportCurrent( ValidationMode mode )
        {
            if ( this.Application.ActivePage == null )
                return;

            // TODO
        }


        /// <summary>
        /// Executes the .ExportAll command.
        /// </summary>
        internal void ExportAll( ValidationMode mode )
        {
            if ( this.Application.ActivePage == null )
                return;

            // TODO
        }


        /// <summary>
        /// Finds a shape.
        /// </summary>
        internal void FindShape()
        {
            if ( this.Application.ActivePage == null )
                return;

            try
            {
                FindShape( this.Application.ActivePage );
            }
            catch ( Exception ex )
            {
                ExceptionMessageBox.Show( "Unhandled exception: FindShape", ex );
            }
        }



        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        * 
        * Workers
        * 
        * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        private static void FindShape( Visio.IVPage page )
        {
            #region Validations

            if ( page == null )
                throw new ArgumentNullException( nameof( page ) );

            #endregion


            /*
             * 
             */
            FindShapeForm form = new FindShapeForm();
            DialogResult result = form.ShowDialog();

            if ( result != DialogResult.OK )
                return;


            /*
             * 
             */
            foreach ( Visio.IVShape shape in page.Shapes )
            {
                string shapeCode = VU.GetProperty( shape, "Prop", "ShapeCode" );

                if ( shapeCode == form.ShapeCode )
                {
                    VU.ShapeColorSet( shape, Visio.VisDefaultColors.visGreen );
                    return;
                }
            }
        }


        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        * 
        * Shared
        * 
        * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        /// <summary>
        /// Increments a named sequence, keeping the current sequence value in the
        /// page shape-sheet. If the sequence does not exist, will create the
        /// necessary properties in the shape-sheet.
        /// </summary>
        /// <param name="shape">Visio shape.</param>
        /// <param name="sequence">Sequence which will be incremented.</param>
        /// <returns>Sequence number.</returns>
        private int PageIncrementSequence( Visio.IVShape shape, string sequence )
        {
            #region Validations

            if ( shape == null )
                throw new ArgumentNullException( nameof( shape ) );

            if ( sequence == null )
                throw new ArgumentNullException( nameof( sequence ) );

            #endregion

            /*
             * 
             */
            Visio.Page page = (Visio.Page) shape.Parent;

            string cellFull = string.Concat( "User.ModelSequence_", sequence, ".Value" );
            string cellPart = string.Concat( "ModelSequence_", sequence );


            /*
             * 
             */
            int value = 0;

            lock ( this )
            {
                Visio.Cell cell;

                if ( page.PageSheet.get_CellExists( cellFull, 1 ) != 0 )
                {
                    cell = page.PageSheet.get_Cells( cellFull );
                    value = int.Parse( VU.FormulaToString( cell.Formula ), CultureInfo.InvariantCulture );
                }
                else
                {
                    page.PageSheet.AddNamedRow( (short) Visio.VisSectionIndices.visSectionUser, cellPart, 0 );
                    cell = page.PageSheet.get_Cells( cellFull );
                }

                cell.Formula = (++value).ToString( CultureInfo.InvariantCulture );
            }

            return value;
        }



        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        * 
        * API
        * 
        * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        /// <summary>
        /// Indicate which ribbon this addin requires.
        /// </summary>
        /// <returns>
        /// Instance of ribbon.
        /// </returns>
        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            _ribbon = new AppRibbon( this );

            return _ribbon;
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new EventHandler( ThisAddIn_Startup );
            this.Shutdown += new EventHandler( ThisAddIn_Shutdown );
        }
    }
}
