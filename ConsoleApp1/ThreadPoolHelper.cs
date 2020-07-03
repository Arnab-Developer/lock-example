using System.Threading;

namespace ConsoleApp1
{
    internal class ThreadPoolHelper
    {
        private readonly ManualResetEvent _doneEvent;

        public ThreadPoolHelper(ManualResetEvent doneEvent)
        {
            _doneEvent = doneEvent;
        }

        public void ThreadPoolCallback(object? state)
        {
            if (state == null) return;
            StateHelper.CurrentInstance.SetObjectToCacheWithLock((int)state);
            _doneEvent.Set();
        }
    }
}
