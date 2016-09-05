// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class FlowStartDefinition
    {
    }


    [XmlRoot( ElementName = "r" )]
    public partial class FlowStart : ShapeBase, IShape
    {
        /// <summary />
        public string Flow { get; set; }

        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public string Notes { get; set; }
    }
}