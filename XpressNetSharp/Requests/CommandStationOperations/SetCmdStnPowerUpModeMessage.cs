using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Sets the starting mode of the command station when it is powered up.
    /// </summary>
    public class SetCmdStnPowerUpModeMessage : Packet
    {
        /// <summary>
        /// Sets the starting mode of the command station when it is powered up.
        /// </summary>
        /// <remarks>
        /// Not all command stations support this request.
        /// </remarks>
        /// <param name="powerUpMode"></param>
        public SetCmdStnPowerUpModeMessage(PowerUpMode powerUpMode) 
            : base(PacketHeaderType.CommandStationOperationsRequest) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.PowerUpMode));
            Payload.Add((byte)powerUpMode);

        }
    }
}
