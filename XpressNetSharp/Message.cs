using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace XpressNetSharp
{
    public class Message : Packet, IObserver<Packet>
    {
        //The following properties are used for returning a response
        private IDisposable _unsubscriber;

        private bool _responseReceived;
        public double Timeout { get; set; }
        public IResponse ResponseData;


        public Message(PacketHeaderType header, IEnumerable<byte> payload) 
            : base(header, payload)
        {
            Timeout = XpressNetConstants.RESPONSE_TIMEOUT;
        }

        public Message(PacketHeaderType headerByte)
            : base(headerByte)
        {
            Timeout = XpressNetConstants.RESPONSE_TIMEOUT;
        }

        static bool IsValueAcceptable(bool val)
        {
            return val;
        }

        bool GetResponseReceivedValue()
        {
            return _responseReceived;
        }
        
        ValueWatcher<bool> _isResponseReceived;


        public async Task<IResponse> WriteAsync(Stream stream, bool returnResponse = false, bool flush = true, CancellationToken token = default(CancellationToken))
        {


            if (returnResponse)
            {
                //subscribe to IObserveable
                PacketRouter router = new PacketRouter();
                Subscribe(router);

                //write the packet to the stream
                //await base.WriteAsync(stream, flush, token);
                await base.WriteAsync(stream, flush, token);

                var ts = TimeSpan.FromSeconds(Timeout);
                //wait for the response
                _isResponseReceived = new ValueWatcher<bool>(GetResponseReceivedValue, IsValueAcceptable);
                await Task.Run(() => _isResponseReceived.Wait(ts), token);

                //Unsubscribe when the correct type of packet has been received or it has timed out
                Unsubscribe();
            }
            else
            {
                //we dont need the return packet so we will just write to the stream and exit
                //await base.WriteAsync(stream, flush, token);
                await base.WriteAsync(stream, flush, token);
            }
            //return the response packet
            return ResponseData;
        }




        protected virtual void Subscribe(IObservable<Packet> provider)
        {
            if (provider != null)
                _unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            Unsubscribe();
        }

        public async virtual void OnError(Exception e)
        {
            // .ToString() is relatively expensive, so only do it if we're logging
            if (XpressNetConstants.XpressNetTraceSource.Switch.ShouldTrace(TraceEventType.Information))
            {
                XpressNetConstants.XpressNetTraceSource.TraceEvent(TraceEventType.Information, 0, "{0}: Cannot get packet from router.", ToString());
            }
            if (Debugger.IsAttached)
            {
                await Console.Out.WriteLineAsync(string.Format("{0}: Cannot get packet from router.", ToString()));
            }
        }

        public virtual void OnNext(Packet packet)
        {
            //when a packet is received, validate it
            if (ValidResponse(packet) || ValidGeneralResponse(packet))
            {
                //if valid set the response data
                ResponseData.Payload = packet.Payload;
                _responseReceived = true; //The right to continue is set here
                if (_isResponseReceived != null) //we might not be expecting a response - but then again if we were not, why has this fired!!!
                {
                    _isResponseReceived.ValueUpdated(true);
                }
            }
        }

        protected virtual void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        protected virtual bool ValidResponse(Packet p)
        {

                return false;
        }

        private bool ValidGeneralResponse(Packet p)
        {

            //TODO: might be better as a case statement!
            if (p.Header == PacketHeaderType.CommandStationOperationResponse &&
                p.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.CmdStnBusy))
            {
                ResponseData = new GeneralResponse();
                return true;
            }
            else if (p.Header == PacketHeaderType.CommandStationOperationResponse &&
                p.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.TransferErrors))
            {
                ResponseData = new GeneralResponse();
                return true;
            }
            else if (p.Header == PacketHeaderType.CommunicationsStateResponse &&
                p.Payload[0] == Convert.ToByte(PacketIdentifier.CommunicationsStateResponse.ResumedAfterError))
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
