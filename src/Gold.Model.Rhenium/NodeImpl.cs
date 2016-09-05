namespace Gold.Model.Rhenium
{
    public partial class Node
    {
        public override string TextGet()
        {
            return $"({ this.ShapeCode })\n{ this.Description }";
        }
    }
}
