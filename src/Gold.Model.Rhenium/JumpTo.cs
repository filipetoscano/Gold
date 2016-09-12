// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class JumpToDefinition : ShapeDefinitionBase<JumpTo>, IShapeDefinition
    {
        /// <summary />
        public string Name
        { get { return "JumpTo"; } }

        /// <summary />
        public string FriendlyName
        { get { return "Jump to"; } }

        /// <summary />
        public override string ShapeCodePrefix
        { get { return "J"; } }

        /// <summary />
        public override string ShapeCodeFormat
        { get { return "J{1:D3}"; } }

        /// <summary />
        public XmlDocument FormDefinition
        { get { return FormDefinitionLoad(); } }
    }


    [XmlRoot( ElementName = "r" )]
    public partial class JumpTo : Shape, IShape
    {
        /// <summary />
        public string Point { get; set; }

        /// <summary />
        public string Notes { get; set; }

        /// <summary />
        protected override bool ValidatePropertiesAuto( ValidationMode mode )
        {
            if ( this.Point == null )
                return false;

            if ( this.Point != null && this.Point.Length > 1 )
                return false;

            return true;
        }
    }
}