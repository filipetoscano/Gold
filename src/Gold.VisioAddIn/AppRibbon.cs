using Gold.VisioAddIn.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;

namespace Gold.VisioAddIn
{
    [ComVisible( true )]
    public class AppRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI _ribbon;
        private int? _modeIndex;


        /// <summary>
        /// New instance of Ribbon, by which we run commands over the current page.
        /// </summary>
        public AppRibbon()
        {
        }


        /// <summary>
        /// Loads the ribbon UI.
        /// </summary>
        /// <param name="ribbonUI"></param>
        public void Ribbon_Load( Office.IRibbonUI ribbonUI )
        {
            _ribbon = ribbonUI;
        }


        /// <summary>
        /// Executes the validate action on the current document.
        /// </summary>
        /// <param name="control">Ribbon control.</param>
        public void OnValidateAction( Office.IRibbonControl control )
        {
            MessageBox.Show( "validate" );
        }


        /// <summary>
        /// Gets the image for the validate button.
        /// </summary>
        /// <param name="control">Ribbon control.</param>
        /// <returns>Image bitmap.</returns>
        public Bitmap ValidateGetImage( Office.IRibbonControl control )
        {
            return Resources.ValidateImage;
        }


        /// <summary>
        /// Executes the export action on the current document.
        /// </summary>
        /// <param name="control">Ribbon control.</param>
        public void OnExportAction( Office.IRibbonControl control )
        {
            MessageBox.Show( "export" );
        }


        /// <summary>
        /// Gets the image for the export button.
        /// </summary>
        /// <param name="control">Ribbon control.</param>
        /// <returns>Image bitmap.</returns>
        public Bitmap ExportGetImage( Office.IRibbonControl control )
        {
            return Resources.ExportImage;
        }


        /// <summary>
        /// Callback whenever the mode drop-down is changed.
        /// </summary>
        /// <param name="control">Ribbon control.</param>
        /// <param name="itemId">Id of the item which was selected.</param>
        /// <param name="index">Index of the item which was selected.</param>
        public void OnModeAction( Office.IRibbonControl control, string itemId, int index )
        {
            _modeIndex = index;
        }


        /// <summary>
        /// Gets the value for the 'mode' drop-down.
        /// </summary>
        /// <param name="control">Ribbon control.</param>
        /// <returns>Index of mode.</returns>
        public int ModeGetSelectedItemIndex( Office.IRibbonControl control )
        {
            if ( _modeIndex.HasValue == false )
                return 0;

            return _modeIndex.Value;
        }


        /// <summary>
        /// Gets the number of items under the mode drop-down.
        /// </summary>
        /// <param name="control">Ribbon control.</param>
        public int ModeGetItemCount( Office.IRibbonControl control )
        {
            return 3;
        }


        /// <summary>
        /// Gets the label for the given drop-down item.
        /// </summary>
        /// <param name="control">Ribbon control.</param>
        /// <param name="index">Index of the item.</param>
        /// <returns>Label of $index item.</returns>
        public string ModeGetItemLabel( Office.IRibbonControl control, int index )
        {
            string l;

            switch ( index )
            {
                case 0:
                    l = "Analysis";
                    break;

                case 1:
                    l = "Storyboard";
                    break;

                case 2:
                    l = "Live";
                    break;

                default:
                    l = "Woot?";
                    break;
            }

            return l;
        }


        #region IRibbonExtensibility Members

        public string GetCustomUI( string ribbonID )
        {
            return GetResourceText( "Gold.VisioAddIn.AppRibbon.xml" );
        }

        #endregion

        #region Helpers

        private static string GetResourceText( string resourceName )
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for ( int i = 0; i < resourceNames.Length; ++i )
            {
                if ( string.Compare( resourceName, resourceNames[ i ], StringComparison.OrdinalIgnoreCase ) == 0 )
                {
                    using ( StreamReader resourceReader = new StreamReader( asm.GetManifestResourceStream( resourceNames[ i ] ) ) )
                    {
                        if ( resourceReader != null )
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
