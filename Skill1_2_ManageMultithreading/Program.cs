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
            SequentialLocking = 6
        }
        static void Main(string[] args)
        {
            bool run = true;

            while (run)
            {
                Items item = Items.None;
                CleanVariables();

                int i = -1;

                while (i < 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Exam Ref 70-483 - Examples");
                    Console.WriteLine("--------------------------");

                    foreach (var j in Enum.GetValues(typeof(Items)))
                    {
                        if (Convert.ToInt32(j) > 0)
                        {
                            Console.WriteLine("{0} - {1};", Convert.ToInt32(Enum.Parse(typeof(Items), j.ToString())),
                                                            Enum.GetName(typeof(Items), j));
                        }
                    }

                    Console.WriteLine("S - Sair");

                    string valor = Console.ReadLine().ToLower();

                    if (valor == "s")
                    {
                        item = Items.Sair;
                        i = 99;
                    }
                    else if (int.TryParse(valor, out i))
                    {
                        item = (Items)Enum.Parse(typeof(Items), valor);
                    }
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

            for(int i = 0; i < items.Length; i++)
                total = total + items[i];

            Console.WriteLine("The total is {0}", total);
        }

        #endregion

        #region Bad Task Interaction Example Methods

        static void addRangeOfValues(int start, int end)
        {
            while(start < end)
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

            while(rangeStart < items.Length)
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
            while(start < end)
            {
                lock(sharedTotalLock)
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

            while(start < end)
            {
                subTotal = subTotal + items[start];
                start++;
            }

            lock(sharedTotalLock)
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

            while(start < end)
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

        #region Sequential Locking Example Methods

        static void Method1()
        {
            lock(lock1)
            {
                Console.WriteLine("Method 1 got lock1");
                Console.WriteLine("Method 1 waiting for lock2");

                lock(lock2)
                {
                    Console.WriteLine("Method 1 got lock2");
                }

                Console.WriteLine("Method 1 released lock2");
            }
            Console.WriteLine("Method 1 released lock1");
        }

        static void Method2()
        {
            lock(lock2)
            {
                Console.WriteLine("Method 2 got lock2");
                Console.WriteLine("Method 2 waiting for lock1");

                lock(lock1)
                {
                    Console.WriteLine("Method 2 got lock1");
                }
                Console.WriteLine("Method 2 released lock1");
            }
            Console.WriteLine("Method 2 released lock2");
        }

        private static void SequentialLockingExample()
        {
            Method1();
            Method2();
        }

        #endregion
    }
}
