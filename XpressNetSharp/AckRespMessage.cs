namespace XpressNetSharp
{
    /// <summary>
    /// If the device receives an acknowledgement request it must answer with a response.
    /// </summary>
    /// <remarks>
    /// The command station will continue to send an acknowledgement request and will allow
    /// no other types of communication until a response to the request is received.
    /// </remarks>
    public class AckRespMessage : Packet
    {
        /// <summary>
        /// If the device receives an acknowledgement request it must answer with a response.
        /// </summary>
        /// <remarks>
        /// The command station will continue to send an acknowledgement request and will allow
        /// no other types of communication until a response to the request is received.
        /// </remarks>
        public AckRespMessage() : base(PacketHeaderType.CommandStationOperationsRequest) { }
    }
}
