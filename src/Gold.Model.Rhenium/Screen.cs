// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class ScreenDefinition : ShapeDefinitionBase<Screen>, IShapeDefinition
    {
        /// <summary />
        public string Name
        { get { return "Screen"; } }

        /// <summary />
        public string FriendlyName
        { get { return "Screen"; } }

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
    public partial class Screen : Shape, IShape
    {
        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public string Notes { get; set; }

        /// <summary />
        public string Moniker { get; set; }

        /// <summary />
        public string ScreenUri { get; set; }

        /// <summary />
        public string Style { get; set; }

        /// <summary />
        protected override bool ValidatePropertiesAuto( ValidationMode mode )
        {
            if ( IsRequired( ValidationMode.Analysis, mode ) == true && this.Description == null )
                return false;

            if ( this.Description != null && this.Description.Length > 100 )
                return false;

            if ( this.Moniker != null && this.Moniker.Length > 100 )
                return false;

            if ( this.ScreenUri != null && this.ScreenUri.Length > 30 )
                return false;

            if ( this.Style != null && this.Style.Length > 30 )
                return false;

            return true;
        }
    }
}