using System;
using System.Threading;

namespace LockExample.ConsoleApp.Utilities
{
    internal class SomeUtility
    {
        private int Name { get; set; }

        private SomeUtility()
        {
            Name = 0;
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
                Name = state;

                Thread.Sleep(1000);

                if (Name == state)
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
}
