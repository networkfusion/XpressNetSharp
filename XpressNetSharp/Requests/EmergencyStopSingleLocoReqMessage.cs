namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to immediatly stop a locomotive without using its preprogrammed deceleration rate.
    /// </summary>
    /// <remarks>
    /// The track power remains on so that other locomotives and turnouts can be controlled.
    /// 
    /// The spec says the loco address is 1 - 99, however I am sure this is a typo.
    /// </remarks>
    public class EmergencyStopSingleLocoReqMessage : Packet
    {
        /// <summary>
        /// Tells the command station to immediatly stop a locomotive without using its preprogrammed deceleration rate.
        /// </summary>
        /// <remarks>
        /// The track power remains on so that other locomotives and turnouts can be controlled.
        /// 
        /// The spec says the loco address is 1 - 99, however I am sure this is a typo.
        /// </remarks>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        public EmergencyStopSingleLocoReqMessage(int address) 
            : base(PacketHeaderType.EmergencyStopSingleLoco) 
        {
            byte[] data = new byte[2];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            Payload.AddRange(data);
        }
    }
}
