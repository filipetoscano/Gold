using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Visio = Microsoft.Office.Interop.Visio;
using Office = Microsoft.Office.Core;

namespace Gold.VisioAddIn
{
    public partial class ThisAddIn
    {
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


                _initializedOk = true;
            }
            catch ( Exception )
            {

            }
        }


        private void ThisAddIn_Shutdown( object sender, System.EventArgs e )
        {
        }



        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         ~ 
         ~ Shape commands
         ~ 
         ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        private void Application_MarkerEvent( Visio.Application app, int sequenceNum, string contextString )
        {
            #region Validations

            if ( app == null )
                throw new ArgumentNullException( nameof( app ) );

            #endregion


            /*
             * 
             */
            if ( _initializedOk == false )
                return;

            if ( contextString == null )
                return;


            /*
             * Don't process any marker events that are being fired as a result
             * of a redo.
             */
            if ( this.Application.IsUndoingOrRedoing == true )
                return;


        }



        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler( ThisAddIn_Startup );
            this.Shutdown += new System.EventHandler( ThisAddIn_Shutdown );
        }

        #endregion
    }
}
