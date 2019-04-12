using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Skill1_4_CreateImplementEventsCallbacks
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            PublishAndSubscribe = 1,
            Unsubscribing = 2,
            EventBasedAlarm = 3,
            EventHandlerAlarm = 4,
            EventHandlerData = 5,
            AggregateExceptions = 6,
            CreateDelegates = 7,
            LambdaExpressions = 8,
            Closures = 9,
            BuiltInDelegates = 10,
            LambdaExpressionTask = 11
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
                    case Items.PublishAndSubscribe:
                        PublishAndSubscribeExample();
                        break;
                    case Items.Unsubscribing:
                        UnsubscribingExample();
                        break;
                    case Items.EventBasedAlarm:
                        EventBasedAlarmExample();
                        break;
                    case Items.EventHandlerAlarm:
                        EventHandlerAlarmExample();
                        break;
                    case Items.EventHandlerData:
                        EventHandlerDataExample();
                        break;
                    case Items.AggregateExceptions:
                        AggregateExceptionsExample();
                        break;
                    case Items.CreateDelegates:
                        CreateDelegatesExample();
                        break;
                    case Items.LambdaExpressions:
                        LambdaExpressionsExample();
                        break;
                    case Items.Closures:
                        ClosuresExample();
                        break;
                    case Items.BuiltInDelegates:
                        BuiltInDelegatesExample();
                        break;
                    case Items.LambdaExpressionTask:
                        LambdaExpressionTaskExample();
                        Thread.Sleep(3000);
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

        #region Classes

        class Alarm
        {
            //Delegate for the alarm event
            public Action OnAlarmRaised { get; set; }

            public void RaiseAlarm()
            {
                //Only raise the alarm if someone has subscribed
                //if(OnAlarmRaised != null)
                //{
                //    OnAlarmRaised();
                //}

                /* The calling of the delegate can be simplified by 
                 * using the null conditional operator. This only performs
                 * an action if the given item is not null.
                 * The null conditional operator means "only access this member 
                 * of the class if the reference is not null */
                OnAlarmRaised?.Invoke();
            }
        }

        class Alarm2
        {
            //Delegate for the alarm event
            /* The member OnAlarmRaised is now created as a data field
             * in the Alarm2 class, rather than a property.
             * OnAlarmRaised no longer has get or set behaviors.
             * However, it is now not possible for code external to the
             * Alarm class to assign values to OnAlarmRaised, and the OnAlarmRaised 
             * delegate can only be called from within the class where it is declared.
             * In the other words, adding the event keyword turns a delegate into
             * a properly useful event. */
            public event Action OnAlarmRaised = delegate { };

            //Called to raise an alarm
            public void RaiseAlarm()
            {
                /* Other improvement over previous version is that it creates
                 * a delegate instance and assigns it when OnAlarmRaised is created, 
                 * so there is now no need to check whether or not the delegate has a
                 * value before calling it. */
                OnAlarmRaised();
            }
        }

        class Alarm3
        {
            //Delegate for the alarm event
            public event EventHandler OnAlarmRaised = delegate { };

            //Called to raise an alarm
            //Does not provide any event arguments
            public void RaiseAlarm()
            {
                //Raises the alarm
                //The event handler receivers a reference to the alarm
                //that is raising this event.
                /* The EventHandler delegate refers to a subscriber method
                 * that will accept two arguments. The first argument is a
                 * reference to the object raising the event. The second
                 * argument is a reference to an object of type EventArgs
                 * that provides information about the event.
                 * The second argument is set to EventArgs.Empty to indicate
                 * that this event does not produce any data, it is simply a
                 * notification that an event has taken place. */
                OnAlarmRaised(this, EventArgs.Empty);
            }
        }

        class Alarm4
        {
            //Delegate for the alarm event
            public event EventHandler<AlarmEventArgs> OnAlarmRaised = delegate { };

            //Called to raise an alarm
            public void RaiseAlarm(string location)
            {
                OnAlarmRaised(this, new AlarmEventArgs(location));
            }
        }

        class Alarm5
        {
            //Delegate for the alarm event
            public event EventHandler<AlarmEventArgs> OnAlarmRaised = delegate { };

            //Called to raise an alarm
            public void RaiseAlarm(string location)
            {
                List<Exception> exceptionList = new List<Exception>();

                foreach(Delegate handler in OnAlarmRaised.GetInvocationList())
                {
                    try
                    {
                        handler.DynamicInvoke(this, new AlarmEventArgs(location));
                    }
                    catch (TargetInvocationException e)
                    {
                        exceptionList.Add(e.InnerException);                        
                    }
                }

                if (exceptionList.Count > 0)
                    throw new AggregateException(exceptionList);
            }
        }

        class AlarmEventArgs : EventArgs
        {
            public string Location { get; set; }

            public AlarmEventArgs(string location)
            {
                this.Location = location;
            }
        }

        #endregion

        #region Publish and Subscribe Example, Unsubscribing Example Methods

        //Method that must run when the alarm is raised
        static void AlarmListener1()
        {
            Console.WriteLine("Alarm listener 1 called");
        }

        //Method that must run when the alarm is raised
        static void AlarmListener2()
        {
            Console.WriteLine("Alarm listener 2 called");
        }

        #endregion

        #region Publish and Subscribe Example Methods

        private static void PublishAndSubscribeExample()
        {
            //Create a new alarm
            Alarm alarm = new Alarm();

            //Connect the two listener methods
            //Subscribers bind to a publisher by using the += operator.
            //The += operator is overloaded to apply between a delegate
            //and a behavior ("add this bevahior to the ones for this 
            //delegate")
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;

            //raise the alarm
            alarm.RaiseAlarm();
            Console.WriteLine("Alarm raised");
        }

        #endregion

        #region Unsubscribing Example Method
        
        private static void UnsubscribingExample()
        {
            //Create a new alarm
            Alarm alarm = new Alarm();

            //Connect the two listener methods
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;

            //raise the alarm
            alarm.RaiseAlarm();
            Console.WriteLine("Alarm raised");

            //Disconnect one listener
            alarm.OnAlarmRaised -= AlarmListener1;

            alarm.RaiseAlarm();
            Console.WriteLine("Alarm raised");
        }

        #endregion

        #region Event Based Alarm Example Method

        private static void EventBasedAlarmExample()
        {
            Alarm2 alarm = new Alarm2();

            //Connect the two listener methods
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;

            //raise the alarm
            alarm.RaiseAlarm();
            Console.WriteLine("Alarm raised");
        }

        #endregion

        #region Event Handler Alarm Example Methods

        static void AlarmListener1(object sender, EventArgs e)
        {
            //Only the sender is valid as this event doesn't have arguments
            Console.WriteLine("Alarm listener 1 called");
        }

        private static void EventHandlerAlarmExample()
        {
            Alarm3 alarm = new Alarm3();

            //Connect the listener method
            alarm.OnAlarmRaised += AlarmListener1;
            
            //Cannot connect to AlarmListener2.
            //No overloaded for 'AlarmListener2' matches delegate 'EventHandler'
            //alarm.OnAlarmRaised += AlarmListener2;

            //raise the alarm
            alarm.RaiseAlarm();
            Console.WriteLine("Alarm raised");
        }

        #endregion

        #region Event Handler Data Example Methods
        
        static void AlarmListener1(object source, AlarmEventArgs args)
        {
            Console.WriteLine("Alarm listener 1 called");
            Console.WriteLine("Alarm in {0}", args.Location);
        }

        private static void EventHandlerDataExample()
        {
            Alarm4 alarm = new Alarm4();

            //Connect the listener method
            alarm.OnAlarmRaised += AlarmListener1;

            //Cannot connect to AlarmListener2.
            //No overloaded for 'AlarmListener2' matches delegate 'EventHandler'
            //alarm.OnAlarmRaised += AlarmListener2;

            //raise the alarm
            alarm.RaiseAlarm("EventHandlerDataExample");
            Console.WriteLine("Alarm raised");
        }

        #endregion

        #region Aggregate Exceptions Example Method

        //The subscribers both throw exceptions
        static void Alarm5Listener1(object source, AlarmEventArgs args)
        {
            Console.WriteLine("Alarm5 listener 1 called");
            Console.WriteLine("Alarm in {0}", args.Location);

            throw new Exception("Bang!");
        }

        static void Alarm5Listener2(object source, AlarmEventArgs args)
        {
            Console.WriteLine("Alarm5 listener 2 called");
            Console.WriteLine("Alarm in {0}", args.Location);

            throw new Exception("Boom!");
        }

        //These errors can be caught and dealt with when the event is raised.
        private static void AggregateExceptionsExample()
        {
            try
            {
                Alarm5 alarm = new Alarm5();

                //Connect the listener method
                alarm.OnAlarmRaised += Alarm5Listener1;
                alarm.OnAlarmRaised += Alarm5Listener2;
                
                //raise the alarm
                alarm.RaiseAlarm("Kitchen");
                Console.WriteLine("Alarm raised");
            }
            catch (AggregateException agg)
            {
                foreach (Exception ex in agg.InnerExceptions)
                    Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Create Delegates Example Methods and Variable

        delegate int IntOperation(int a, int b);

        static int Add(int a, int b)
        {
            Console.WriteLine("Add called");
            return a + b;
        }

        static int Subtract(int a, int b)
        {
            Console.WriteLine("Subtract called");
            return a - b;
        }

        private static void CreateDelegatesExample()
        {
            //Explicitly create the delegate
            /* The op variable is made to refer first 
             * to a method called Add */
            IntOperation op = new IntOperation(Add);
            Console.WriteLine(op(2, 2));

            //Delegate is crated automatically from method
            /* Then the op variable is made to refer to a method
             * called Subtract */            
            op = Subtract;

            /* Each time that op is called it will execute the 
             * method that it has been made to refer to */
            Console.WriteLine(op(2, 2));
        }

        #endregion

        #region Lambda Expressions Method

        private static void LambdaExpressionsExample()
        {
            /* The operator => is called lambda operator.
             * The items a and b on the left of the lambda expression are mapped onto 
             * method parameters defined by the delegate.
             * Lambda expressions can accept multiple parameters and contain multiple 
             * statements in which case the statements are enclosed in a block.
             * When describing the behavior of the lambda expression you can use the 
             * phrase "goes into" to describe what is happening. In this case you 
             * could say "a and b go into a plus b" */
            IntOperation add = (a, b) =>
            {
                Console.WriteLine("Add called");
                return a + b;
            };

            Console.WriteLine(add(2, 2));
        }

        #endregion

        #region Closures Example Methods and Variables

        delegate int GetValue();

        static GetValue getLocalInt;

        /* The method SetLocalInt declares a local variable called localInt
         * and sets its value to 99. Under normal circunstances the variable
         * localInt would be destroyed upon completion of the SetLocalInt method.
         * However, the localInt variable is used in a lambda expression, which 
         * is assigned to the delegate getLocalInt. The compiler makes sure that
         * the localInt variable is available for use in the lambda expression
         * when it is subsequently called from the ClosuresExample method. This
         * extension of variable life is called a closure. */
        static void SetLocalInt()
        {
            //Local variable set to 99
            int localInt = 99;

            /* Set delegate getLocalInt to returns
             * the value of localInt */

            getLocalInt = () =>
            {
                Console.WriteLine("Tipo da variável: {0} - Nome da variável: {1}", localInt.GetType().Name, nameof(localInt));
                return localInt;
            };
        }

        private static void ClosuresExample()
        {
            SetLocalInt();
            Console.WriteLine("Value of localInt = {0}", getLocalInt());
        }

        #endregion

        #region Built In Delegates Example Methods

        /* The Func types provide a range of delegates for 
         * methods that accept values and return results.
         * The example above, creates an add behavior that 
         * has the same return type and parameters as the 
         * IntOperation delegate in CreateDelegatesExample
         * The add method accept two integers and returns 
         * an integer as the result */
        static Func<int, int, int> add = (a, b) => a + b;

        /* If the lambda expression doesn't return a result, you can
         * use the Action type. The statement below creates a delegate
         * called logMessage that refers to a lambda expression that
         * accepts a string and then prints it to the console. */
        static Action<string> logMessage = (message) => Console.WriteLine(message);

        /* The Predicate built in delegate type lets you create
         * code that takes a value of a particular type and returns
         * true or false. The dividesByThree predicate below returns
         * true if the value is divisible by 3. */
        static Predicate<int> dividesByThree = (i) => i % 3 == 0;

        private static void BuiltInDelegatesExample()
        {
            //testing add delegate
            Console.WriteLine(add(1, 2));

            //testing logMessage delegate
            logMessage("Logging a message");

            //testing dividesByThree delegate
            Console.WriteLine(dividesByThree(9));
        }

        #endregion

        #region Lambda Expression Task Example Method

        /* A lambda expression can be used directly in a 
         * context where you just want to express a particular
         * behavior. The program below uses Task.Run to start
         * a new task. The code performed by the task is 
         * expressed directly as a lambda expression, which is 
         * given as an argument to the Task.Run method. At no 
         * point does this code ever have a name. 
         * A lambda expression used in this way can be described
         * as an anonymous method; because it is a piece of 
         * functional code that doesn't have a name. */
        private static void LambdaExpressionTaskExample()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(500);
                }
            });

            Console.WriteLine("Task running...");
        }

        #endregion
    }
}
