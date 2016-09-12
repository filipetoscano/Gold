using System.Xml.Serialization;

namespace Gold
{
    public abstract class Shape
    {
        [XmlIgnore]
        public string ShapeCode
        {
            get;
            set;
        }


        public bool IsValid( ValidationMode mode )
        {
            if ( ValidatePropertiesAuto( mode ) == false )
                return false;

            if ( ValidateProperties( mode ) == false )
                return false;

            return true;
        }


        protected abstract bool ValidatePropertiesAuto( ValidationMode mode );


        protected virtual bool ValidateProperties( ValidationMode mode )
        {
            return true;
        }


        protected bool IsRequired( ValidationMode requiredMode, ValidationMode mode )
        {
            int req = (int) requiredMode;
            int cur = (int) mode;

            return cur >= req;
        }


        public virtual string TextGet()
        {
            return null;
        }
    }
}
