using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Sets whether functions 5 to 8 of the locomotive are momentaty or on/off
    /// </summary>
    public class SetFunctionType_Group2Message : Packet
    {
        /// <summary>
        /// Sets whether functions 5 to 8 of the locomotive are momentaty or on/off
        /// </summary>
        /// <remarks>
        /// The type of each function is stored in a database maintained by the command station.
        /// </remarks>
        /// <param name="address">The address of the locomotive (0 - 9999)</param>
        /// <param name="f5">Function 5</param>
        /// <param name="f6">Function 6</param>
        /// <param name="f7">Function 7</param>
        /// <param name="f8">Function 8</param>
        public SetFunctionType_Group2Message(int address, FunctionType f5, FunctionType f6, FunctionType f7, FunctionType f8)
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.SetFunctionState_Group2));

            byte[] data = new byte[3];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] = 0x00;
            //TODO: check if & operator speeds up this function
            if (f8 == FunctionType.OnOff)
                data[2] += 8;

            if (f7 == FunctionType.OnOff)
                data[2] += 4;

            if (f6 == FunctionType.OnOff)
                data[2] += 2;

            if (f5 == FunctionType.OnOff)
                data[2] += 1;

            Payload.AddRange(data);
        }
    }
}
