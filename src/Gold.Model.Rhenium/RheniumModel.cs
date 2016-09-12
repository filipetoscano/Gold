using System.Collections.Generic;

namespace Gold.Model.Rhenium
{
    public class RheniumModel : IModelDefinition
    {
        public string Name
        {
            get
            {
                return "Rhenium";
            }
        }


        public List<IShapeDefinition> Shapes
        {
            get
            {
                List<IShapeDefinition> l = new List<IShapeDefinition>();
                l.Add( new FileDefinition() );
                l.Add( new FlowStartDefinition() );
                l.Add( new JumpPointDefinition() );
                l.Add( new JumpToDefinition() );
                l.Add( new NodeDefinition() );
                l.Add( new RedirectDefinition() );
                l.Add( new ReturnDefinition() );
                l.Add( new ScreenDefinition() );
                l.Add( new SubFlowCallDefinition() );
                l.Add( new SubFlowStartDefinition() );
                l.Add( new TransitionDefinition() );

                return l;
            }
        }
    }
}
