using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to join 2 locomotives as a double header.
    /// </summary>
    /// <remarks>
    /// This means that speed and direction changes are sent to both locomotives by the command station
    /// whenever the speed and/or direction changes on either locomotive.
    /// </remarks>
    public class EstablishDoubleHeaderMessage : Message
    {
        /// <summary>
        /// Tells the command station to join 2 locomotives as a double header.
        /// </summary>
        /// <remarks>
        /// This means that speed and direction changes are sent to both locomotives by the command station
        /// whenever the speed and/or direction changes on either locomotive.
        /// </remarks>
        /// <param name="masterLocoAddress">Address of the master locomotive (0 - 9999)</param>
        /// <param name="slaveLocoAddress">Address of the slave locomotive(0 - 9999)</param>
        public EstablishDoubleHeaderMessage(int masterLocoAddress, int slaveLocoAddress) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.EstablishOrDesolveDoubleHeader));

            if (masterLocoAddress != slaveLocoAddress)
            {
                byte[] data = new byte[4];

                if (masterLocoAddress >= XpressNetConstants.MIN_LOCO_ADDRESS && masterLocoAddress <= XpressNetConstants.MAX_LOCO_ADDRESS)
                    ValueConverter.LocoAddress(masterLocoAddress, out data[0], out data[1]);
                else
                    throw new XpressNetProtocolViolationException("Number out of bounds");


                if (slaveLocoAddress >= XpressNetConstants.MIN_LOCO_ADDRESS && slaveLocoAddress <= XpressNetConstants.MAX_LOCO_ADDRESS)
                    ValueConverter.LocoAddress(slaveLocoAddress, out data[2], out data[3]);
                else
                    throw new XpressNetProtocolViolationException("Number out of bounds");

                Payload.AddRange(data);
            }
            else
                throw new XpressNetProtocolViolationException("Addresses cannot be equal");
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
