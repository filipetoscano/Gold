// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class NodeDefinition : ShapeDefinitionBase<Node>, IShapeDefinition
    {
        /// <summary />
        public string Name
        { get { return "Node"; } }

        /// <summary />
        public string FriendlyName
        { get { return "Node"; } }

        /// <summary />
        public override string ShapeCodePrefix
        { get { return "N"; } }

        /// <summary />
        public override string ShapeCodeFormat
        { get { return "N{1:D3}"; } }

        /// <summary />
        public XmlDocument FormDefinition
        { get { return FormDefinitionLoad(); } }
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