using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XpressNetSharp;

namespace UnitTests
{
    [TestClass]
    public class XpressNetSharpRequestUnitTests
    {
        [TestMethod]
        public async Task AckRespValidTestMethod()
        {
            byte[] expectedMessageBytes = new byte[] { 0x20, 0x20 };

            var testStream = new MemoryStream();
            var msgResp = new AckRespMessage();
            await msgResp.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task ResumeOperationsReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x21, 0x81, 0xA0 };

            var testStream = new MemoryStream();
            var msgReq = new ResumeOperationsReqMessage();
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task StopOperationsReqValidTestMethod()
        {
            byte[] expectedMessageBytes = new byte[] { 0x21, 0x80, 0xA1 };

            var testStream = new MemoryStream();
            var msgReq = new EmergencyStopOperationsReqMessage();
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task StopAllLocosReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x80, 0x80 };

            var testStream = new MemoryStream();
            var msgReq = new EmergencyStopAllLocosReqMessage();
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task StopSingleLocoReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x92, 0x00, 0x03, 0x91 };

            var testStream = new MemoryStream();
            var msgReq = new EmergencyStopSingleLocoReqMessage(3);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }


        //TODO: check value is within bounds [1-8]
        [TestMethod]
        public async Task RegisterModeReadReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x22, 0x11, 0x01, 0x32 };

            var testStream = new MemoryStream();
            var msgReq = new RegisterModeReadReqMessage(1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        //TODO: check value is within bounds [1-256] (256 sent as 00)
        [TestMethod]
        public async Task DirectModeReadReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x22, 0x15, 0x08, 0x3f };

            var testStream = new MemoryStream();
            var msgReq = new DirectModeReadReqMessage(8);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        //TODO: check value is within bounds [1-256] (256 sent as 00)
        [TestMethod]
        public async Task PagedModeReadReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x22, 0x14, 0x08, 0x3e };

            var testStream = new MemoryStream();
            var msgReq = new PagedModeReadReqMessage(8);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }


        [TestMethod]
        public async Task ServiceModeResultsReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x21, 0x10, 0x31 };

            var testStream = new MemoryStream();
            var msgReq = new ServiceModeResultsReqMessage();
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        //TODO: check value is within bounds [1-8]
        [TestMethod]
        public async Task RegisterModeWriteReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x23, 0x12, 0x08, 0x01, 0x38 };

            var testStream = new MemoryStream();
            var msgReq = new RegisterModeWriteReqMessage(8, 1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        //TODO: check value is within bounds [1-256]
        [TestMethod]
        public async Task DirectModeWriteReqValidTestMethod()
        {
            byte[] expectedMessageBytes = new byte[] { 0x23, 0x16, 0x08, 0x01, 0x3c };

            var testStream = new MemoryStream();
            var msgReq = new DirectModeWriteReqMessage(8, 1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        //TODO: check value is within bounds [1-256]
        [TestMethod]
        public async Task PagedModeWriteReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x23, 0x17, 0x08, 0x01, 0x3d };

            var testStream = new MemoryStream();
            var msgReq = new PagedModeWriteReqMessage(8, 1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }


        [TestMethod]
        public async Task CmdStnSoftwareVersionReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x21, 0x21, 0x00};

            var testStream = new MemoryStream();
            var msgReq = new CmdStnSoftwareVersionReqMessage();
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }


        [TestMethod]
        public async Task CmdStnStatusReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x21, 0x24, 0x05 };

            var testStream = new MemoryStream();
            var msgReq = new CmdStnStatusReqMessage();
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        //TODO: test command station power up mode manual
        [TestMethod]
        public async Task CmdStnPowerUpModeReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0x22, 0x22, 0x04, 0x04 };

            var testStream = new MemoryStream();
            var msgReq = new SetCmdStnPowerUpModeMessage(PowerUpMode.Automatic);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        ////TODO: implement
        //[TestMethod]
        //public async Task AccessoryDecoderInformationReqValidTestMethod()
        //{
        //    byte[] expectedMessageBytes = new byte[] { 0x42, 0x22, 0x80, 0xyy };

        //    MemoryStream testStream = new MemoryStream();
        //    var msgReq = new XpressNetAccDecoderInfoReqMessage(88, Nibble.Lower);
        //    await msgReq.WriteXpressNetMessageAsync(testStream);
        //    byte[] actualMessageBytes = ReadStream(testStream);


        //    CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
        //        string.Format("message not valid, expected {0}, received {1}",
        //        ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        //}

        ////TODO: implement
        //[TestMethod]
        //public async Task AccessoryDecoderOperationReqValidTestMethod()
        //{
        //    byte[] expectedMessageBytes = new byte[] { 0x52, 0x22, 0x80, 0xyy };

        //    MemoryStream testStream = new MemoryStream();
        //    var msgReq = new XpressNetAccDecoderOperationsReqMessage(3, AccessoryState.Activate, AccessoryOutput.One);
        //    await msgReq.WriteXpressNetMessageAsync(testStream);
        //    byte[] actualMessageBytes = ReadStream(testStream);


        //    CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
        //        string.Format("message not valid, expected {0}, received {1}",
        //        ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        //}

        //TODO: test numbers out of bounds
        [TestMethod]
        public async Task LocoInfoReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE3, 0x00, 0x00, 0x03, 0xE0 };

            var testStream = new MemoryStream();
            var msgReq = new LocoInfoReqMessage(3);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }


        [TestMethod]
        public async Task FunctionStatusReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE3, 0x07, 0x00, 0x03, 0xE7 };

            var testStream = new MemoryStream();
            var msgReq = new FunctionTypeReqMessage(3);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        //TODO: be sure to test speed for number out of bounds...
        [TestMethod]
        public async Task SpeedAndDirection14ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x10, 0x00, 0x03, 0x8A, 0x7d };

            var testStream = new MemoryStream();
            var msgReq = new SetLocoSpeedAndDirection_SpeedSteps14Message(3, 10, Direction.Forward);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }


        //TODO: be sure to test speed for number out of bounds...
        [TestMethod]
        public async Task SpeedAndDirection27ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x11, 0x00, 0x03, 0x94, 0x62 };

            var testStream = new MemoryStream();
            var msgReq = new SetLocoSpeedAndDirection_SpeedSteps27Message(3, 20, Direction.Forward);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        //TODO: be sure to test speed for number out of bounds...
        [TestMethod]
        public async Task SpeedAndDirection28ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x12, 0x00, 0x03, 0x94, 0x61 };

            var testStream = new MemoryStream();
            var msgReq = new SetLocoSpeedAndDirection_SpeedSteps28Message(3, 20, Direction.Forward);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        //TODO: be sure to test speed for number out of bounds...
        [TestMethod]
        public async Task SpeedAndDirection128ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] {0xE4, 0x13, 0x00, 0x03,  0x94, 0x60};

            var testStream = new MemoryStream();
            var msgReq = new SetLocoSpeedAndDirection_SpeedSteps128Message(3, 20, Direction.Forward);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes, 
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task FunctionOperationInstructionGroup1ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x20, 0x00, 0x03, 0x1F, 0xd8 };

            var testStream = new MemoryStream();
            var msgReq = new SetFunctionOperationInstruction_Group1Message(3, FunctionState.On, FunctionState.On, FunctionState.On, FunctionState.On, FunctionState.On);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task FunctionOperationInstructionGroup1ValidTestMethodXXReal()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x20, 0x00, 0x04, 0x10, 0xD0 };

            var testStream = new MemoryStream();
            var msgReq = new SetFunctionOperationInstruction_Group1Message(4, FunctionState.On, FunctionState.Off, FunctionState.Off, FunctionState.Off, FunctionState.Off);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task FunctionOperationInstructionGroup2ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x21, 0x00, 0x03, 0x0F, 0xC9 };

            var testStream = new MemoryStream();
            var msgReq = new SetFunctionOperationInstruction_Group2Message(3, FunctionState.On, FunctionState.On, FunctionState.On, FunctionState.On);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task FunctionOperationInstructionGroup3ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x22, 0x00, 0x03, 0x0F, 0xCA };

            var testStream = new MemoryStream();
            var msgReq = new SetFunctionOperationInstruction_Group3Message(3, FunctionState.On, FunctionState.On, FunctionState.On, FunctionState.On);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task SetFunctionStateGroup1ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x24, 0x00, 0x03, 0x1F, 0xDC };

            var testStream = new MemoryStream();
            var msgReq = new SetFunctionType_Group1Message(3, FunctionType.OnOff, FunctionType.OnOff, FunctionType.OnOff, FunctionType.OnOff, FunctionType.OnOff);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task SetFunctionStateGroup2ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x25, 0x00, 0x03, 0x0F, 0xCD };

            var testStream = new MemoryStream();
            var msgReq = new SetFunctionType_Group2Message(3, FunctionType.OnOff, FunctionType.OnOff, FunctionType.OnOff, FunctionType.OnOff);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task SetFunctionStateGroup3ValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x26, 0x00, 0x03, 0x0F, 0xCE };

            var testStream = new MemoryStream();
            var msgReq = new SetFunctionType_Group3Message(3, FunctionType.OnOff, FunctionType.OnOff, FunctionType.OnOff, FunctionType.OnOff);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task EstablishDoubleHeaderValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE5, 0x43, 0x00, 0x03, 0x00, 0x04, 0xA1 };

            var testStream = new MemoryStream();
            var msgReq = new EstablishDoubleHeaderMessage(3, 4);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task DesolveDoubleHeaderValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE5, 0x43, 0x00, 0x03, 0x00, 0x00, 0xA5 };

            var testStream = new MemoryStream();
            var msgReq = new DesolveDoubleHeaderMessage(3);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task OperationsModeProgrammingByteModeReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE6, 0x30, 0x00, 0x03, 0xEF, 0xFE, 0x01, 0xC5 };

            var testStream = new MemoryStream();
            var msgReq = new OperationsByteModeWriteReqMessage(3, 1023, 1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task OperationsModeProgrammingBitModeReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE6, 0x30, 0x00, 0x03, 0xE8, 0x07, 0xF9, 0xC3 };

            var testStream = new MemoryStream();
            var msgReq = new OperationsBitModeWriteReqMessage(3, 8, 2, true);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task AddLocoToMultiUnitReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x40, 0x00, 0x03, 0x01, 0xA6 };

            var testStream = new MemoryStream();
            var msgReq = new AddLocoToMultiUnitReqMessage(3, 1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }


        [TestMethod]
        public async Task AddLocoToMultiUnitReq_ReversedDirectionValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x41, 0x00, 0x03, 0x01,0xA7 };

            var testStream = new MemoryStream();
            var msgReq = new AddLocoToMultiUnitReq_ReversedDirectionMessage(3, 1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task RemoveLocoFromMultiUnitReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x42, 0x00, 0x03, 0x01, 0xA4 };

            var testStream = new MemoryStream();
            var msgReq = new RemoveLocoFromMultiUnitReqMessage(3, 1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task AddressInquiryMultiUnitMemberReq_ForwardSearchValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x01, 0x01, 0x00, 0x03, 0xE7 };

            var testStream = new MemoryStream();
            var msgReq = new XpressNetSharp.Requests.LocomotiveFunctions.AddressInquiry.MultiUnitMember.ForwardSearchMessage(1, 3);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task AddressInquiryMultiUnitMemberReq_BackwardSearchValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE4, 0x02, 0x01, 0x00, 0x03, 0xE4 };

            var testStream = new MemoryStream();
            var msgReq = new XpressNetSharp.Requests.LocomotiveFunctions.AddressInquiry.MultiUnitMember.BackwardSearchMessage(1, 3);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task AddressInquiryMultiUnitReq_ForwardSearchValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE2, 0x03, 0x01, 0xE0 };

            var testStream = new MemoryStream();
            var msgReq = new XpressNetSharp.Requests.LocomotiveFunctions.AddressInquiry.MultiUnit.ForwardSearchMessage(1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task AddressInquiryMultiUnitReq_BackwardSearchValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE2, 0x04, 0x01, 0xE7 };

            var testStream = new MemoryStream();
            var msgReq = new XpressNetSharp.Requests.LocomotiveFunctions.AddressInquiry.MultiUnit.BackwardSearchMessage(1);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task AddressInquiryCmdStnLocoStackReq_ForwardSearchValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE3, 0x05, 0x00, 0x03, 0xE5 };

            var testStream = new MemoryStream();
            var msgReq = new XpressNetSharp.Requests.LocomotiveFunctions.AddressInquiry.CmdStnLocoStack.ForwardSearchMessage(3);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task AddressInquiryCmdStnLocoStackReq_BackwardSearchValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE3, 0x06, 0x00, 0x03, 0xE6 };

            var testStream = new MemoryStream();
            var msgReq = new XpressNetSharp.Requests.LocomotiveFunctions.AddressInquiry.CmdStnLocoStack.BackwardSearchMessage(3);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }

        [TestMethod]
        public async Task DeleteLocoFromCmdStnStackReqValidTestMethod()
        {
            var expectedMessageBytes = new byte[] { 0xE3, 0x44, 0x00, 0x03, 0xa4 };

            var testStream = new MemoryStream();
            var msgReq = new DeleteLocoFromCmdStnStackReqMessage(3);
            await msgReq.WriteAsync(testStream);
            var actualMessageBytes = ReadStream(testStream);


            CollectionAssert.AreEqual(expectedMessageBytes, actualMessageBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedMessageBytes), ByteArrayToString(actualMessageBytes)));

        }






        [TestMethod]
        public void LocoHighAddressValid()
        {
            var expectedBytes = new byte[] { 0xC1, 0xF4 };

            var actualBytes = new byte[2];

            ValueConverter.LocoAddress(500, out actualBytes[0], out actualBytes[1]);


            CollectionAssert.AreEqual(expectedBytes, actualBytes,
                string.Format("message not valid, expected {0}, received {1}",
                ByteArrayToString(expectedBytes), ByteArrayToString(actualBytes)));

        }


        /// <summary>
        /// Converts a Byte Array to a string
        /// </summary>
        /// <param name="ba">Byte Array</param>
        /// <returns>String of Hex chars</returns>
        public static string ByteArrayToString(byte[] ba)
        {
            var hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        /// <summary>
        /// Reads a stream and outputs it to a byte array
        /// </summary>
        /// <param name="input">The Stream</param>
        /// <returns>Stream content as a Byte[]</returns>
        public static byte[] ReadStream(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                input.Seek(0, SeekOrigin.Begin);
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
