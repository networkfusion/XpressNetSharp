using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Asks the command station to report back the information it contains on a locomotive.
    /// </summary>
    public class LocoInfoReqMessage : Message
    {
        /// <summary>
        /// Asks the command station to report back the information it contains on a locomotive.
        /// </summary>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        public LocoInfoReqMessage(int address) 
            : base(PacketHeaderType.LocomotiveFunction)
        {

            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.LocoInfo));

            byte[] locoAddress = new byte[2];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
               ValueConverter.LocoAddress(address, out locoAddress[0], out locoAddress[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            Payload.AddRange(locoAddress);
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.LocomotiveFunction) //TODO: unit test to check this works 
            {
                ResponseData = new LocoInfoResp_NormalResp(); //TODO: if the loco is in a dh or mu it may return a different resp?! (it is probably better to modify the response to make it generic!
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
