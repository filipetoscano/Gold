using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gold.Runtime
{
    public class ModelCommandResult
    {
        private List<ModelCommandPageResult> _pages;

        /// <summary>
        /// Initializes a new instance of the ModelCommandResult class.
        /// </summary>
        public ModelCommandResult()
        {
            _pages = new List<ModelCommandPageResult>();
        }


        /// <summary>
        /// Gets whether the command has completed successfully.
        /// </summary>
        public bool Success
        {
            get
            {
                return this.Pages.All( i => { return i.Success; } );
            }
        }


        /// <summary>
        /// Gets the collection of page results.
        /// </summary>
        public ReadOnlyCollection<ModelCommandPageResult> Pages
        {
            get
            {
                return _pages.AsReadOnly();
            }
        }
        

        public void Add( ModelCommandPageResult result )
        {
            #region Validations

            if ( result == null )
                throw new ArgumentNullException( "result" );

            #endregion

            _pages.Add( result );
        }
    }
}

/* eof */