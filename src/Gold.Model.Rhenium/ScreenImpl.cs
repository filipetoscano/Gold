namespace Gold.Model.Rhenium
{
    public partial class Screen
    {
        public override string TextGet()
        {
            return $"({ this.ShapeCode })\n{ this.Description }";
        }
    }
}
