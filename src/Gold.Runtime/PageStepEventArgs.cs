using System;

namespace Gold.Runtime
{
    public class PageStepEventArgs : EventArgs
    {
        public PageStepEventArgs( int stepIndex, int stepCount, string description )
        {
            #region Validations

            if ( description == null )
                throw new ArgumentNullException( nameof( description ) );

            if ( stepIndex < 0 )
                throw new ArgumentOutOfRangeException( nameof( stepIndex ), "Must be positive" );

            if ( stepCount < 1 )
                throw new ArgumentOutOfRangeException( nameof( stepCount ), "Must be positive" );

            if ( stepIndex > stepCount )
                throw new ArgumentOutOfRangeException( nameof( stepIndex ), "Must be less-than-equal to stepCount" );

            #endregion

            this.Description = description;
            this.StepIndex = stepIndex;
            this.StepCount = stepCount;
        }

        
        /// <summary>
        /// Description of the current step.
        /// </summary>
        public string Description
        {
            get;
            private set;
        }


        /// <summary>
        /// Index of the current step.
        /// </summary>
        public int StepIndex
        {
            get;
            private set;
        }


        /// <summary>
        /// Total number of steps.
        /// </summary>
        public int StepCount
        {
            get;
            private set;
        }
    }
}

/* eof */
