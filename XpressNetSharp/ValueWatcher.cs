using System;
using System.Threading;

namespace XpressNetSharp
{
    public class ValueWatcher<TValue> : IDisposable
    {
        readonly ManualResetEvent _ev = new ManualResetEvent(false);
        readonly Func<TValue, bool> _isValueAcceptableFunc;

        public ValueWatcher(Func<TValue> currentValueFunc, Func<TValue, bool> isValueAcceptableFunc)
        {
            _isValueAcceptableFunc = isValueAcceptableFunc;
            ValueUpdated(currentValueFunc.Invoke());
        }

        public void ValueUpdated(TValue value)
        {
            if (_isValueAcceptableFunc.Invoke(value))
                _ev.Set();
            else
                _ev.Reset();
        }

        public bool Wait()
        {
            return _ev.WaitOne();
        }

        public bool Wait(int timeoutMs)
        {
            return _ev.WaitOne(timeoutMs);
        }

        public bool Wait(TimeSpan ts)
        {
            return _ev.WaitOne(ts);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ev.Dispose();
            }
        }

        #endregion
    }
}
