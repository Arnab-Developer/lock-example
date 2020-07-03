using LockExample.ConsoleApp.Utilities;
using System.Threading;

namespace LockExample.ConsoleApp.Helpers
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
            SomeUtility.CurrentInstance.PerformSomeOperation((int)state);
            _doneEvent.Set();
        }
    }
}
