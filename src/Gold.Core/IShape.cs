namespace Gold
{
    public interface IShape
    {
        string ShapeCode
        {
            get;
            set;
        }

        bool IsValid();

        string TextGet();
    }
}
