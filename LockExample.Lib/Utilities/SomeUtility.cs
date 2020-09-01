using LockExample.Lib.Models;
using System.Threading;

namespace LockExample.Lib.Utilities
{
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

        public void PerformSomeOperationWithLock(InputOutputValue inputOutputValues)
        {
            lock (this)
            {
                State = inputOutputValues.Input;
                Thread.Sleep(1000);
                inputOutputValues.Output = State;
            }
        }

        public void PerformSomeOperationWithNoLock(InputOutputValue inputOutputValues)
        {
            State = inputOutputValues.Input;
            Thread.Sleep(1000);
            inputOutputValues.Output = State;
        }
    }
}
