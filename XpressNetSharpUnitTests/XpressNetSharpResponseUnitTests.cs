using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XpressNetSharp;

namespace UnitTests
{
    [TestClass]
    public class XpressNetSharpResponseUnitTests
    {
        //[TestMethod]
        //public async Task NormalOperationsResumedBroadcastTestMethod()
        //{
        //    byte[] MessageBytes = new byte[] { 0x61, 0x01, 0x60 };

        //    MemoryStream testStream = new MemoryStream();
        //    await testStream.WriteAsync(MessageBytes, 0, MessageBytes.Length);
        //    await testStream.FlushAsync();
        //    testStream.Seek(0, SeekOrigin.Begin);
            
        //    var testMsg = XpressNetMessage.ReadXpressNetMessageAsync(testStream);

        //    Assert.IsInstanceOfType(testMsg.Result, typeof(NormalOperationsResumedBroadcastMessage));
        //}

        [TestMethod]
        public async Task CmdStnStatusReqResponseTestMethod()
        {
            byte[] responseMessageBytes = new byte[] { 0x62, 0x22, 0x40, 0x00 };

            var outStream = new MemoryStream();
            var inStream = new MemoryStream(responseMessageBytes);

            
            var testMsg = new CmdStnStatusReqMessage();
            var response = testMsg.WriteAsync(outStream, true);
            await PacketHandler.ReadPacketAsync(inStream);

            Assert.IsInstanceOfType(response.Result, typeof(CmdStnStatusResp));
        }

        [TestMethod]
        public async Task CmdStnSoftwareVersionReqResponseTestMethod()
        {
            const string expectedMessage = "Version: 6.9";
            byte[] responseMessageBytes = new byte[] { 0x63, 0x21, 0x69, 0x01, 0x2a };


            var reqStream = new MemoryStream();
            var respStream = new MemoryStream(responseMessageBytes);

            var msgReq = new CmdStnSoftwareVersionReqMessage();
            var response = msgReq.WriteAsync(reqStream, true);

            await PacketHandler.ReadPacketAsync(respStream);

            var result = string.Empty;
            var resp = response.Result as CmdStnSoftwareVersionResp;
            if (resp != null)
            {
                var newResponse = resp;
                result = string.Format("Version: {0}.{1}", newResponse.Major, newResponse.Minor);
            }

            Assert.AreEqual(expectedMessage, result);

        }

        [TestMethod]
        public async Task ServiceModeResultsReqResponseDirectTestMethod()
        {
            const string expectedMessage = "Mode: Direct, CV: 1, Value: 6";
            byte[] responseMessageBytes = new byte[] { 0x63, 0x14, 0x01, 0x06, 0x70 };


            var reqStream = new MemoryStream();
            var respStream = new MemoryStream(responseMessageBytes);

            var msgReq = new ServiceModeResultsReqMessage();
            var response = msgReq.WriteAsync(reqStream, true);

            await PacketHandler.ReadPacketAsync(respStream);

            var result = string.Empty;
            var resp = response.Result as ServiceModeProgrammingResultsResp;
            if (resp != null)
            {
                var newResponse = resp;
                result = string.Format("Mode: {0}, CV: {1}, Value: {2}", newResponse.Mode, newResponse.RegisterOrCv, newResponse.Value);
            }

            Assert.AreEqual(expectedMessage, result);

        }

        [TestMethod]
        public async Task ServiceModeResultsReqResponsePagedTestMethod()
        {
            const string expectedMessage = "Mode: RegisterOrPaged, CV: 1, Value: 6";
            var responseMessageBytes = new byte[] { 0x63, 0x10, 0x01, 0x06, 0x74 };


            var reqStream = new MemoryStream();
            var respStream = new MemoryStream(responseMessageBytes);

            var msgReq = new ServiceModeResultsReqMessage();
            var response = msgReq.WriteAsync(reqStream, true);

            await PacketHandler.ReadPacketAsync(respStream);

            string result = "";
            var resp = response.Result as ServiceModeProgrammingResultsResp;
            if (resp != null)
            {
                var newResponse = resp;
                result = string.Format("Mode: {0}, CV: {1}, Value: {2}", newResponse.Mode, newResponse.RegisterOrCv, newResponse.Value);
            }

            Assert.AreEqual(expectedMessage, result);

        }
    }
}
