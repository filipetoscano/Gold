using System;

namespace Gold.Runtime
{
    public class PageEventArgs : EventArgs
    {
        public PageEventArgs( int pageIndex, int pageCount, string page )
        {
            #region Validations

            if ( page == null )
                throw new ArgumentNullException( nameof( page ) );

            if ( pageIndex < 0 )
                throw new ArgumentOutOfRangeException( nameof( pageIndex ), "Must be positive" );

            if ( pageCount < 1 )
                throw new ArgumentOutOfRangeException( nameof( pageCount ), "Must be positive" );

            if ( pageIndex > pageCount )
                throw new ArgumentOutOfRangeException( nameof( pageIndex ), "Must be less-than-equal to pageCount" );

            #endregion

            this.Page = page;
            this.PageIndex = pageIndex;
            this.PageCount = pageCount;
        }


        /// <summary>
        /// Name of the current Visio page.
        /// </summary>
        public string Page
        {
            get;
            private set;
        }


        /// <summary>
        /// Index of the current page.
        /// </summary>
        public int PageIndex
        {
            get;
            private set;
        }


        /// <summary>
        /// Number of pages.
        /// </summary>
        public int PageCount
        {
            get;
            private set;
        }
    }
}

/* eof */