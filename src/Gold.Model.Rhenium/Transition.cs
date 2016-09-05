// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class TransitionDefinition
    {
    }


    [XmlRoot( ElementName = "r" )]
    public partial class Transition : ShapeBase, IShape
    {
        /// <summary />
        public string Event { get; set; }

        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public string Notes { get; set; }

        /// <summary />
        public string TechNotes { get; set; }
    }
}