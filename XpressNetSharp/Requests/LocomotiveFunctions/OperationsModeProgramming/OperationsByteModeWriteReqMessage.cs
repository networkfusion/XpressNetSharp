using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to write the value to a locomotive whilst in operations mode.
    /// </summary>
    /// <remarks>
    /// This function will return an exception if you try to set the locomotives address.
    /// 
    /// Some command stations donot support this command and will respond with a response of "Instruction not supported".
    /// </remarks>
    public class OperationsByteModeWriteReqMessage : Packet
    {
        /// <summary>
        /// Tells the command station to write the value to a locomotive whilst in operations mode.
        /// </summary>
        /// <remarks>
        /// This function will return an exception if you try to set the locomotives address.
        /// 
        /// Some command stations do not support this command and will respond with a response of "Instruction not supported".
        /// </remarks>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        /// <param name="cv">CV (1 - 1024)</param>
        /// <param name="value">Value to write</param>
        public OperationsByteModeWriteReqMessage(int address, int cv, int value) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {

            if (cv == 1 || cv == 17 || cv == 18)
                throw new XpressNetProtocolViolationException("Changing Locomotive Address not permitted in this mode");

            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.OperationsModeWrite));

            byte[] data = new byte[5];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");


            if (cv >= XpressNetConstants.MIN_CV && cv <= XpressNetConstants.MAX_CV_10_BIT)
            {
                var convertedCv = (ushort)(cv - 1);
                ValueConverter.FromUShort(convertedCv, out data[2], out data[3]);
            }
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] += 0xEC;

            if (value >= 0 && value < 256)
                data[4] = (byte)value;
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            Payload.AddRange(data);
        }


        //TODO: can receive an instruction not supported response!
    }
}
