namespace Gold.Model.Rhenium
{
    public partial class Screen
    {
        public override string ShapeText()
        {
            return $"({ this.ShapeCode })\n{ this.Description }";
        }
    }
}
