using System.Collections.Generic;

namespace XpressNetSharp
{
    public class CmdStnStatusResp : IResponse
    {
        public IList<byte> Payload { get; set; }

        public bool IsEmergencyOff { get { return GetBit(Payload[1], 1); } }
        public bool IsEmergencyStop { get { return GetBit(Payload[1], 2); } }
        public PowerUpMode StartMode { get { return GetStartMode(GetBit(Payload[1], 3)); } }
        public bool IsInServiceMode { get { return GetBit(Payload[1], 4); } }
        public bool IsPoweringUp { get { return GetBit(Payload[1], 7); } }
        public bool RamError { get { return GetBit(Payload[1], 8); } }
        public bool AwaitingStartMode { get { return GetAwaitingStartMode(); } }

        private bool GetBit(byte b, int bitNumber)
        {
            var bit = (b & (1 << bitNumber - 1)) != 0;
            return bit;
        }

        private PowerUpMode GetStartMode(bool b)
        {
            if (b)
            {
                return PowerUpMode.Automatic;
            }
            else
            {
                return PowerUpMode.Manual;
            }
        }

        private bool GetAwaitingStartMode()
        {
            if (StartMode == PowerUpMode.Automatic && IsPoweringUp)
                return true;
            else
                return false;
        }

    }
}
