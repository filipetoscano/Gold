using System.Collections.Generic;

namespace Gold
{
    public interface IModelDefinition
    {
        string Name
        {
            get;
        }


        List<IShapeDefinition> Shapes
        {
            get;
        }
    }
}
