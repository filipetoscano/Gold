// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class ReturnDefinition : ShapeDefinitionBase<Return>, IShapeDefinition
    {
        /// <summary />
        public string Name
        { get { return "Return"; } }

        /// <summary />
        public string FriendlyName
        { get { return "Return"; } }

        /// <summary />
        public override string ShapeCodePrefix
        { get { return "Z"; } }

        /// <summary />
        public override string ShapeCodeFormat
        { get { return "Z{1:D3}"; } }

        /// <summary />
        public XmlDocument FormDefinition
        { get { return FormDefinitionLoad(); } }
    }


    [XmlRoot( ElementName = "r" )]
    public partial class Return : Shape, IShape
    {
        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public string Notes { get; set; }

        /// <summary />
        protected override bool ValidatePropertiesAuto( ValidationMode mode )
        {
            if ( this.Description != null && this.Description.Length > 100 )
                return false;

            return true;
        }
    }
}