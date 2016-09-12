namespace Gold.Model.Rhenium
{
    public partial class SubFlowCall
    {
        public override string TextGet()
        {
            return $"({ this.ShapeCode })\n{ this.Description }\n[{ this.Code }]";
        }
    }
}
