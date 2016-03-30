using System;

namespace XpressNetSharp.PacketIdentifier
{
    [Flags]
    public enum LocomotiveFunctionResponse : byte
    {
        AddressRetrieval = 0x30,
        LocomotiveOperatedByAnotherDevice = 0x40,
        FunctionStatus = 0x50,
        MultiUnitOrDoubleHeaderError = 0x80
    }
}
