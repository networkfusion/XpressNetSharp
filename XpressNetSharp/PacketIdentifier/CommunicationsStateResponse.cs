using System;

namespace XpressNetSharp.PacketIdentifier
{
    [Flags]
    public enum CommunicationsStateResponse : byte
    {
        /// <summary>
        /// Error occured between the interface and the PC.
        /// </summary>
        TimeoutPc = 0x01,
        /// <summary>
        /// Error occured between the interface and the Command Station.
        /// </summary>
        TimeoutCmdStn = 0x02,
        /// <summary>
        /// Unknown Communications error, awaiting acknowledgement request or Xor byte incorrect.
        /// </summary>
        UnknownError = 0x03,
        /// <summary>
        /// Instruction was sucessfully sent to the command station or normal opperations have
        /// resumed after a timeout.
        /// </summary>
        /// <remarks>
        /// This response can also occur on instructions that dont normally contain a response
        /// such as the speed and direction command.
        /// </remarks>
        ResumedAfterError = 0x04, //TODO: implement this command in one way requests!
        /// <summary>
        /// The command station is no longer providing a timeslot for communication.
        /// </summary>
        TimeslotError = 0x05,
        /// <summary>
        /// Buffer overflow in the command station
        /// </summary>
        BufferOverflow = 0x06

    }
}
