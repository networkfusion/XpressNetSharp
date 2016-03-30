namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to respond with an accessory decoder status.
    /// </summary>
    public class AccDecoderInfoReqMessage : Message
    {//TODO: Add documentation!
        public AccDecoderInfoReqMessage(ushort address)
            : base(PacketHeaderType.AccessoryDecoderInformation)
        {//TODO: Test this function as Accessory Decoders are the least understood!
            byte[] data = new byte[2];

            // compute address byte field
            if (address >= XpressNetConstants.MIN_ACC_DECODER_ADDRESS && address <= XpressNetConstants.MAX_ACC_DECODER_ADDRESS)
                data[0] = (byte)((address-1) / 4);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            // The MSB of the upper nibble is required to be set on
            // The rest of the upper nibble should be zeros.
            data[1] = 0x80;

            // The LSB of the lower nibble says weather or not the
            // information request is for the upper or lower nibble.
            int remainder = ((address - 1) -  data[0] * 4); //Modulo optimisation [was (address - 1) % 4;]

            if (remainder > 1)//higher nibble would be greater than 1.
                data[1] |= 0x01; //set high nibble


            Payload.AddRange(data);
        }

        protected override bool ValidResponse(Packet packet)
        {
            //if (packet.Header == PacketHeaderType.EmergencyStopAll)
            //{
            //    ResponseData = new StateResponse();
            //    return true;
            //}
            //else
            //{
                return false;
            //}
        }
    }
}
