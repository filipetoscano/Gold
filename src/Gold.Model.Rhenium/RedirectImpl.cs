namespace Gold.Model.Rhenium
{
    public partial class Redirect
    {
        public override string TextGet()
        {
            return $"({ this.ShapeCode })\n{ this.Description }";
        }
    }
}
