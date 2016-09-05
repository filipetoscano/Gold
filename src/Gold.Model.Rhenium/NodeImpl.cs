namespace Gold.Model.Rhenium
{
    public partial class Node
    {
        public override string ShapeText()
        {
            return $"({ this.ShapeCode })\n{ this.Description }";
        }
    }
}
