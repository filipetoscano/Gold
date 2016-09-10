using Microsoft.Office.Interop.Visio;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Gold.Runtime
{
    public sealed class ModelExporter
    {
        public ModelExporter()
        {
        }


        public ModelExportSettings Settings
        {
            get;
            set;
        }


        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         ~ 
         ~ Public methods
         ~ 
         ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        /// <summary>
        /// Executes an export command.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        public ModelCommandResult Execute( ModelCommand command )
        {
            #region Validations

            if ( command == null )
                throw new ArgumentNullException( nameof( command ) );

            #endregion

            ModelCommandResult result = ValidateSettings( command, this.Settings );

            if ( result != null )
                return result;

            switch ( command.Operation )
            {
                case ModelOperation.ExportAll:
                    result = ExportAll( command.Document );
                    break;

                case ModelOperation.ExportCurrent:
                    result = ExportPage( command.Page );
                    break;

                case ModelOperation.ValidateAll:
                    result = ValidateAll( command.Document );
                    break;

                case ModelOperation.ValidateCurrent:
                    result = ValidatePage( command.Page );
                    break;
            }

            return result;
        }


        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         ~ 
         ~ Private methods
         ~ 
         ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        /// <summary>
        /// Validate all of the pages in the designated document.
        /// </summary>
        /// <param name="document">Visio document.</param>
        private ModelCommandResult ValidateAll( IVDocument document )
        {
            #region Validations

            if ( document == null )
                throw new ArgumentNullException( nameof( document ) );

            #endregion

            ModelCommandResult result = new ModelCommandResult();

            int pageCount = document.Pages.Count;

            for ( int i = 1; i <= pageCount; i++ )
            {
                Page page = document.Pages[ i ];
                PageEventArgs ev = new PageEventArgs( i, pageCount, page.Name );

                OnPageStart( ev );

                ModelCommandPageResult pageResult = Work( page, true );
                result.Add( pageResult );

                OnPageEnd( ev );
            }

            return result;
        }


        /// <summary>
        /// Validate the designated Visio page.
        /// </summary>
        /// <param name="page">Visio page.</param>
        private ModelCommandResult ValidatePage( IVPage page )
        {
            #region Validations

            if ( page == null )
                throw new ArgumentNullException( nameof( page ) );

            #endregion

            ModelCommandResult result = new ModelCommandResult();

            PageEventArgs ev = new PageEventArgs( 1, 1, page.Name );
            OnPageStart( ev );

            ModelCommandPageResult pageResult = Work( page, true );
            result.Add( pageResult );

            OnPageEnd( ev );

            return result;
        }


        /// <summary>
        /// Validate and export all of the pages in the designated document.
        /// </summary>
        /// <param name="document">Visio document.</param>
        private ModelCommandResult ExportAll( IVDocument document )
        {
            #region Validations

            if ( document == null )
                throw new ArgumentNullException( nameof( document ) );

            #endregion

            ModelCommandResult result = new ModelCommandResult();
            int pageCount = document.Pages.Count;

            for ( int i = 1; i <= pageCount; i++ )
            {
                Page page = document.Pages[ i ];
                PageEventArgs ev = new PageEventArgs( i, pageCount, page.Name );

                OnPageStart( ev );

                ModelCommandPageResult pageResult = Work( page, false );
                result.Add( pageResult );

                OnPageEnd( ev );
            }

            return result;
        }


        /// <summary>
        /// Validate and export the designated page.
        /// </summary>
        /// <param name="page">Visio page.</param>
        private ModelCommandResult ExportPage( IVPage page )
        {
            #region Validations

            if ( page == null )
                throw new ArgumentNullException( nameof( page ) );

            #endregion

            ModelCommandResult result = new ModelCommandResult();

            PageEventArgs ev = new PageEventArgs( 1, 1, page.Name );
            OnPageStart( ev );

            ModelCommandPageResult pageResult = Work( page, false );
            result.Add( pageResult );

            OnPageEnd( ev );

            return result;
        }



        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         ~ 
         ~ Settings validation
         ~ 
         ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        private static ModelCommandResult ValidateSettings( ModelCommand command, ModelExportSettings settings )
        {
            #region Validations

            if ( command == null )
                throw new ArgumentNullException( nameof( command ) );

            #endregion

            string pageName = PageName( command );

            if ( settings == null )
                return BuildResult( pageName, "SettingsRequired" );

            if ( command.Operation == ModelOperation.ExportAll || command.Operation == ModelOperation.ValidateAll )
            {
                if ( command.Document == null )
                    return BuildResult( pageName, "SettingsPathDocumentNull" );
            }

            if ( command.Operation == ModelOperation.ExportCurrent || command.Operation == ModelOperation.ValidateCurrent )
            {
                if ( command.Page == null )
                    return BuildResult( pageName, "SettingsPathPageNull" );
            }

            if ( command.Operation == ModelOperation.ExportAll || command.Operation == ModelOperation.ExportCurrent )
            {
                if ( string.IsNullOrEmpty( settings.Path ) == true )
                {
                    if ( settings.Program == "VisioAddIn" )
                        return BuildResult( pageName, "SettingsPathRequiredAddIn" );
                    else
                        return BuildResult( pageName, "SettingsPathRequiredElse" );
                }
            }

            return null;
        }


        private static string PageName( ModelCommand command )
        {
            #region Validations

            if ( command == null )
                throw new ArgumentNullException( nameof( command ) );

            #endregion

            if ( command.Page != null )
                return command.Page.Name;
            else
                return command.Document.Pages[ 1 ].Name;
        }


        private static ModelCommandResult BuildResult( string pageName, string itemId )
        {
            #region Validations

            if ( pageName == null )
                throw new ArgumentNullException( nameof( pageName ) );

            if ( itemId == null )
                throw new ArgumentNullException( nameof( itemId ) );

            #endregion

            ModelCommandPageResult pageResult = new ModelCommandPageResult( pageName );
            pageResult.Add( itemId );

            ModelCommandResult result = new ModelCommandResult();
            result.Add( pageResult );

            return result;
        }



        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         ~ 
         ~ Worker methods
         ~ 
         ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        [SuppressMessage( "Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes" )]
        private ModelCommandPageResult Work( IVPage page, bool validateOnly )
        {
            ModelCommandPageResult result;

            try
            {
                //result = WorkImpl( page, validateOnly );
                result = null;
            }
            catch ( Exception )
            {
                /*
                 * Aw crap, WorkImpl() should be coded in a such a way as to _never_
                 * throw an exception. Anytime the 'WorkerFail' error shows up, we
                 * should stride to eliminate them.
                 */
                result = new ModelCommandPageResult( page.Name );
                //result.Add( R.WorkerFail, ToString( ex ) );
            }

            return result;
        }



        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         ~ 
         ~ Delegates
         ~ 
         ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
        public event EventHandler<PageEventArgs> PageStart;
        public event EventHandler<PageEventArgs> PageEnd;
        public event EventHandler<PageStepEventArgs> StepStart;
        public event EventHandler<PageStepEventArgs> StepEnd;

        private void OnPageStart( PageEventArgs e )
        {
            PageStart?.Invoke( this, e );
        }

        private void OnPageEnd( PageEventArgs e )
        {
            PageEnd?.Invoke( this, e );
        }

        private void OnStepStart( PageStepEventArgs e )
        {
            StepStart?.Invoke( this, e );
        }

        private void OnStepEnd( PageStepEventArgs e )
        {
            StepEnd?.Invoke( this, e );
        }
    }
}
