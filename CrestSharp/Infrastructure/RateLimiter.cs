using System;
using System.Collections.Generic;
using System.Threading;

namespace CrestSharp.Infrastructure
{
    class RateLimiter
    {
        private readonly Queue<DateTime> _requestTimes;
        private readonly int _maxRequests;
        private readonly TimeSpan _timeSpan;

        public RateLimiter(int maxRequests, TimeSpan timeSpan)
        {
            _maxRequests = maxRequests;
            _timeSpan = timeSpan;
            _requestTimes = new Queue<DateTime>(maxRequests);
        }

        private void SynchronizeQueue()
        {
            var limit = DateTime.UtcNow.Subtract(_timeSpan);
            while ((_requestTimes.Count > 0) && (_requestTimes.Peek()<limit))
            {
                _requestTimes.Dequeue();
            }
        }

        private bool CanRequestNow()
        {
            SynchronizeQueue();
            return _requestTimes.Count < _maxRequests;
        }

        public void EnqueueRequest()
        {
            while (!CanRequestNow())
            {
                Thread.Sleep(
                             _requestTimes.Peek()
                                 .Add(_timeSpan)
                                 .Subtract(DateTime.UtcNow));
            }

            _requestTimes.Enqueue(DateTime.UtcNow);
        }
    }
}