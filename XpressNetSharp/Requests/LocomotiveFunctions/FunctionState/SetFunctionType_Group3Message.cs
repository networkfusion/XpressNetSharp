using System;

namespace XpressNetSharp
{
    public class SetFunctionType_Group3Message : Packet
    {
        /// <summary>
        /// Sets whether functions 9 to 12 of the locomotive are momentaty or on/off
        /// </summary>
        /// <remarks>
        /// The type of each function is stored in a database maintained by the command station.
        /// </remarks>
        /// <param name="address">The address of the locomotive (0 - 9999)</param>
        /// <param name="f9">Function 9</param>
        /// <param name="f10">Function 10</param>
        /// <param name="f11">Function 11</param>
        /// <param name="f12">Function 12</param>
        public SetFunctionType_Group3Message(int address, FunctionType f9, FunctionType f10, FunctionType f11, FunctionType f12)
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.SetFunctionState_Group3));

            byte[] data = new byte[3];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] = 0x00;
            //TODO: check if & operator speeds up this function
            if (f12 == FunctionType.OnOff)
                data[2] += 8;

            if (f11 == FunctionType.OnOff)
                data[2] += 4;

            if (f10 == FunctionType.OnOff)
                data[2] += 2;

            if (f9 == FunctionType.OnOff)
                data[2] += 1;

            Payload.AddRange(data);
        }
    }
}
