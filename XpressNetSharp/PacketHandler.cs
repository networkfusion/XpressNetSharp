using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace XpressNetSharp
{
    public class BroadcastMessageReceivedEventArgs : EventArgs
    {
        public BroadcastType ResponseType { get; set; }
        public IResponse Response { get; set; }
        public DateTime ReceivedTime { get; set; }
    }

    public static class PacketHandler 

    {
        
        public static event EventHandler<BroadcastMessageReceivedEventArgs> BroadcastMessageReceived = delegate {};

        public static void OnBroadcastMessageReceived(BroadcastMessageReceivedEventArgs e)
        {
            var handler = BroadcastMessageReceived;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        public static Packet ReadPacket(Stream stream)
        {
            var headerBuffer = new byte[1];
            var bytesRead = stream.Read(headerBuffer, 0, 1);
            if (bytesRead == 0)
            {
                return null;
            }

            var headerVal = (byte)(headerBuffer[0] >> 4);
            var payloadLength = (headerBuffer[0] & 0x0F);

            var packet = new Packet((PacketHeaderType)Enum.Parse(typeof(PacketHeaderType), headerVal.ToString()));

            var bytesRemaining = payloadLength;
            var offset = 0;

            if (payloadLength > 0)
            {
                var payloadBytes = new byte[payloadLength];
                do
                {
                    bytesRead = stream.Read(payloadBytes, offset, bytesRemaining);
                    if (bytesRead == 0)
                    {
                        throw new XpressNetProtocolViolationException("Unexpected end of stream");
                    }
                    bytesRemaining -= bytesRead;
                    offset += bytesRead;
                } while (bytesRemaining > 0);
                packet.Payload.AddRange(payloadBytes);

                // .ToString() is relatively expensive, so only do it if we're logging
                if (XpressNetConstants.XpressNetTraceSource.Switch.ShouldTrace(TraceEventType.Verbose))
                {
                    XpressNetConstants.XpressNetTraceSource.TraceEvent(TraceEventType.Verbose, 0, "Received Packet: {0}", packet.ToString());
                }
                if (Debugger.IsAttached)
                {
                    Console.Out.WriteLineAsync(string.Format("Received Packet: {0}", packet));
                }
            }

            //check the xor matches
            var xorByte = new byte[1];
            stream.Read(xorByte, 0, 1);
            if (xorByte[0] != packet.GetXoredByte())
                throw new XpressNetProtocolViolationException("Packet checksum is invalid");


            PacketReceived(packet, stream);

            return packet;
        }


        public static async Task<Packet> ReadPacketAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            var headerBuffer = new byte[1];
            var bytesRead = await stream.ReadAsync(headerBuffer, 0, 1, cancellationToken);
            if (bytesRead == 0)
            {
                return null;
            }

            var headerVal = (byte)(headerBuffer[0] >> 4);
            var payloadLength = (headerBuffer[0] & 0x0F);

            var packet = new Packet((PacketHeaderType)Enum.Parse(typeof(PacketHeaderType), headerVal.ToString()));

            var bytesRemaining = payloadLength;
            var offset = 0;

            if (payloadLength > 0)
            {
                var payloadBytes = new byte[payloadLength];
                do
                {
                    bytesRead = await stream.ReadAsync(payloadBytes, offset, bytesRemaining, cancellationToken);
                    if (bytesRead == 0)
                    {
                        throw new XpressNetProtocolViolationException("Unexpected end of stream");
                    }
                    bytesRemaining -= bytesRead;
                    offset += bytesRead;
                } while (bytesRemaining > 0);
                packet.Payload.AddRange(payloadBytes);

                // .ToString() is relatively expensive, so only do it if we're logging
                if (XpressNetConstants.XpressNetTraceSource.Switch.ShouldTrace(TraceEventType.Verbose))
                {
                    XpressNetConstants.XpressNetTraceSource.TraceEvent(TraceEventType.Verbose, 0, "Received Packet: {0}", packet.ToString());
                }
                if (Debugger.IsAttached)
                {
                    await Console.Out.WriteLineAsync(string.Format("Received Packet: {0}", packet));
                }
            }
            
            //check the xor matches
            //check the xor matches
            var xorByte = new byte[1];
            await stream.ReadAsync(xorByte, 0, 1, cancellationToken);
            if (xorByte[0] != packet.GetXoredByte())
                throw new XpressNetProtocolViolationException("Packet checksum is invalid");


            PacketReceived(packet, stream);
            
            return packet;
        }

        internal async static void PacketReceived(Packet packet, Stream stream)
        {
            
            //Handle Direct return messages : it is possible this should be placed in the packet class!
            if (packet.Header == PacketHeaderType.CommandStationOperationResponse && packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.TransferErrors))
            {
                var ackResp = new AckRespMessage();
                //if (XpressNetConstants.UseAsyncWrite)
                //{
                //    //ackResp.WriteAsync(stream);
                //}
                //else
                //{
                    await ackResp.WriteAsync(stream);
                //}
            }
            
            //Handle Broadcast Messages

            if (packet.Header == PacketHeaderType.EmergencyStopAll) 
            {
                var bmr = new BroadcastMessageReceivedEventArgs
                {
                    ResponseType = BroadcastType.EmergencyStop,
                    ReceivedTime = DateTime.Now
                };
                OnBroadcastMessageReceived(bmr);

                
            } 
            else if (packet.Header == PacketHeaderType.CommandStationOperationResponse && packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.NormalOperationsResumedBroadcast))
            {
                var bmr = new BroadcastMessageReceivedEventArgs
                {
                    ResponseType = BroadcastType.NormalOperationResumed,
                    ReceivedTime = DateTime.Now
                };
                OnBroadcastMessageReceived(bmr);
            }
            else if (packet.Header == PacketHeaderType.CommandStationOperationResponse && packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.TrackPowerOffBroadcast))
            {
                var bmr = new BroadcastMessageReceivedEventArgs
                {
                    ResponseType = BroadcastType.TrackPowerOff,
                    ReceivedTime = DateTime.Now
                };
                OnBroadcastMessageReceived(bmr);
            }
            else if (packet.Header == PacketHeaderType.CommandStationOperationResponse && packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.ServiceModeEntryBroadcast))
            {
                var bmr = new BroadcastMessageReceivedEventArgs
                {
                    ResponseType = BroadcastType.ServiceModeEntry,
                    ReceivedTime = DateTime.Now
                };
                OnBroadcastMessageReceived(bmr);
            }
            else if (packet.Header == PacketHeaderType.AccessoryDecoderInformation)
            {
                var bmr = new BroadcastMessageReceivedEventArgs
                {
                    ResponseType = BroadcastType.AccessoryFeedback,
                    ReceivedTime = DateTime.Now
                };
                //bmr.Response = 
                OnBroadcastMessageReceived(bmr);
            }
            //else if (packet.Header == PacketHeaderType.LocomotiveFunction && packet.Payload[0] == Convert.ToByte(PacketIdentifier.LocomotiveFunctionResponses.LocomotiveOperatedByAnotherDevice))
            //{
            //BroadcastMessageReceivedEventArgs bmr = new BroadcastMessageReceivedEventArgs();
            //bmr.ResponseType = BroadcastType.LocomotiveOperatedByAnotherDevice;
            //bmr.Response = 
            //OnBroadcastMessageReceived(bmr);
            //}
            //else if (packet.Header == PacketHeaderType.LocomotiveFunction && packet.Payload[0] == Convert.ToByte(PacketIdentifier.LocomotiveFunctionResponses.LocomotiveDoubleHeaderOccupied))
            //{
            //BroadcastMessageReceivedEventArgs bmr = new BroadcastMessageReceivedEventArgs();
            //bmr.ResponseType = BroadcastType.DoubleHeaderOccupied;
            //bmr.Response = 
            //OnBroadcastMessageReceived(bmr);
            //}

            //else //we may wish to send the broadcasts on anyway so this else may have to be removed!!
            //{
                var pump = new PacketRouter();
                pump.DisseminatePacket(packet);
            //}
        }
    }
}
