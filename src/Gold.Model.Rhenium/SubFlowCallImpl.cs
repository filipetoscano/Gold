namespace Gold.Model.Rhenium
{
    public partial class SubFlowCall
    {
        public override string TextGet()
        {
            return $"[{ this.Code }] { this.Description }";
        }
    }
}
