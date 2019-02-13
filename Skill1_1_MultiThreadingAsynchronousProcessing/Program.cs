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
            ParallelForEach = 2,
            ParallelFor = 3,
            ManagingParallelForLoop = 4,
            ParallelLINQ = 5,
            InformingParallelization = 6
        }

        static void Main(string[] args)
        {
            bool run = true;

            while (run)
            {
                Items item = Items.None;

                int i = -1;

                while (i < 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Exam Ref 70-483 - Examples");
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("1 - Parallel Invoke;");
                    Console.WriteLine("2 - Parallel For Each;");
                    Console.WriteLine("3 - Parallel For;");
                    Console.WriteLine("4 - Managing a Parallel For Loop;");
                    Console.WriteLine("5 - Parallel LINQ;");
                    Console.WriteLine("6 - Informing Parallelization;");
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
                    case Items.ParallelInvoke:
                        ParallelInvokeExample();
                        break;
                    case Items.ParallelForEach:
                        ParallelForEachExample();
                        break;
                    case Items.ParallelFor:
                        ParallelForExample();
                        break;
                    case Items.ManagingParallelForLoop:
                        ManagingParallelForLoopExample();
                        break;
                    case Items.ParallelLINQ:
                        ParallelLINQExample();
                        break;
                    case Items.InformingParallelization:
                        InformingParallelizationExample();
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
        }

        #endregion

        #region Parallel ForEach, Parallel For and Managing Parallel For Loop Example Methods

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on " + item);
            Thread.Sleep(100);
            Console.WriteLine("Finished working on " + item);
        }

        #endregion

        #region Parallel ForEach Example Methods        

        private static void ParallelForEachExample()
        {
            var items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, item =>
            {
                WorkOnItem(item);
            });
        }

        #endregion

        #region Parallel For Example Methods

        private static void ParallelForExample()
        {
            var items = Enumerable.Range(0, 500).ToArray();

            Parallel.For(0, items.Length, i =>
            {
                WorkOnItem(items[i]);
            });
        }

        #endregion

        #region Managing Parallel For Loop Example Methods
        private static void ManagingParallelForLoopExample()
        {
            var items = Enumerable.Range(0, 500).ToArray();
            ParallelLoopResult result = Parallel.For(0, items.Count(), (int i, ParallelLoopState loopState) =>
            {
                if (i == 200)
                    loopState.Stop();

                WorkOnItem(items[i]);
            });

            Console.WriteLine("Completed: " + result.IsCompleted);
            Console.WriteLine("Items: " + result.LowestBreakIteration);
        }

        #endregion

        #region Parallel LINQ, Informing Parallelization Example Methods and Classes

        class Person
        {
            public string Name { get; set; }
            public string City { get; set; }

            public static Person[] ListPerson()
            {
                return new Person[]
                {
                    new Person { Name = "Alan", City = "Hull"},
                    new Person { Name = "Beryl", City = "Seattle"},
                    new Person { Name = "Charles", City = "London"},
                    new Person { Name = "David", City = "Seattle"},
                    new Person { Name = "Eddy", City = "Paris"},
                    new Person { Name = "Fred", City = "Berlin"},
                    new Person { Name = "Gordon", City = "Hull"},
                    new Person { Name = "Henry", City = "Seattle"},
                    new Person { Name = "Isaac", City = "Seattle"},
                    new Person { Name = "James", City = "London"}
                };
            }
        }

        #endregion

        #region Parallel LINQ Example Methods and classes
        
        private static void ParallelLINQExample()
        {
            Person[] people = Person.ListPerson();

            var result = from person in people.AsParallel()
                         where person.City == "Seattle"
                         select person;

            foreach (var person in result)
                Console.WriteLine(person.Name);
        }

        #endregion

        #region Informing Parallelization Example Methods

        private static void InformingParallelizationExample()
        {
            Person[] people = Person.ListPerson();

            var result = from person in people.AsParallel()
                                              .WithDegreeOfParallelism(4)
                                              .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                         where person.City == "Seattle"
                         select person;

            foreach (var person in result)
                Console.WriteLine(person.Name);
        }

        #endregion
    }
}