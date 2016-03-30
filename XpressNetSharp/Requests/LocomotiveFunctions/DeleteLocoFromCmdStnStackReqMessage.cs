using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Requests the locomotive be removed from the command station.
    /// </summary>
    public class DeleteLocoFromCmdStnStackReqMessage : Packet
    {
        /// <summary>
        /// Requests the locomotive be removed from the command station.
        /// </summary>
        /// <remarks>
        /// The command station database contains information on each locomotive being controlled. Most command stations can only support
        /// a limited number of entries. When the database is full a member of the database must be removed before a new entry can be
        /// added. This problem is more frequently encountered with command stations that have limited RAM.
        /// </remarks>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        public DeleteLocoFromCmdStnStackReqMessage(int address) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.DeleteLocoFromCmdStnStack));

            byte[] data = new byte[2];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            Payload.AddRange(data);
        }
    }
}
