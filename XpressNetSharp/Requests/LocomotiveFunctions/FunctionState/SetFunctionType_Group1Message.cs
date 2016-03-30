using System;

namespace XpressNetSharp
{
        /// <summary>
        /// Sets whether functions 0 to 4 of the locomotive are momentaty or constant on/off
        /// </summary>
    public class SetFunctionType_Group1Message : Packet
    {
        /// <summary>
        /// Sets whether functions 0 to 4 of the locomotive are momentaty or constant on/off
        /// </summary>
        /// <remarks>
        /// The type of each function is stored in a database maintained by the command station.
        /// </remarks>
        /// <param name="address">The address of the locomotive (0 - 9999)</param>
        /// <param name="f0">Function 0</param>
        /// <param name="f1">Function 1</param>
        /// <param name="f2">Function 2</param>
        /// <param name="f3">Function 3</param>
        /// <param name="f4">Function 4</param>
        public SetFunctionType_Group1Message(int address, FunctionType f0, FunctionType f1, FunctionType f2, FunctionType f3, FunctionType f4)
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.SetFunctionState_Group1));

            byte[] data = new byte[3];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] = 0x00;
            //TODO: check if & operator speeds up this function
            if (f0 == FunctionType.OnOff)
                data[2] += 16;

            if (f4 == FunctionType.OnOff)
                data[2] += 8;

            if (f3 == FunctionType.OnOff)
                data[2] += 4;

            if (f2 == FunctionType.OnOff)
                data[2] += 2;

            if (f1 == FunctionType.OnOff)
                data[2] += 1;

            Payload.AddRange(data);
        }
    }
}
