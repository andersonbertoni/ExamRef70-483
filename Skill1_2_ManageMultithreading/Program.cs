using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Skill1_2_ManageMultithreading
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            SingleTaskSumming = 1,
            BadTaskInteraction = 2,
            UsingLocking = 3,
            SensibleLocking = 4,
            UsingMonitors = 5,
            SequentialLocking = 6,
            DeadlockedTasks = 7,
            InterlockTotal = 8,
            CancelATask = 9,
            CancelWithException = 10
        }
        static void Main(string[] args)
        {
            bool run = true;

            while (run)
            {
                Items item = Items.None;
                CleanVariables();
                                
                int i = ConsoleFunctions.ShowMenuOptions<Items>();

                if (i == 99)
                {
                    item = Items.Sair;
                }
                else
                {
                    item = (Items)Enum.Parse(typeof(Items), i.ToString());
                }

                switch (item)
                {
                    case Items.SingleTaskSumming:
                        SingleTaskSummingExample();
                        break;
                    case Items.BadTaskInteraction:
                        BadTaskInteractionExample();
                        break;
                    case Items.UsingLocking:
                        UsingLockingExample();
                        break;
                    case Items.SensibleLocking:
                        SensibleLockingExample();
                        break;
                    case Items.UsingMonitors:
                        UsingMonitorsExample();
                        break;
                    case Items.SequentialLocking:
                        SequentialLockingExample();
                        break;
                    case Items.DeadlockedTasks:
                        DeadlockedTasksExample();
                        break;
                    case Items.InterlockTotal:
                        InterlockTotalExample();
                        break;
                    case Items.CancelATask:
                        CancelATaskExample();
                        break;
                    case Items.CancelWithException:
                        CancelWithExceptionExample();
                        break;
                    case Items.Sair:
                        run = false;
                        break;
                }

                if (run)
                {
                    Console.WriteLine("Finished processing. Press a key to end.");
                    Console.ReadKey();
                }
            }
        }       

        private static void CleanVariables()
        {
            sharedTotal = 0;
            cancelationTokenSource = new CancellationTokenSource();
        }

        #region Variables

        static int[] items = Enumerable.Range(0, 50000001).ToArray();
        static long sharedTotal;
        static object sharedTotalLock = new object();
        static object lock1 = new object();
        static object lock2 = new object();

        #endregion

        #region Single Task Summing Example Methods

        private static void SingleTaskSummingExample()
        {
            long total = 0;

            for (int i = 0; i < items.Length; i++)
                total = total + items[i];

            Console.WriteLine("The total is {0}", total);
        }

        #endregion

        #region Bad Task Interaction Example Methods

        static void addRangeOfValues(int start, int end)
        {
            while (start < end)
            {
                sharedTotal = sharedTotal + items[start];
                start++;
            }
        }

        private static void BadTaskInteractionExample()
        {
            List<Task> tasks = new List<Task>();

            int rangeSize = 1000;
            int rangeStart = 0;

            while (rangeStart < items.Length)
            {
                int rangeEnd = rangeStart + rangeSize;

                if (rangeEnd > items.Length)
                    rangeEnd = items.Length;

                //create local copies of the parameters
                int rs = rangeStart;
                int re = rangeEnd;

                tasks.Add(Task.Run(() => addRangeOfValues(rs, re)));
                rangeStart = rangeEnd;
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("The total is {0}", sharedTotal);
        }

        #endregion

        #region Using Locking Example Methods

        static void addRangeOfValuesWithLocking(int start, int end)
        {
            while (start < end)
            {
                lock (sharedTotalLock)
                {
                    sharedTotal = sharedTotal + items[start];
                }

                start++;
            }
        }

        private static void UsingLockingExample()
        {
            List<Task> tasks = new List<Task>();

            int rangeSize = 1000;
            int rangeStart = 0;

            while (rangeStart < items.Length)
            {
                int rangeEnd = rangeStart + rangeSize;

                if (rangeEnd > items.Length)
                    rangeEnd = items.Length;

                //create local copies of the parameters
                int rs = rangeStart;
                int re = rangeEnd;

                tasks.Add(Task.Run(() => addRangeOfValuesWithLocking(rs, re)));
                rangeStart = rangeEnd;
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("The total is {0}", sharedTotal);
        }

        #endregion

        #region Sensible Locking Example Methods

        static void addRangeOfValuesSensibleLocking(int start, int end)
        {
            long subTotal = 0;

            while (start < end)
            {
                subTotal = subTotal + items[start];
                start++;
            }

            lock (sharedTotalLock)
            {
                sharedTotal = sharedTotal + subTotal;
            }
        }

        private static void SensibleLockingExample()
        {
            List<Task> tasks = new List<Task>();

            int rangeSize = 1000;
            int rangeStart = 0;

            while (rangeStart < items.Length)
            {
                int rangeEnd = rangeStart + rangeSize;

                if (rangeEnd > items.Length)
                    rangeEnd = items.Length;

                //create local copies of the parameters
                int rs = rangeStart;
                int re = rangeEnd;

                tasks.Add(Task.Run(() => addRangeOfValuesSensibleLocking(rs, re)));
                rangeStart = rangeEnd;
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("The total is {0}", sharedTotal);
        }

        #endregion

        #region Using Monitors Example Methods

        static void addRangeOfValueUsingMonitors(int start, int end)
        {
            long subTotal = 0;

            while (start < end)
            {
                subTotal = subTotal + items[start];
                start++;
            }

            Monitor.Enter(sharedTotalLock);
            sharedTotal = sharedTotal + subTotal;
            Monitor.Exit(sharedTotalLock);
        }

        private static void UsingMonitorsExample()
        {
            List<Task> tasks = new List<Task>();

            int rangeSize = 1000;
            int rangeStart = 0;

            while (rangeStart < items.Length)
            {
                int rangeEnd = rangeStart + rangeSize;

                if (rangeEnd > items.Length)
                    rangeEnd = items.Length;

                //create local copies of the parameters
                int rs = rangeStart;
                int re = rangeEnd;

                tasks.Add(Task.Run(() => addRangeOfValueUsingMonitors(rs, re)));
                rangeStart = rangeEnd;
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("The total is {0}", sharedTotal);
        }

        #endregion

        #region Sequential Locking Example, Deadlocked Tasks Example Methods

        static void Method1()
        {
            lock (lock1)
            {
                Console.WriteLine("Method 1 got lock1");
                Console.WriteLine("Method 1 waiting for lock2");

                lock (lock2)
                {
                    Console.WriteLine("Method 1 got lock2");
                }

                Console.WriteLine("Method 1 released lock2");
            }
            Console.WriteLine("Method 1 released lock1");
        }

        static void Method2()
        {
            lock (lock2)
            {
                Console.WriteLine("Method 2 got lock2");
                Console.WriteLine("Method 2 waiting for lock1");

                lock (lock1)
                {
                    Console.WriteLine("Method 2 got lock1");
                }
                Console.WriteLine("Method 2 released lock1");
            }
            Console.WriteLine("Method 2 released lock2");
        }

        #endregion

        #region Sequential Locking Example Methods

        private static void SequentialLockingExample()
        {
            Method1();
            Method2();
        }

        #endregion

        #region Deadlocked Tasks Example Methods

        private static void DeadlockedTasksExample()
        {
            Task t1 = Task.Run(() => Method1());
            Task t2 = Task.Run(() => Method2());

            Console.WriteLine("Waiting for task2");
            t2.Wait();

            Console.WriteLine("Tasks complete");
        }

        #endregion

        #region Interlock Total Methods

        static void addRangeOfValuesWithInterlock(int start, int end)
        {
            long subtotal = 0;

            while (start < end)
            {
                subtotal = subtotal + items[start];
                start++;
            }

            Interlocked.Add(ref sharedTotal, subtotal);
        }

        private static void InterlockTotalExample()
        {
            List<Task> tasks = new List<Task>();

            int rangeSize = 1000;
            int rangeStart = 0;

            while (rangeStart < items.Length)
            {
                int rangeEnd = rangeStart + rangeSize;

                if (rangeEnd > items.Length)
                    rangeEnd = items.Length;

                //create local copies of the parameters
                int rs = rangeStart;
                int re = rangeEnd;

                tasks.Add(Task.Run(() => addRangeOfValuesWithInterlock(rs, re)));
                rangeStart = rangeEnd;
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("The total is {0}", sharedTotal);
        }

        #endregion

        #region Cancel a Task Example Methods and Variable

        static CancellationTokenSource cancelationTokenSource = new CancellationTokenSource();

        static void Clock()
        {
            while(!cancelationTokenSource.IsCancellationRequested)
            {
                Console.WriteLine("Tick");
                Thread.Sleep(500);
            }
        }

        private static void CancelATaskExample()
        {
            Task.Run(() => Clock());
            Console.WriteLine("Press any key to stop the clock");
            Console.ReadKey();
            cancelationTokenSource.Cancel();
            Console.WriteLine("Clock stopped");
            Console.ReadKey();
        }

        #endregion

        #region Cancel with Exception Methods

        static void Clock(CancellationToken cancellationToken)
        {
            int tickCount = 0;

            while(!cancellationToken.IsCancellationRequested && tickCount < 20)
            {
                tickCount++;
                Console.WriteLine("Tick");
                Thread.Sleep(500);
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        private static void CancelWithExceptionExample()
        {
            Task clock = Task.Run(() => Clock(cancelationTokenSource.Token));

            Console.WriteLine("Press any key to stop the clock");
            Console.ReadKey();

            if(clock.IsCompleted)
            {
                Console.WriteLine("Clock task completed");
            }
            else
            {
                try
                {
                    cancelationTokenSource.Cancel();
                    clock.Wait();
                }
                catch(AggregateException ex)
                {
                    Console.WriteLine("Clock stopped: {0}", ex.InnerExceptions[0].ToString());
                }
            }
        }
        
        #endregion
    }
}
