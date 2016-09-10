namespace Gold.Runtime
{
    public class ModelExportSettings
    {
        public ModelExportSettings()
        {
            this.Mode = ValidationMode.Live;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Program
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets validation mode.
        /// </summary>
        public ValidationMode Mode
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            get;
            set;
        }
    }
}

/* eof */