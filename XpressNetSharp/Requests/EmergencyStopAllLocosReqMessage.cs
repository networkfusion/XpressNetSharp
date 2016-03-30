namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to immediately emergency stop all locomotives.
    /// </summary>
    /// <remarks>
    /// The track power remains on so that turnouts can continue to be controlled.
    /// </remarks>
    public class EmergencyStopAllLocosReqMessage : Message
    {
        /// <summary>
        /// Tells the command station to immediately emergency stop all locomotives.
        /// </summary>
        /// <remarks>
        /// The track power remains on so that turnouts can continue to be controlled.
        /// </remarks>
        public EmergencyStopAllLocosReqMessage() 
            : base(PacketHeaderType.EmergencyStopAll) 
        {
        
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.EmergencyStopAll)
            {
                ResponseData = new GeneralResponse();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
