namespace Gold
{
    public enum ValidationMode
    {
        /// <summary>
        /// Minimum set of properties, required only while visually authoring
        /// the image.
        /// </summary>
        Analysis = 0,

        /// <summary>
        /// Minimum sub-set of properties, required to prototype the DSL.
        /// </summary>
        Prototype = 1,

        /// <summary>
        /// Maximum set of properties which are required in runtime.
        /// </summary>
        Live = 2
    }
}
