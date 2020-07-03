using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main()
        {
            const int counter = 10;
            var doneEvents = new ManualResetEvent[counter];

            for (var i = 0; i < counter; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new ThreadPoolHelper(doneEvents[i]).ThreadPoolCallback, i);
            }

            WaitHandle.WaitAll(doneEvents);
        }
    }
}
