using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Sets the state of functions 0 to 4 of a locomotive
    /// </summary>
    public class SetFunctionOperationInstruction_Group1Message : Packet
    {
        /// <summary>
        /// Sets the state of functions 0 to 4 of a locomotive
        /// </summary>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        /// <param name="f0">Function 0</param>
        /// <param name="f1">Function 1</param>
        /// <param name="f2">Function 2</param>
        /// <param name="f3">Function 3</param>
        /// <param name="f4">Function 4</param>
        public SetFunctionOperationInstruction_Group1Message(int address, FunctionState f0, FunctionState f1, FunctionState f2, FunctionState f3, FunctionState f4) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.SetFunctionOperationInstruction_Group1));

            byte[] data = new byte[3];

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            data[2] = 0x00;

            if (f0 == FunctionState.On)
                data[2] += 16;

            if (f4 == FunctionState.On)
                data[2] += 8;

            if (f3 == FunctionState.On)
                data[2] += 4;

            if (f2 == FunctionState.On)
                data[2] += 2;

            if (f1 == FunctionState.On)
                data[2] += 1;

            Payload.AddRange(data);
        }
    }
}
