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
        { get { return "Flow"; } }

        /// <summary />
        public XmlDocument FormDefinition
        { get { return FormDefinitionLoad(); } }
    }


    [XmlRoot( ElementName = "r" )]
    public partial class FlowStart : Shape, IShape
    {
        /// <summary />
        public string Code { get; set; }

        /// <summary />
        public string Name { get; set; }

        /// <summary />
        public string Notes { get; set; }

        /// <summary />
        protected override bool ValidatePropertiesAuto( ValidationMode mode )
        {
            if ( IsRequired( ValidationMode.Analysis, mode ) == true && this.Code == null )
                return false;

            if ( this.Code != null && this.Code.Length > 6 )
                return false;

            if ( IsRequired( ValidationMode.Analysis, mode ) == true && this.Name == null )
                return false;

            if ( this.Name != null && this.Name.Length > 50 )
                return false;

            return true;
        }
    }
}