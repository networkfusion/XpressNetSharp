namespace XpressNetSharp
{
	/// <summary>
	/// Tells the command station to operate an accessory decoder
	/// </summary>
	public class AccDecoderOperationsReqMessage : Packet
	{
		/// <summary>
		/// Tells the command station to operate an accessory decoder
		/// </summary>
		/// <param name="address">Address of the accessory decoder</param>
		/// <param name="state">Activate or Deactivate the output</param>
		/// <param name="output">Output of the selected turnout</param>
		public AccDecoderOperationsReqMessage(ushort address, AccessoryState state, AccessoryOutput output)
			: base(PacketHeaderType.AccessoryDecoderOperation)
		{//TODO: Test this function as Accessory Decoders are the least understood!

			byte[] data = new byte[2];

			if (address >= XpressNetConstants.MIN_ACC_DECODER_ADDRESS && address <= XpressNetConstants.MAX_ACC_DECODER_ADDRESS)
			{
				data[0] = (byte)((address - 1) / 4);
				data[1] = (byte)(((address - 1) - data[0] * 4) << 1); //This is this a Modulo optimisation //Modulo optimisation [was (address - 1) % 4;] with a bitshift to move it into the right part of the byte 
			}
			else
				throw new XpressNetProtocolViolationException("Number out of bounds");

			data[1] |= 0x80; //Set the MSB to high

			if (state == AccessoryState.Deactivate)
				data[1] |= 0x08; //Set the 4th nibble to high

			if (output == AccessoryOutput.Two)
				data[1] |= 0x01; //Set the LSB to high


			Payload.AddRange(data);


		}
	}
}
