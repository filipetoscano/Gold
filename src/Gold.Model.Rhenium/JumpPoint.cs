// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class JumpPointDefinition : ShapeDefinitionBase<JumpPoint>, IShapeDefinition
    {
        /// <summary />
        public string Name
        { get { return "JumpPoint"; } }

        /// <summary />
        public string FriendlyName
        { get { return "Jump Point"; } }

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
    public partial class JumpPoint : Shape, IShape
    {
        /// <summary />
        public string Point { get; set; }

        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public string Notes { get; set; }

        /// <summary />
        protected override bool ValidatePropertiesAuto( ValidationMode mode )
        {
            if ( this.Point == null )
                return false;

            if ( this.Point != null && this.Point.Length > 1 )
                return false;

            if ( this.Description != null && this.Description.Length > 100 )
                return false;

            return true;
        }
    }
}