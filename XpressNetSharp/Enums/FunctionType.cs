using System;

namespace XpressNetSharp
{
    [Flags]
    public enum FunctionType
    {
        /// <summary>
        /// The function is momentary.
        /// </summary>
        Momentary = 0,
        /// <summary>
        /// The function is on / off
        /// </summary>
        OnOff = 1
    }
}
