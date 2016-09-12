namespace Gold.Model.Rhenium
{
    public partial class SubFlowStart
    {
        public override string TextGet()
        {
            return $"[{ this.Code }] { this.Name }";
        }
    }
}
