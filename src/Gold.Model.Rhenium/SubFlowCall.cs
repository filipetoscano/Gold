// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class SubFlowCallDefinition : ShapeDefinitionBase<SubFlowCall>, IShapeDefinition
    {
        /// <summary />
        public string Name
        { get { return "SubFlowCall"; } }

        /// <summary />
        public string FriendlyName
        { get { return "Sub-flow"; } }

        /// <summary />
        public override string ShapeCodePrefix
        { get { return "S"; } }

        /// <summary />
        public override string ShapeCodeFormat
        { get { return "S{1:D3}"; } }

        /// <summary />
        public XmlDocument FormDefinition
        { get { return FormDefinitionLoad(); } }
    }


    [XmlRoot( ElementName = "r" )]
    public partial class SubFlowCall : Shape, IShape
    {
        /// <summary />
        public string Code { get; set; }

        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public string Notes { get; set; }

        /// <summary />
        protected override bool ValidatePropertiesAuto( ValidationMode mode )
        {
            if ( IsRequired( ValidationMode.Analysis, mode ) == true && this.Code == null )
                return false;

            if ( this.Code != null && this.Code.Length > 6 )
                return false;

            if ( IsRequired( ValidationMode.Analysis, mode ) == true && this.Description == null )
                return false;

            if ( this.Description != null && this.Description.Length > 100 )
                return false;

            return true;
        }
    }
}