using System;

namespace XpressNetSharp
{
    [Flags]
    public enum PowerUpMode : byte
    {
        /// <summary>
        /// No speed and direction commands are sent to the locomotives on power up.
        /// </summary>
        Manual = 0x00,
        /// <summary>
        /// The speed, direction and function status is sent to all known locomotives on power up
        /// </summary>
        Automatic = 0x04
    }
}
