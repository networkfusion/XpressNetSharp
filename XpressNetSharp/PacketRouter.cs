using System;
using System.Collections.Generic;
using System.Linq;

namespace XpressNetSharp
{
    public class PacketRouter : IObservable<Packet>
    {

        private static readonly List<IObserver<Packet>> Observers = new List<IObserver<Packet>>();


        public IDisposable Subscribe(IObserver<Packet> observer)
        {
            if (!Observers.Contains(observer))
                Observers.Add(observer);

            return new Unsubscriber(Observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<Packet>> _observers;
            private readonly IObserver<Packet> _observer;

            public Unsubscriber(List<IObserver<Packet>> observers, IObserver<Packet> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void DisseminatePacket(Packet packet)
        {// this can cause a collection cannot be modified exception if someone subscribes or unsubscribes whilst a packet is being disceminated!
              
            //foreach (var observer in observers) 
            //{
            //    if (packet == null)
            //        observer.OnError(new PacketUnknownException());
            //    else
            //        observer.OnNext(packet);
            //}

            //instead we will use a for loop until a better solution is found
            for (var i = Observers.Count - 1; i >= 0; i--)
            {
                if (packet == null)
                {
                    Observers[i].OnError(new PacketUnknownException());
                }
                else if (Observers[i] != null) //packet could be null if the observer has unsubscribed!
                {
                    Observers[i].OnNext(packet);
                }
            }  
        }

        public void EndTransmission()
        {
            foreach (var observer in Observers.ToArray().Where(observer => Observers.Contains(observer)))
            {
                observer.OnCompleted();
            }

            Observers.Clear();
        }
    }

    public class PacketUnknownException : Exception
    {
        internal PacketUnknownException()
        {
        }
    }

}
