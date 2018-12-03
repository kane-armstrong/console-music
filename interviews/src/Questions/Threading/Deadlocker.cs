using System;
using System.Threading;

namespace Questions.Threading
{
    public class Deadlocker
    {
        private static readonly object LockA = new object();
        private static readonly object LockB = new object();

        public static void SimulateDeadLock()
        {
            var thread1 = new Thread(MethodA);
            var thread2 = new Thread(MethodB);
            thread1.Start();
            thread2.Start();
            Thread.Sleep(0);
        }

        private static void MethodA()
        {
            lock (LockA)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Acquiring lock in MethodA");
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Calling MethodB");
                MethodB();
            }
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Released lock in MethodA");
        }

        private static void MethodB()
        {
            lock (LockB)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Acquiring lock in MethodB");
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Calling MethodA");
                MethodA();
            }
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Released lock in MethodB");
        }
    }
}