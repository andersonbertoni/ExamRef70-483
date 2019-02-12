using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Skill1_1_MultiThreadingAsynchronousProcessing
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            ParallelInvoke = 1,
            ParallelForEach = 2
        }

        static void Main(string[] args)
        {
            bool run = true;

            while(run)
            {
                Items item = Items.None;

                int i = -1;

                while(i < 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Exam Ref 70-483 - Examples");
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("1 - Parallel Invoke;");
                    Console.WriteLine("2 - Parallel For Each;");
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
                               
                switch(item)
                {
                    case Items.ParallelInvoke:
                        ParallelInvokeExample();
                        break;
                    case Items.ParallelForEach:
                        ParallelForEachExample();
                        break;
                    case Items.Sair:
                        run = false;
                        break;
                }
            }
        }

        #region Parallel Invoke Example Methods

        static void Task1()
        {
            Console.WriteLine("Task 1 starting");
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 ending");
        }

        static void Task2()
        {
            Console.WriteLine("Task 2 starting");
            Thread.Sleep(1000);
            Console.WriteLine("Task 2 ending");
        }

        private static void ParallelInvokeExample()
        {
            Parallel.Invoke(() => Task1(), () => Task2());
            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }

        #endregion

        #region Parallel ForEach Example Methods

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on " + item);
            Thread.Sleep(100);
            Console.WriteLine("Finished working on " + item);
        }

        private static void ParallelForEachExample()
        {
            var items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, item =>
            {
                WorkOnItem(item);
            });

            Console.WriteLine("Finished processing. Press a key to end");
            Console.ReadKey();
        }

        #endregion
    }
}
