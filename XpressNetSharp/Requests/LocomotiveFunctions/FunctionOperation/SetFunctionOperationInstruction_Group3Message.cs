using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Sets the state of functions 9 to 12 of a locomotive
    /// </summary>
    public class SetFunctionOperationInstruction_Group3Message : Packet
    {
        /// <summary>
        /// Sets the state of functions 9 to 12 of a locomotive
        /// </summary>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        /// <param name="f9">Function 9</param>
        /// <param name="f10">Function 10</param>
        /// <param name="f11">Function 11</param>
        /// <param name="f12">Function 12</param>
        public SetFunctionOperationInstruction_Group3Message(int address, FunctionState f9, FunctionState f10, FunctionState f11, FunctionState f12)
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.SetFunctionOperationInstruction_Group3));

            byte[] data = new byte[3];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] = 0x00;

            if (f12 == FunctionState.On)
                data[2] += 8;

            if (f11 == FunctionState.On)
                data[2] += 4;

            if (f10 == FunctionState.On)
                data[2] += 2;

            if (f9 == FunctionState.On)
                data[2] += 1;

            Payload.AddRange(data);
        }
    }
}
