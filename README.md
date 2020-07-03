# Lock example in multithreading environment.

I have a simple utility class which is singleton.

    internal class SomeUtility
    {
        private int State { get; set; }

        private SomeUtility()
        {
            State = 0;
        }

        public static SomeUtility CurrentInstance
        {
            get { return SingletonCreator.Instance; }
        }

        private class SingletonCreator
        {
            internal static SomeUtility Instance { get; }

            static SingletonCreator()
            {
                Instance = new SomeUtility();
            }
        }

        public void PerformSomeOperation(int state)
        {
            lock (this)
            {
                State = state;

                Thread.Sleep(1000);

                if (State == state)
                {
                    Console.WriteLine("value has not been changed by other threads.");
                }
                else
                {
                    Console.WriteLine("value has been changed by other threads.");
                }
            }
        }
    }
    
To use this class object in multithreading environment I have another helper class.

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
    
From the main method I have tested it.
    
    using LockExample.ConsoleApp.Helpers;
    using System.Threading;

    namespace LockExample.ConsoleApp
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
    
If I remove the `lock` from the `LockExample.ConsoleApp.Utilities.SomeUtility.PerformSomeOperation()` method then you can see the value of `State` has been changed by other threads. But if I use the `lock` then it is not. 
