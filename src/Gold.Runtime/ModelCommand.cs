using Microsoft.Office.Interop.Visio;

namespace Gold.Runtime
{
    public class ModelCommand
    {
        public ModelOperation Operation
        {
            get;
            set;
        }

        public IVDocument Document
        {
            get;
            set;
        }

        public IVPage Page
        {
            get;
            set;
        }
    }
}

/* eof */