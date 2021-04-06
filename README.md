# Use of `lock` in multithreading

This is a demo to show the use of `lock` in multithreading with C#.

There are two methods in class `LockExample.Lib.Utilities.SomeUtility`. One is with `lock` and another without.

```c#
public void PerformSomeOperationWithLock(InputOutputValue inputOutputValues)
{
    lock (this)
    {
        // code here...
    }
}

public void PerformSomeOperationWithNoLock(InputOutputValue inputOutputValues)
{
    // code here...
}
```

There are two test cases are written for each of these in class `LockExample.Test.SomeUtilityTest` to show the operation difference.

```c#
[Fact]
public void PerformSomeOperationWithLockTest()
{
    // code here...
}

[Fact]
public void PerformSomeOperationWithNoLockTest()
{
    // code here...
}
```
