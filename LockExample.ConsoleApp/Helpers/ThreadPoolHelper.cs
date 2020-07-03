using LockExample.ConsoleApp.Utilities;
using System.Threading;

namespace LockExample.ConsoleApp.Helpers
{
    internal class ThreadPoolHelper
    {
        private ManualResetEvent DoneEvent { get; }

        public ThreadPoolHelper(ManualResetEvent doneEvent)
        {
            DoneEvent = doneEvent;
        }

        public void ThreadPoolCallback(object? state)
        {
            if (state == null) return;
            SomeUtility.CurrentInstance.PerformSomeOperation((int)state);
            DoneEvent.Set();
        }
    }
}
