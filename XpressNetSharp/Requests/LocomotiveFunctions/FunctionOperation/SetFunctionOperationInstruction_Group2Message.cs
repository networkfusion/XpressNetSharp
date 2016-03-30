using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Sets the state of functions 5 to 8 of a locomotive
    /// </summary>
    public class SetFunctionOperationInstruction_Group2Message : Packet
    {
        /// <summary>
        /// Sets the state of functions 5 to 8 of a locomotive
        /// </summary>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        /// <param name="f5">Function 5</param>
        /// <param name="f6">Function 6</param>
        /// <param name="f7">Function 7</param>
        /// <param name="f8">Function 8</param>
        public SetFunctionOperationInstruction_Group2Message(int address, FunctionState f5, FunctionState f6, FunctionState f7, FunctionState f8)
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.SetFunctionOperationInstruction_Group2));

            byte[] data = new byte[3];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] = 0x00;

            if (f8 == FunctionState.On)
                data[2] += 8;

            if (f7 == FunctionState.On)
                data[2] += 4;

            if (f6 == FunctionState.On)
                data[2] += 2;

            if (f5 == FunctionState.On)
                data[2] += 1;

            Payload.AddRange(data);
        }
    }
}
