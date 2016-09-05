namespace Gold.Model.Rhenium
{
    public partial class FlowStart
    {
        public override string ShapeText()
        {
            return $"[{ this.Flow }] { this.Description }";
        }
    }
}
