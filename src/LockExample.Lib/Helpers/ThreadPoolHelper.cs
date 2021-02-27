using LockExample.Lib.Models;
using LockExample.Lib.Utilities;
using System.Threading;

namespace LockExample.Lib.Helpers
{
    public class ThreadPoolHelper
    {
        private ManualResetEvent DoneEvent { get; }

        public ThreadPoolHelper(ManualResetEvent doneEvent)
        {
            DoneEvent = doneEvent;
        }

        public void ThreadPoolCallbackWithLock(object? inputOutputValues)
        {
            if (inputOutputValues == null) return;
            SomeUtility.CurrentInstance.PerformSomeOperationWithLock((InputOutputValue)inputOutputValues);
            DoneEvent.Set();
        }

        public void ThreadPoolCallbackWithNoLock(object? inputOutputValues)
        {
            if (inputOutputValues == null) return;
            SomeUtility.CurrentInstance.PerformSomeOperationWithNoLock((InputOutputValue)inputOutputValues);
            DoneEvent.Set();
        }
    }
}
