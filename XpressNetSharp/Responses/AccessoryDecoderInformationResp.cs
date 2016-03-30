using System.Collections.Generic;

namespace XpressNetSharp
{
    public enum AccessoryType
    {
        /// <summary>
        /// An accessory decoder without feedback.
        /// </summary>
        AccessoryDecoderNoFeedback = 0,
        /// <summary>
        /// An accessory decoder with feedback.
        /// </summary>
        AccessoryDecoderWithFeedback = 1,
        /// <summary>
        /// A feedback module.
        /// </summary>
        FeedbackModule = 3,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        Reserved = 4
    }

    public enum AccessoryStatus
    {
        /// <summary>
        /// Turnout has not been controlled during this operating session
        /// </summary>
        TurnoutNotOperated,
        /// <summary>
        /// Last command sent was "0" [explain more!]
        /// </summary>
        LastCommand0,
        /// <summary>
        /// Last command sent was "1" [explain more!]
        /// </summary>
        LastCommand1,
        /// <summary>
        /// Invalid combination (both end switches of a turnout with feedback are indicating they are active)
        /// </summary>
        InvalidCombination

    }
    public class AccessoryDecoderInformationResp : IResponse
    {// the response will contain the response for 2 turnouts in a group, we must handle this somehow!!!
        public IList<byte> Payload { get; set; }

        //TODO: potentially populate an array of 2 entities containing the following information:
        public int Address { get {return ((Payload[0] + 1)*4) ;} } //needs to take into account the nibble
        public AccessoryType Type { get; private set; }
        public bool Success { get; private set; }
        public AccessoryStatus Status { get; private set; }
        //public AccessoryStatus state1 { get; private set; }

    }
}
