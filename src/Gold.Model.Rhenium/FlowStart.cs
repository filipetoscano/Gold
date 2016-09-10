// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class FlowStartDefinition : ShapeDefinitionBase<FlowStart>, IShapeDefinition
    {
        /// <summary />
        public string Name
        { get { return "FlowStart"; } }

        /// <summary />
        public string FriendlyName
        { get { return "Web flow"; } }

        /// <summary />
        public XmlDocument FormDefinition
        { get { return FormDefinitionLoad(); } }
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

        /// <summary />
        protected override bool ValidatePropertiesAuto( ValidationMode mode )
        {
            if ( IsRequired( ValidationMode.Analysis, mode ) == true && this.Flow == null )
                return false;

            if ( this.Flow != null && this.Flow.Length > 6 )
                return false;

            if ( IsRequired( ValidationMode.Analysis, mode ) == true && this.Description == null )
                return false;

            if ( this.Description != null && this.Description.Length > 50 )
                return false;

            return true;
        }
    }
}