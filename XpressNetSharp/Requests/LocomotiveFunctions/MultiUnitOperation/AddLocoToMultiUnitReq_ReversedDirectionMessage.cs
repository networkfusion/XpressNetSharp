using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Add a locomotive to a multi-unit consist, if it is not already contained in a consist currently maintained
    /// by the command station.
    /// </summary>
    /// <remarks>
    /// If this is the first locomotive added then a new consist will be created automatically.
    /// 
    /// When a locomotive is added to a consist, the locomotives relitive direction can be defined. This allows for
    /// head to head, tail to tail or elephant style consists. <seealso cref="AddLocoToMultiUnitReq_ReversedDirectionMessage"/>
    /// </remarks>
    public class AddLocoToMultiUnitReq_ReversedDirectionMessage : Message
    {
        /// <summary>
        /// Add a locomotive to a multi-unit consist, if it is not already contained in a consist currently maintained
        /// by the command station.
        /// </summary>
        /// <remarks>
        /// If this is the first locomotive added then a new consist will be created automatically.
        /// 
        /// When a locomotive is added to a consist, the locomotives relitive direction can be defined. This allows for
        /// head to head, tail to tail or elephant style consists. <seealso cref="AddLocoToMultiUnitReq"/>
        /// 
        /// The mtr and the address should not be the same.
        /// </remarks>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        /// <param name="mtr">The multi-unit identifier (1 - 99)</param>
        public AddLocoToMultiUnitReq_ReversedDirectionMessage(int address, int mtr) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.AddLocoToMultiUnit_ReversedDirection));

            byte[] data = new byte[3];

            if (address == mtr)
                throw new XpressNetProtocolViolationException("mtr and address cannot be equal");

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
