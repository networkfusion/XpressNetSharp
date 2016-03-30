using System.Diagnostics;

namespace XpressNetSharp
{
    public static class XpressNetConstants
    {
        public const int MAX_PACKET_LENGTH = 17; //including header and xor

        public const int MIN_LOCO_ADDRESS = 0;
        public const int MAX_LOCO_ADDRESS = 9999;

        public const int MIN_CV = 1;
        public const int MAX_CV_8_BIT = 256;
        public const int MAX_CV_10_BIT = 1024;

        public const int MIN_ACC_DECODER_ADDRESS = 1;
        public const int MAX_ACC_DECODER_ADDRESS = 1024;

        public const int MIN_MTR = 1;
        public const int MAX_MTR = 99;

        public const int RESPONSE_TIMEOUT = 5;

        //internal static bool UseAsyncWrite = false;
        internal static TraceSource XpressNetTraceSource = new TraceSource("XpressNetTrace");


    }
}
