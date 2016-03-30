using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Sets the state of functions 13 to 20 of a locomotive
    /// </summary>
    public class SetFunctionOperationInstruction_Group4Message : Packet
    {
        /// <summary>
        /// Sets the state of functions 13 to 20 of a locomotive
        /// XpressNet 3.6 and later
        /// </summary>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        /// <param name="f13">Function 9</param>
        /// <param name="f14">Function 10</param>
        /// <param name="f15">Function 11</param>
        /// <param name="f16">Function 12</param>
        public SetFunctionOperationInstruction_Group4Message(int address, FunctionState f13, FunctionState f14, FunctionState f15, FunctionState f16, FunctionState f17, FunctionState f18, FunctionState f19, FunctionState f20)
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.SetFunctionOperationInstruction_Group4));

            byte[] data = new byte[3];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] = 0x00;

            if (f20 == FunctionState.On)
                data[2] += 128;

            if (f19 == FunctionState.On)
                data[2] += 64;

            if (f18 == FunctionState.On)
                data[2] += 32;

            if (f17 == FunctionState.On)
                data[2] += 16;

            if (f16 == FunctionState.On)
                data[2] += 8;

            if (f15 == FunctionState.On)
                data[2] += 4;

            if (f14 == FunctionState.On)
                data[2] += 2;

            if (f13 == FunctionState.On)
                data[2] += 1;

            Payload.AddRange(data);
        }
    }
}
