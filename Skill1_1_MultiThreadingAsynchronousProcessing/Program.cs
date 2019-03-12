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
            ContinuationOptions = 16,
            AttachedChildTasks = 17,
            CreatingThreads = 18,
            UsingThreadStart = 19,
            ThreadAndLambdaExpressions = 20,
            ParameterizedThreadStart = 21,
            ThreadLambdaParameters = 22,
            AbortingAThread = 23,
            AbortingAThreadWithASharedFlagVariable = 24,
            ThreadSynchronizationUsingJoin = 25,
            ThreadLocal = 26,
            ThreadContext = 27,
            ThreadPool = 28
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
                    case Items.AttachedChildTasks:
                        AttachedChildTasksExample();
                        break;
                    case Items.CreatingThreads:
                        CreatingThreadsExample();
                        break;
                    case Items.UsingThreadStart:
                        UsingThreadStartExample();
                        break;
                    case Items.ThreadAndLambdaExpressions:
                        ThreadAndLambdaExpressionsExample();
                        break;
                    case Items.ParameterizedThreadStart:
                        ParameterizedThreadStartExample();
                        break;
                    case Items.ThreadLambdaParameters:
                        ThreadLambdaParametersExample();
                        break;
                    case Items.AbortingAThread:
                        AbortingAThreadExample();
                        break;
                    case Items.AbortingAThreadWithASharedFlagVariable:
                        AbortingAThreadWithASharedFlagVariableExample();
                        break;
                    case Items.ThreadSynchronizationUsingJoin:
                        ThreadSynchronizationUsingJoinExample();
                        break;
                    case Items.ThreadLocal:
                        ThreadLocalExample();
                        break;
                    case Items.ThreadContext:
                        ThreadContextExample();
                        break;
                    case Items.ThreadPool:
                        ThreadPoolExample();
                        break;
                    case Items.Sair:
                        run = false;
                        break;
                }

                if (run)
                {
                    /* Se o exemplo for relacionado com Thread, espero 1 seg antes de exibir a mensagem final. 
                     * Caso contrário o sistema printa a mensagem final antes da mensagem do exemplo */
                    if (Convert.ToInt32(item) > 17) 
                        Thread.Sleep(1000);

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

        #region Attached Child Tasks Example Methods

        public static void DoChild(object state)
        {
            Console.WriteLine("Child {0} starting", state);
            Thread.Sleep(2000);
            Console.WriteLine("Child {0} finished", state);
        }

        private static void AttachedChildTasksExample()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent starts");
                for (int i = 0; i < 10; i++)
                {
                    int taskNo = i;
                    Task.Factory.StartNew((x) => DoChild(x), //lambda expression
                                                 taskNo,     //state object
                                                 TaskCreationOptions.AttachedToParent); //
                }
            });

            parent.Wait(); //will wait for all the attached children to complete
        }

        #endregion

        #region Creating Threads, Using Thread Start Example Methods

        static void ThreadHello()
        {
            Console.WriteLine("Hello from the thread");
            Thread.Sleep(2000);
        }

        #endregion

        #region Creating Threads Example Methods

        private static void CreatingThreadsExample()
        {
            Thread thread = new Thread(ThreadHello);
            thread.Start();
        }

        #endregion

        #region Using Thread Start Example Methods

        private static void UsingThreadStartExample()
        {
            ThreadStart ts = new ThreadStart(ThreadHello);
            Thread thread = new Thread(ts);
            thread.Start();
        }

        #endregion

        #region Thread And Lambda Expressions Example Methods

        private static void ThreadAndLambdaExpressionsExample()
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("Hello from the thread");
                Thread.Sleep(1000);
            });

            thread.Start();
        }

        #endregion

        #region Parameterized Thread Start, Thread Lambda Parameters Example Methods

        static void WorkOnData(object data)
        {
            Console.WriteLine("Working on: {0}", data);
            Thread.Sleep(1000);
        }

        #endregion

        #region Parameterized Thread Start Example Methods

        private static void ParameterizedThreadStartExample()
        {
            ParameterizedThreadStart ps = new ParameterizedThreadStart(WorkOnData);
            Thread thread = new Thread(ps);
            thread.Start(99);
        }

        #endregion

        #region Thread Lambda Parameters Example Methods

        private static void ThreadLambdaParametersExample()
        {
            Thread thread = new Thread((data) =>
            {
                WorkOnData(data);
            });

            thread.Start(99);
        }

        #endregion

        #region AbortingAThreadExample

        private static void AbortingAThreadExample()
        {
            Thread tickThread = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Tick");
                    Thread.Sleep(1000);
                }
            });

            tickThread.Start();

            Console.WriteLine("Press a key to stop the clock");
            Console.ReadKey();
            tickThread.Abort();
        }

        #endregion

        #region Aborting A Thread With A Shared Flag Variable Example Methods

        static bool tickRunning; //flag variable

        private static void AbortingAThreadWithASharedFlagVariableExample()
        {
            tickRunning = true;

            Thread tickThread = new Thread(() =>
            {
                while (tickRunning)
                {
                    Console.WriteLine("Tick");
                    Thread.Sleep(1000);
                }
            });

            tickThread.Start();
            Console.WriteLine("Press a key to stop the clock");
            Console.ReadKey();
            tickRunning = false;            
        }

        #endregion

        #region Thread Synchronization Using Join Example Methods

        private static void ThreadSynchronizationUsingJoinExample()
        {
            Thread threadToWaitFor = new Thread(() =>
            {
                Console.WriteLine("Thread Starting");
                Thread.Sleep(2000);
                Console.WriteLine("Thread done");
            });

            threadToWaitFor.Start();
            Console.WriteLine("Joining thread");
            threadToWaitFor.Join();
        }

        #endregion

        #region Thread Local Example Methods

        private static ThreadLocal<Random> RandomGenerator = 
            new ThreadLocal<Random>(() =>
            {
               return new Random(2);
            });

        private static void ThreadLocalExample()
        {
            Thread t1 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("t1: {0}", RandomGenerator.Value.Next(10));
                    Thread.Sleep(500);
                }
            });

            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("t2: {0}", RandomGenerator.Value.Next(10));
                    Thread.Sleep(500);
                }
            });

            t1.Start();
            t2.Start();

            Console.ReadKey();
        }

        #endregion

        #region Thread Context Example Methods

        private static void DisplayThread(Thread t)
        {
            Console.WriteLine("Name: {0}", t.Name);
            Console.WriteLine("Culture: {0}", t.CurrentCulture);
            Console.WriteLine("Priority: {0}", t.Priority);
            Console.WriteLine("Context: {0}", t.ExecutionContext);
            Console.WriteLine("IsBackground: {0}", t.IsBackground);
            Console.WriteLine("IsPool: {0}", t.IsThreadPoolThread);
        }

        private static void ThreadContextExample()
        {
            Thread.CurrentThread.Name = "Main Method";
            DisplayThread(Thread.CurrentThread);
        }

        #endregion

        #region Thread Pool Example Methods

        private static void DoWork(object state)
        {
            Console.WriteLine("Doing work: {0}", state);
            Thread.Sleep(500);
            Console.WriteLine("Work finished: {0}", state);
        }

        private static void ThreadPoolExample()
        {
            for(int i = 0; i < 50; i++)
            {
                int stateNumber = i;
                ThreadPool.QueueUserWorkItem(state => DoWork(stateNumber));
            }
            Console.ReadKey();
        }

        #endregion
    }
}