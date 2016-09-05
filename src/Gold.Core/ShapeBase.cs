using System.Xml.Serialization;

namespace Gold
{
    public class ShapeBase
    {
        [XmlIgnore]
        public string ShapeCode
        {
            get;
            set;
        }


        public virtual bool IsValid()
        {
            return true;
        }


        public virtual string TextGet()
        {
            return null;
        }
    }
}
