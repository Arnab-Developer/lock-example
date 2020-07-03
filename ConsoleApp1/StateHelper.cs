using System;
using System.Threading;

namespace ConsoleApp1
{
    internal class StateHelper
    {
        private int Name { get; set; }

        private StateHelper()
        {
            Name = 0;
        }

        public static StateHelper CurrentInstance
        {
            get { return SingletonCreator.Instance; }
        }

        private class SingletonCreator
        {
            static SingletonCreator() { }

            internal static readonly StateHelper Instance = new StateHelper();
        }

        public void SetObjectToCacheWithLock(int state)
        {
            lock (this)
            {
                Name = state;

                Thread.Sleep(1000);

                if (Name == state)
                {
                    Console.WriteLine("value has not been changed by other thread.");
                }
                else
                {
                    Console.WriteLine("value has been changed by other thread.");
                }
            }
        }
    }
}
