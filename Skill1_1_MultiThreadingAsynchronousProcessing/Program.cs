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
            InformingParallelization = 6,
            UsingAsOrdered = 7,
            UsingAsSequential = 8,
            UsingForAll = 9,
            ExceptionsInPLINQ = 10, 
            CreateATask = 11,
            RunATask = 12,
            TaskReturningAValue = 13,
            TaskWaitAll = 14,
            ContinuationTasks = 15,
            ContinuationOptions = 16
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
                    Console.WriteLine("7 - Using AsOrdered;");
                    Console.WriteLine("8 - Using AsSequential;");
                    Console.WriteLine("9 - Using ForAll;");
                    Console.WriteLine("10 - Exceptions in PLINQ;");
                    Console.WriteLine("11 - Create a Task;");
                    Console.WriteLine("12 - Run a Task;");
                    Console.WriteLine("13 - Task Returning a Value;");
                    Console.WriteLine("14 - Task WaitAll;");
                    Console.WriteLine("15 - Continuation Tasks;");
                    Console.WriteLine("16 - Continuation Options");
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
                    case Items.UsingAsOrdered:
                        UsingAsOrderedExample();
                        break;
                    case Items.UsingAsSequential:
                        UsingAsSequentialExample();
                        break;
                    case Items.UsingForAll:
                        UsingForAllExample();
                        break;
                    case Items.ExceptionsInPLINQ:
                        ExceptionsInPLINQExample();
                        break;
                    case Items.CreateATask:
                        CreateATaskExample();
                        break;
                    case Items.RunATask:
                        RunATaskExample();
                        break;
                    case Items.TaskReturningAValue:
                        TaskReturningAValueExample();
                        break;
                    case Items.TaskWaitAll:
                        TaskWaitAllExample();
                        break;
                    case Items.ContinuationTasks:
                        ContinuationTasksExample();
                        break;
                    case Items.ContinuationOptions:
                        ContinuationOptionsExample();
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

        #region Parallel LINQ, Informing Parallelization, Using AsOrdered, Using AsSequential, Using ForAll, Exceptions In PLINQ Example Methods and Classes

        class Person
        {
            public string Name { get; set; }
            public string City { get; set; }

            public static Person[] ListPerson()
            {
                return new Person[]
                {
                    new Person { Name = "Alan", City = ""},
                    new Person { Name = "Beryl", City = "Seattle"},
                    new Person { Name = "Charles", City = ""},
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

        #region Using AsOrdered Example Methods
        
        private static void UsingAsOrderedExample()
        {
            Person[] people = Person.ListPerson();

            var result = from person in people.AsParallel().AsOrdered()
                         where person.City == "Seattle"
                         select person;

            foreach(var person in result)
                Console.WriteLine(person.Name);
        }

        #endregion

        #region Using AsSequential Example Methods

        private static void UsingAsSequentialExample()
        {
            Person[] people = Person.ListPerson();

            var result = (from person in people.AsParallel()
                          where person.City == "Seattle"
                          orderby (person.Name)
                          select new
                          {
                              person.Name
                          }).AsSequential().Take(4);

            foreach (var person in result)
                Console.WriteLine(person.Name);
        }

        #endregion

        #region Using ForAll Example Methods

        private static void UsingForAllExample()
        {
            Person[] people = Person.ListPerson();

            var result = from person in people.AsParallel()
                         where person.City == "Seattle"
                         select person;

            result.ForAll(person => Console.WriteLine(person.Name));
        }

        #endregion

        #region Exceptions In PLINQ Example Methods

        public static bool CheckCity(string name)
        {
            if (name == "")
                throw new ArgumentException(name);

            return name == "Seattle";
        }
        
        private static void ExceptionsInPLINQExample()
        {
            Person[] people = Person.ListPerson();

            try
            {
                var result = from person in people.AsParallel()
                             where CheckCity(person.City)
                             select person;

                result.ForAll(person => Console.WriteLine(person.Name));

            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions.Count() + " exceptions.");
            }
        }

        #endregion

        #region Create a Task, Run a Task Example Methods

        public static void DoWork()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
        }

        #endregion

        #region Create a Task Example Methods

        private static void CreateATaskExample()
        {
            Task newTask = new Task(() => DoWork());

            newTask.Start();
            newTask.Wait();
        }

        #endregion
        
        #region Run a Task Example Methods

        private static void RunATaskExample()
        {
            Task newTask = Task.Run(() => DoWork());
            newTask.Wait();
        }

        #endregion

        #region Task Returning a Value Example Methods 

        public static int CalculateResult()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
            return 99;
        }

        private static void TaskReturningAValueExample()
        {
            Task<int> task = Task.Run(() =>
            {
                return CalculateResult();
            });

            Console.WriteLine(task.Result);
        }

        #endregion

        #region Task WaitAll Example Methods

        public static void DoWork(int i)
        {
            Console.WriteLine("Task {0} starting", i);
            Thread.Sleep(2000);
            Console.WriteLine("Task {0} finished", i);
        }

        private static void TaskWaitAllExample()
        {
            Task[] Tasks = new Task[10];
            for(int i = 0; i < 10; i++)
            {
                int taskNum = i; //make a local copy of the loop counter so that the correct task number is passed into the lambda expression
                Tasks[i] = Task.Run(() => DoWork(taskNum));
            }
            Task.WaitAll(Tasks);
        }

        #endregion

        #region Continuation Tasks, Continuation Options Examples Methods

        public static void HelloTask()
        {            
            Thread.Sleep(1000);                
            Console.WriteLine("Hello");            
        }

        public static void WorldTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("World");
        }

        #endregion

        #region Continuation Tasks Example Methods        

        private static void ContinuationTasksExample()
        {
            Task task = Task.Run(() => HelloTask());
            task.ContinueWith((prevTask) => WorldTask()).Wait();
        }

        #endregion

        #region Continuation Options Example Methods

        private static void ExceptionTask()
        {
            Console.WriteLine("An error occurred.");
        }

        private static void ContinuationOptionsExample()
        {
            Task task = Task.Run(() => HelloTask());
            task.ContinueWith((prevTask) => WorldTask(), TaskContinuationOptions.OnlyOnRanToCompletion).Wait();
            task.ContinueWith((prevTask) => ExceptionTask(), TaskContinuationOptions.OnlyOnFaulted).Wait();
        }

        #endregion
    }
}