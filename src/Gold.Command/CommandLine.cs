using NDesk.Options;
using Platinum;
using System;
using System.Collections.Generic;

namespace Gold.Command
{
    public class CommandLine
    {
        /// <summary />
        public List<string> FilePatterns { get; private set; }

        /// <summary />
        public ValidationMode Mode { get; private set; }

        /// <summary />
        public string OutputDirectory { get; private set; }

        /// <summary />
        public bool ValidateOnly { get; private set; }

        /// <summary />
        public bool Verbose { get; private set; }

        /// <summary />
        public bool Help { get; private set; }


        public bool Parse( string[] args )
        {
            /*
             * Defaults
             */
            this.Mode = ValidationMode.Live;


            /*
             * Parse
             */
            var p = new OptionSet()
            {
                { "m=",                v => this.Mode = Enumerate.ParseInsensitive<ValidationMode>( v ) },
                { "o=",                v => this.OutputDirectory = v },
                { "output-directory=", v => this.OutputDirectory = v },

                { "h|help",        v => this.Help = true },
                { "t|validate",    v => this.ValidateOnly = true },
                { "v|verbose",     v => this.Verbose = true },
            };

            List<string> extra;
            
            try
            { 
                extra = p.Parse( args );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( ex.ToString() );
                return false;
            }


            /*
             * Stop parsing the remainder of the options, if the user has
             * specified that he wishes to view the help.
             */
            if ( this.Help == true )
                return true;


            /*
             * All extra parameters are file patterns, which need to be
             * expanded.
             */
            this.FilePatterns = extra;

            return true;
        }


        public void HelpShow()
        {
            Console.WriteLine( "usage: aucmd [OPTION] [FILES]..." );
            Console.WriteLine( "  -m=MODE                      Validation mode (Default=Live)" );
            Console.WriteLine( "  -o, --output-directory=PATH  Validation mode (Default=Live)" );
            Console.WriteLine( "  -t, --validate               Validate file only, don't export" );
            Console.WriteLine( "  -v, --verbose                Verbose output" );
            Console.WriteLine( "  -h, --help                   Print this help page" );
        }
    }
}
