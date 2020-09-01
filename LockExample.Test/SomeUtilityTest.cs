using LockExample.Lib.Helpers;
using LockExample.Lib.Models;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace LockExample.Test
{
    public class SomeUtilityTest
    {
        [Fact]
        public void PerformSomeOperationWithLockTest()
        {
            const int counter = 40;
            var doneEvents = new ManualResetEvent[counter];
            IList<InputOutputValue> inputOutputValues = new List<InputOutputValue>();

            for (var loopCounter = 0; loopCounter < counter; loopCounter++)
            {
                doneEvents[loopCounter] = new ManualResetEvent(false);
                var inputOutputValue = new InputOutputValue { Input = loopCounter };
                var helper = new ThreadPoolHelper(doneEvents[loopCounter]);
                ThreadPool.QueueUserWorkItem(helper.ThreadPoolCallbackWithLock, inputOutputValue);
                inputOutputValues.Add(inputOutputValue);
            }

            WaitHandle.WaitAll(doneEvents);

            var inputOutputValueCounter = 0;
            foreach (InputOutputValue inputOutputValue in inputOutputValues)
            {
                if (inputOutputValue.Input == inputOutputValue.Output)
                {
                    inputOutputValueCounter++;
                }
            }
            Assert.Equal(counter, inputOutputValueCounter);
        }

        [Fact]
        public void PerformSomeOperationWithNoLockTest()
        {
            const int counter = 40;
            var doneEvents = new ManualResetEvent[counter];
            IList<InputOutputValue> inputOutputValues = new List<InputOutputValue>();

            for (var loopCounter = 0; loopCounter < counter; loopCounter++)
            {
                doneEvents[loopCounter] = new ManualResetEvent(false);
                var inputOutputValue = new InputOutputValue { Input = loopCounter };
                var helper = new ThreadPoolHelper(doneEvents[loopCounter]);
                ThreadPool.QueueUserWorkItem(helper.ThreadPoolCallbackWithNoLock, inputOutputValue);
                inputOutputValues.Add(inputOutputValue);
            }

            WaitHandle.WaitAll(doneEvents);

            var inputOutputValueCounter = 0;
            foreach (InputOutputValue inputOutputValue in inputOutputValues)
            {
                if (inputOutputValue.Input == inputOutputValue.Output)
                {
                    inputOutputValueCounter++;
                }
            }
            Assert.NotEqual(counter, inputOutputValueCounter);
        }
    }
}
