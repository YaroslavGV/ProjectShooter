using System;

namespace Session
{
    public class Duration
    {
        private TimeSpan _accumulatedTime;
        private DateTime _referenceTime;
        private bool _running;

        public Duration ()
        {
            _accumulatedTime = TimeSpan.Zero;
            _referenceTime = DateTime.Now;
            _running = true;
        }

        public TimeSpan Passed
        {
            get
            {
                if (_running)
                    return _accumulatedTime + (DateTime.Now - _referenceTime);
                else
                    return _accumulatedTime;
            }
        }

        public void Pause () 
        {
            _running = false;
            _accumulatedTime += DateTime.Now - _referenceTime;
        }

        public void Resume ()
        {
            _running = false;
            _referenceTime = DateTime.Now;
        }
    }
}