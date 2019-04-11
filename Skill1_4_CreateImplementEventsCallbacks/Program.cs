using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
            AggregateExceptions = 6
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
    }
}
