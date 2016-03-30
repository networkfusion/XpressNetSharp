using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Sets the state of functions 21 to 28 of a locomotive
    /// </summary>
    public class SetFunctionOperationInstruction_Group5Message : Packet
    {
        /// <summary>
        /// Sets the state of functions 21 to 28 of a locomotive
        /// XpressNet 3.6 and later
        /// </summary>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        /// <param name="f21">Function 9</param>
        /// <param name="f22">Function 10</param>
        /// <param name="f23">Function 11</param>
        /// <param name="f24">Function 12</param>
        public SetFunctionOperationInstruction_Group5Message(int address, FunctionState f21, FunctionState f22, FunctionState f23, FunctionState f24, FunctionState f25, FunctionState f26, FunctionState f27, FunctionState f28)
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.SetFunctionOperationInstruction_Group5));

            byte[] data = new byte[3];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] = 0x00;

            if (f28 == FunctionState.On)
                data[2] += 128;

            if (f27 == FunctionState.On)
                data[2] += 64;

            if (f26 == FunctionState.On)
                data[2] += 32;

            if (f25 == FunctionState.On)
                data[2] += 16;

            if (f24 == FunctionState.On)
                data[2] += 8;

            if (f23 == FunctionState.On)
                data[2] += 4;

            if (f22 == FunctionState.On)
                data[2] += 2;

            if (f21 == FunctionState.On)
                data[2] += 1;

            Payload.AddRange(data);
        }
    }
}
