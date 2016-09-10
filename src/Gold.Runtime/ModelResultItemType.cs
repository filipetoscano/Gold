namespace Gold.Runtime
{
    public enum ModelResultItemType
    {
        /// <summary>
        /// Item is a fatal error, which prevented the model command from
        /// being completed successfully.
        /// </summary>
        Error,

        /// <summary>
        /// Item is a warning: although it does not prevent the model command
        /// from completing the end-user should be made aware of the item.
        /// </summary>
        Warning,

        /// <summary>
        /// Information item.
        /// </summary>
        Information
    }
}

/* eof */