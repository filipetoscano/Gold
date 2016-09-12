namespace Gold.Model.Rhenium
{
    public partial class File
    {
        public override string TextGet()
        {
            return $"({ this.ShapeCode })\n{ this.Description }";
        }
    }
}
