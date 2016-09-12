// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Gold.Model.Rhenium
{
    public class TransitionDefinition : ShapeDefinitionBase<Transition>, IShapeDefinition
    {
        /// <summary />
        public string Name
        { get { return "Transition"; } }

        /// <summary />
        public string FriendlyName
        { get { return "Transition"; } }

        /// <summary />
        public override string ShapeCodePrefix
        { get { return "T"; } }

        /// <summary />
        public override string ShapeCodeFormat
        { get { return "T{1:D4}"; } }

        /// <summary />
        public XmlDocument FormDefinition
        { get { return FormDefinitionLoad(); } }
    }


    [XmlRoot( ElementName = "r" )]
    public partial class Transition : Shape, IShape
    {
        /// <summary />
        public string Event { get; set; }

        /// <summary />
        public string Description { get; set; }

        /// <summary />
        public string Notes { get; set; }

        /// <summary />
        public string TechNotes { get; set; }

        /// <summary />
        protected override bool ValidatePropertiesAuto( ValidationMode mode )
        {
            if ( IsRequired( ValidationMode.Analysis, mode ) == true && this.Event == null )
                return false;

            if ( this.Event != null && this.Event.Length > 30 )
                return false;

            if ( IsRequired( ValidationMode.Analysis, mode ) == true && this.Description == null )
                return false;

            if ( this.Description != null && this.Description.Length > 200 )
                return false;

            return true;
        }
    }
}