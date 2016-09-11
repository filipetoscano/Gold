using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Gold.Command
{
    /// <remarks>
    /// See http://commandline.codeplex.com/documentation for more information on
    /// the features of the CommandLine library. Please note that all of the
    /// properties *must* be public in order to be set by reflection.
    /// </remarks>
    public class ProgramOptions
    {
        [SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
        //[Option( "m", "mode", HelpText = "Design mode (Required)", Required = true )]
        public ValidationMode Mode;

        [SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
        //[Option( "d", "output-directory", HelpText = "Write to output directory, instead of placing output file side by side with Visio document (Optional)" )]
        public string OutputDirectory;

        [SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
        //[Option( "t", "validate", HelpText = "Validate only, don't export" )]
        public bool ValidateOnly;

        [SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
        //[Option( "v", "verbose", HelpText = "Emit verbose progress information" )]
        public bool Verbose;

        [SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
        //[ValueList( typeof( List<string> ) )]
        public IList<string> FilePatterns;

        [SuppressMessage( "Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate" )]
        //[HelpOption( HelpText = "Display help screen" )]
        public string GetUsage()
        {
            //var help = new HelpText( "modelexp v" + ToolVersionGetMm() );
            //help.AdditionalNewLineAfterOption = false;
            //help.AddPreOptionsLine( "usage: modelexp [OPTIONS] FILES [FILES [...]]" );
            //help.AddOptions( this );

            //return help;
            return null;
        }

        private static string ToolVersionGetMm()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString( 2 );
        }

        public static string ToolVersionGet()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString( 4 );
        }
    }
}
