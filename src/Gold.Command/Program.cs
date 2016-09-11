using Gold.Runtime;
using System;
using System.IO;
using System.Linq;

namespace Gold.Command
{
    public static class Program
    {
        private static ProgramOptions _options;

        static void Main( string[] args )
        {
            /*
             * Command-line options
             */
            ProgramOptions options = new ProgramOptions();
            //ICommandLineParser parser = new CommandLineParser( new CommandLineParserSettings( Console.Error ) );

            //if ( parser.ParseArguments( args, options ) == false )
            //{
            //    Environment.Exit( 1 );
            //}

            if ( options.FilePatterns.Count == 0 )
            {
                Console.WriteLine( options.GetUsage() );
                Environment.Exit( 2 );
            }

            _options = options;



            /*
             * Construct list of (unique) files to be processed.
             */
            string[] files = null; //Path2.ToUnique( options.FilePatterns.ToArray() );

            if ( files.Length == 0 )
            {
                Console.WriteLine( "error: no matching files" );
                Environment.Exit( 3 );
            }


            /*
             * 
             */
            Console.WriteLine( "~ mode: '{0}'", options.Mode );


            /*
             * 
             */
            Microsoft.Office.Interop.Visio.Application visio = null;
            bool anyError = false;

            try
            {
                /*
                 * 
                 */
                ModelExporter exporter = new ModelExporter();
                exporter.PageStart += new EventHandler<PageEventArgs>( OnPageStart );
                exporter.PageEnd += new EventHandler<PageEventArgs>( OnPageEnd );
                exporter.StepStart += new EventHandler<PageStepEventArgs>( OnStepStart );
                exporter.StepEnd += new EventHandler<PageStepEventArgs>( OnStepEnd );

                /*
                 * 
                 */
                visio = new Microsoft.Office.Interop.Visio.Application();
                visio.AlertResponse = 1;
                visio.Visible = false;

                foreach ( string file in files )
                {
                    /*
                     * 
                     */
                    Console.Write( "+ opening '{0}'...", "TODO" ); //Path2.RelativePath( file ) );
                    FileInfo fileInfo = new FileInfo( file );

                    if ( fileInfo.Exists == false )
                    {
                        ConsoleFail();
                        Console.WriteLine( " - file does not exist" );

                        anyError = true;
                        continue;
                    }


                    /*
                     * Open the document: if the document is not a valid/compatible Visio 
                     * document, this will fail.
                     */
                    Microsoft.Office.Interop.Visio.Document document;

                    try
                    {
                        document = visio.Documents.Open( fileInfo.FullName );
                        ConsoleOk();
                    }
                    catch ( Exception ex )
                    {
                        ConsoleFail();

                        if ( options.Verbose == true )
                            ConsoleDump( ex );

                        anyError = true;
                        continue;
                    }


                    /*
                     * Settings
                     */
                    ModelExportSettings exportSettings = new ModelExportSettings();
                    exportSettings.Program = "ModelExport";
                    exportSettings.Mode = options.Mode;
                    exportSettings.Path = options.OutputDirectory ?? fileInfo.DirectoryName;

                    exporter.Settings = exportSettings;


                    /*
                     * Export.
                     */
                    ModelCommandResult result = null;

                    ModelCommand command = new ModelCommand();
                    command.Document = document;
                    command.Operation = ModelOperation.ExportAll;

                    if ( options.ValidateOnly == true )
                        command.Operation = ModelOperation.ValidateAll;


                    try
                    {
                        result = exporter.Execute( command );
                    }
                    catch ( Exception ex )
                    {
                        anyError = true;

                        if ( options.Verbose == true )
                            ConsoleDump( ex );
                    }
                    finally
                    {
                        document.Close();
                    }

                    if ( result == null )
                        continue;


                    /*
                     * 
                     */
                    Console.WriteLine( "" );
                    Console.WriteLine( "execution summary" );
                    Console.WriteLine( "-------------------------------------------------------------------------" );

                    foreach ( ModelCommandPageResult pageResult in result.Pages )
                    {
                        Console.Write( pageResult.Name );

                        if ( pageResult.Success == true )
                            ConsoleOk();
                        else
                            ConsoleFail();

                        foreach ( ModelResultItem item in pageResult.Items )
                        {
                            string marker;

                            switch ( item.ItemType )
                            {
                                case ModelResultItemType.Error:
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    marker = "!";
                                    break;

                                case ModelResultItemType.Warning:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    marker = "W";
                                    break;

                                default:
                                    marker = " ";
                                    break;
                            }

                            if ( item.VisioShapeId == null )
                                Console.WriteLine( "{0} [page] {1}#{2}: {3}", marker, item.Actor, item.Code, item.Description );
                            else
                                Console.WriteLine( "{0} [{1}] {2}#{3}: {4}", marker, item.VisioShapeId, item.Actor, item.Code, item.Description );

                            Console.ResetColor();
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                ConsoleDump( ex );
            }
            finally
            {
                // This _needs_ to be performed here: in case an exception is caught,
                // make sure that Visio is closed or otherwise a zombie process will
                // be left running on the machine.
                if ( visio != null )
                    visio.Quit();
            }


            /*
             * 
             */
            if ( anyError == true )
            {
                Environment.Exit( 3 );
            }
            else
            {
                Environment.Exit( 0 );
            }
        }


        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         ~ 
         ~ Notification callbacks
         ~ 
         ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        private static void OnPageStart( object sender, PageEventArgs args )
        {
            #region Validations

            if ( args == null )
                throw new ArgumentNullException( nameof( args ) );

            #endregion

            Console.WriteLine( ">> {0}/{1}: {2}", args.PageIndex, args.PageCount, args.Page );
        }

        private static void OnPageEnd( object sender, PageEventArgs args )
        {
            #region Validations

            if ( args == null )
                throw new ArgumentNullException( nameof( args ) );

            #endregion

            Console.WriteLine();
            Console.WriteLine( "<< {0}/{1}: {2}", args.PageIndex, args.PageCount, args.Page );
        }

        private static void OnStepStart( object sender, PageStepEventArgs args )
        {
            #region Validations

            if ( args == null )
                throw new ArgumentNullException( nameof( args ) );

            #endregion

            if ( _options.Verbose == true )
                Console.Write( " > {0}/{1}: {2}", args.StepIndex, args.StepCount, args.Description );
            else
                Console.Write( "." );
        }

        private static void OnStepEnd( object sender, PageStepEventArgs args )
        {
            #region Validations

            if ( args == null )
                throw new ArgumentNullException( nameof( args ) );

            #endregion

            if ( _options.Verbose == true )
            {
                Console.WriteLine( "" );
            }
            else
            {
                if ( args.StepCount > 500 && args.StepIndex == args.StepCount )
                    Console.Write( " PARTY" );
                else if ( (args.StepIndex % 70) == 0 )
                    Console.WriteLine( " {0:0.00%}", 1.0 * args.StepIndex / args.StepCount );
            }
        }



        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         ~ 
         ~ Console stuff
         ~ 
         ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        private static void ConsoleOk()
        {
            Console.Write( " " );

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write( "ok" );

            Console.ResetColor();
            Console.WriteLine( "" );
        }

        private static void ConsoleFail()
        {
            Console.Write( " " );

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine( "fail" );

            Console.ResetColor();
        }

        private static void ConsoleDump( Exception exception )
        {
            #region Validations

            if ( exception == null )
                throw new ArgumentNullException( nameof( exception ) );

            #endregion

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine( exception.ToString() );
            Console.ResetColor();
        }
    }
}
