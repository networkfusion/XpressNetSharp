using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Inquires the Type of a locomotives functions.
    /// </summary>
    public class FunctionTypeReqMessage : Message
    {
        /// <summary>
        /// Inquires the Type of a locomotives functions.
        /// </summary>
        /// <remarks>
        /// XpressNet supports the concept of momentary functions and on/off functions. The command station may maintain
        /// a list of the type of function (whether it is on/off or momentary), however this instruction does not change
        /// the DCC packets sent to the track and is only used as a convenient place to hold this data in order to extend
        /// the user interface. As such it is the responsibility of the XpressNet device that is controlling the 
        /// function to determine the length of time a momentary function is on.
        /// </remarks>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        public FunctionTypeReqMessage(int address) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.FunctionStatus));

            byte[] data = new byte[2];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            Payload.AddRange(data);
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.LocomotiveFunction && packet.Payload[0] == Convert.ToByte(PacketIdentifier.LocomotiveFunctionResponse.FunctionStatus))
            {
                ResponseData = new FunctionTypeResp();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
