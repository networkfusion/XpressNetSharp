using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to remove a locomotive from a multi-unit consist.
    /// </summary>
    /// <remarks>
    /// A locomotive can only be removed from a multi-unit consist it is currently within.
    /// If the consist only contains 1 locomotive the consist will be deleted automatically by the 
    /// command station.
    /// </remarks>
    public class RemoveLocoFromMultiUnitReqMessage : Message
    {
        /// <summary>
        /// Tells the command station to remove a locomotive from a multi-unit consist.
        /// </summary>
        /// <remarks>
        /// A locomotive can only be removed from a multi-unit consist it is currently within.
        /// If the consist only contains 1 locomotive the consist will be deleted automatically by the 
        /// command station.
        /// </remarks>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        /// <param name="mtr">The multi-unit identifier (1 - 99)</param>
        public RemoveLocoFromMultiUnitReqMessage(int address, int mtr) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.RemoveLocoFromMultiUnit));

            byte[] data = new byte[3];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            if (mtr >= XpressNetConstants.MIN_MTR && mtr <= XpressNetConstants.MAX_MTR)
                data[2] = (byte)mtr;
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            Payload.AddRange(data);
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.LocomotiveFunction &&
                (packet.Payload[0] > Convert.ToByte(PacketIdentifier.LocomotiveFunctionResponse.MultiUnitOrDoubleHeaderError) &&
                packet.Payload[0] < (Convert.ToByte(PacketIdentifier.LocomotiveFunctionResponse.MultiUnitOrDoubleHeaderError) + 9))) //TODO: unit test to check this works 
            {
                ResponseData = new SetMultiUnitOrDoubleHeaderErrorResp();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
