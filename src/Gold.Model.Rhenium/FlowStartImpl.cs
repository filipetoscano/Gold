﻿namespace Gold.Model.Rhenium
{
    public partial class FlowStart
    {
        public override string TextGet()
        {
            return $"[{ this.Code }] { this.Name }";
        }
    }
}
