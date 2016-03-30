using System;

namespace XpressNetSharp.PacketIdentifier
{
    [Flags]
    public enum LocomotiveFunctionRequest
    {
        LocoInfo = 0x00,
        AddressInquiryMultiUnitMember_ForwardSearch = 0x01,
        AddressInquiryMultiUnitMember_BackwardSearch = 0x02,
        AddressInquiryMultiUnit_ForwardSearch = 0x03,
        AddressInquiryMultiUnit_BackwardSearch = 0x04,
        AddressInquiryCmdStnLocoStack_ForwardSearch = 0x05,
        AddressInquiryCmdStnLocoStack_BackwardSearch = 0x06,
        FunctionStatus = 0x07,
        FunctionStatus13to28 = 0x08, //3.6 (Unimplemented)
        FunctionLevel13to28 = 0x09, //3.6 (Unimplemented)
        SetLocoSpeedAndDirection_SpeedSteps14 = 0x10,
        SetLocoSpeedAndDirection_SpeedSteps27 = 0x11,
        SetLocoSpeedAndDirection_SpeedSteps28 = 0x12,
        SetLocoSpeedAndDirection_SpeedSteps128 = 0x13,
        SetFunctionOperationInstruction_Group1 = 0x20,
        SetFunctionOperationInstruction_Group2 = 0x21,
        SetFunctionOperationInstruction_Group3 = 0x22,
        SetFunctionOperationInstruction_Group4 = 0x23, //3.6
        SetFunctionState_Group1 = 0x24,
        SetFunctionState_Group2 = 0x25,
        SetFunctionState_Group3 = 0x26,
        SetFunctionState_Group4 = 0x27, //3.6 (Unimplemented)
        SetFunctionOperationInstruction_Group5 = 0x28, //3.6
        SetFunctionState_Group5 = 0x2c, //3.6 (Unimplemented)
        RefreshFunctionMode = 0x2f, //3.6 (Unimplemented)
        OperationsModeWrite = 0x30,
        AddLocoToMultiUnit = 0x40,
        AddLocoToMultiUnit_ReversedDirection = 0x41,
        RemoveLocoFromMultiUnit = 0x42,
        EstablishOrDesolveDoubleHeader = 0x43,
        DeleteLocoFromCmdStnStack = 0x44,
    }
}
