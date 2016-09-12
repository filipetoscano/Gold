namespace Gold
{
    public class ShapeTransition
    {
        /// <summary>
        /// Gets the source of the transition.
        /// </summary>
        public IShape From
        {
            get;
            set;
        }


        /// <summary>
        /// Gets the transition shape.
        /// </summary>
        public IShape Transition
        {
            get;
            set;
        }


        /// <summary>
        /// Gets the target of the transition.
        /// </summary>
        public IShape To
        {
            get;
            set;
        }
    }
}
