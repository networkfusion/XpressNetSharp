using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to remove a locomotive from the double header in which it is currently merged
    /// </summary>
    /// <remarks>
    /// This will unlink the locomotives in the command station.
    /// </remarks>
    public class DesolveDoubleHeaderMessage : Message
    {
        /// <summary>
        /// Tells the command station to remove a locomotive from the double header in which it is currently merged
        /// </summary>
        /// <remarks>
        /// This will unlink the locomotives in the command station.
        /// </remarks>
        /// <param name="locoAddress">Address of the locomotive (0 - 9999)</param>
        public DesolveDoubleHeaderMessage(int locoAddress) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.EstablishOrDesolveDoubleHeader));

            byte[] data = new byte[4];

            if (locoAddress >= XpressNetConstants.MIN_LOCO_ADDRESS && locoAddress <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(locoAddress, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] = 0x00;
            data[3] = 0x00;

            Payload.AddRange(data);
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.LocomotiveFunction && (packet.Payload[0] > 0x80 && packet.Payload[0] < 0x89)) //TODO: unit test to check this works
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
