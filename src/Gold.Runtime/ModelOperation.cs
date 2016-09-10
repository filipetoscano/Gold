namespace Gold.Runtime
{
    /// <summary>
    /// Operation being performed over the model document.
    /// </summary>
    public enum ModelOperation
    {
        /// <summary>
        /// Validate all of the pages.
        /// </summary>
        ValidateAll,

        /// <summary>
        /// Validate the current page.
        /// </summary>
        ValidateCurrent,

        /// <summary>
        /// Export all of the pages.
        /// </summary>
        ExportAll,

        /// <summary>
        /// Export the current page.
        /// </summary>
        ExportCurrent
    }
}

/* eof */