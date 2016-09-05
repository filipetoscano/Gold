// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class NodeDefinition
    {
    }


    [XmlRoot( ElementName = "r" )]
    public partial class Node : ShapeBase, IShape
    {
        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public string Notes { get; set; }

        /// <summary />
        public string Moniker { get; set; }

        /// <summary />
        public string StoryEvent { get; set; }
    }
}