using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Request the command station to transmit back the result of the preceeding read packet.
    /// </summary>
    public class ServiceModeResultsReqMessage : Message
    {
        /// <summary>
        /// Request the command station to transmit back the result of the preceeding read packet.
        /// </summary>
        public ServiceModeResultsReqMessage() 
            : base(PacketHeaderType.CommandStationOperationsRequest) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.ServiceModeResults));
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.CommandStationOperationResponse && 
                packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.ServiceModeForRegisterAndPagedMode))

            {
                ResponseData = new ServiceModeProgrammingResultsResp();
                return true;
            }
            else if (packet.Header == PacketHeaderType.CommandStationOperationResponse && 
                     packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.ServiceModeForDirectCvMode))
            {
                ResponseData = new ServiceModeProgrammingResultsResp();
                return true;
            }
            else if (packet.Header == PacketHeaderType.CommandStationOperationResponse &&
                 packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.ProgrammingInfoDataByteNotFound))
            {
                ResponseData = new GeneralResponse();
                return true;
            }
            else if (packet.Header == PacketHeaderType.CommandStationOperationResponse &&
                     packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.ProgrammingInfoShortCircuit))
            {
                ResponseData = new GeneralResponse();
                return true;
            }
            else if (packet.Header == PacketHeaderType.CommandStationOperationResponse &&
                     packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.ProgrammingInfoCommandStationBusy))
            {
                ResponseData = new GeneralResponse();
                return true;
            }
            else if (packet.Header == PacketHeaderType.CommandStationOperationResponse &&
                     packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.ProgrammingInfoCommandStationReady))
            {
                ResponseData = new GeneralResponse();
                return true;
            }
            else if (packet.Header == PacketHeaderType.CommandStationOperationResponse &&
                     packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.InstructionUnsupported))
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
