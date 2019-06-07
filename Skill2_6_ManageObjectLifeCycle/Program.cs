using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill2_6_ManageObjectLifeCycle
{
    class Program
    {
        public static Person2 zombie;

        private enum Items
        {
            Sair = -1,
            None = 0,
            GarbageCollection = 1,
            FinalizerMethod = 2
        }

        static void Main(string[] args)
        {
            bool run = true;

            while (run)
            {
                Items item = Items.None;

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
                    case Items.GarbageCollection:
                        GarbageCollectionExample();
                        break;
                    case Items.FinalizerMethod:
                        FinalizerMethodExample();
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

        class Person
        {
            long[] personArray = new long[1000000];
        }

        public class Person2
        {
            long[] personArray = new long[1000000];

            /* The .NET Framework will take care of the creation and destruction of our objects, 
             * but we need to manage the resources that our objects use. For example, if an 
             * application creates a file handle and stores it in an object, when the object is
             * destroyed, the file handle will be lost. If the file connected to the handle is 
             * not closed properly before the object is destroyed, the file that the handle is 
             * connected to will not be usable.
             * There are two mechanisms that we can use that allow us to get control at the point
             * an object is being destroyed and tidy up any resources that the object may be using.
             * They are finalization and disposable.
             * The finalization of an object is triggered by the garbage collection process. An 
             * object can contain a finalizer method that is invoked by the garbage collector in 
             * advance of that object being removed from memory. This method gets control and can
             * release any resources that are being used by that object. The finalizer method is 
             * given as a type less method with the name of the class pre-ceded by a tilde (~)
             * character. The code below shows hot to give the Person2 class a finalizer method.
             * Here it is just prints a message, but in an application we would use this method
             * to release any resources that the Person2 object had allocated. */             
            ~Person2()
            {
                //This is where the person would be finalized
                Console.WriteLine("Finalizer called");

                //This will "bring the object back from the dead"
                //by creating a new reference to the object from
                //the program.
                //Program.zombie = this;

                /* When the garbage collector is about to remove an object, it checks to see
                 * if the object contains a finalizer method. If there is a finalizer method
                 * present, the garbage collector adds the object to a queue of objects 
                 * waiting to be finalized. Once all of these objects have been identified, 
                 * the garbage collector starts a thread to execute all the finalizer methods
                 * and waits for the thread to complete. Once the finalization methods are 
                 * complete the garbage collector performs another garbage collection to 
                 * remove the finalized objects. There are no guarantees as to when the
                 * finalizer thread will run. Objects waiting to be finalized will remain in
                 * memory until all of the finalizer methods have completed and the garbage
                 * collector has made another garbage collection pass to remove them.
                 * A slow-running finalizer can seriously impair the garbage collection process.
                 * The code below inputs a delay that will cause the program to run out of memory. */

                //This will break the garbage collection process 
                //as it slows it down so that it can't complete
                //faster than objects are being created.
                //System.Threading.Thread.Sleep(100);
            }
        }

        #endregion

        #region GarbageCollectionExample Method
        
        private static void GarbageCollectionExample()
        {
            /* Consider the following two statements. The first statement declares a Person reference
             * called p and makes the reference refer to a new Person instance. The second statement
             * assigns the reference p to a new Person instance. These two statements will cause work
             * for the garbage collector. The first Person object that was created can play no further
             * part in the program as there is no way of accessing it. */
            Person p = new Person();
            p = new Person();

            /* You get a similar situation when a reference variable goes out of scope. This block of 
             * code that follows also creates work for the garbage collector. When the program exits 
             * from the block the Person object that was created is no longer accessible because the
             * reference p no longer exists. However, the person object will still be occupying memory. */
            {
                Person p2 = new Person();
            }

            /* A process has a particular amount allocated to it. Garbage collection only occurs when
             * the amount of memory available for new objects falls below a threshold. The code below 
             * deliberately creates a large number of inaccessible objects. The Visual Studio runtime
             * environment provides a display of memory use that show the garbage collection process
             * in action.
             * If we look at the graph provided we will see that the memory usage of the program varies
             * dramatically over time. The rate at which the garbage is collected (showed in yellow)
             * will change, depending on the loading on the host computer. */
            for(long i = 0; i < 1000; i++)
            {
                Person p1 = new Person();
                System.Threading.Thread.Sleep(3);
            }

            /* The storage graph shows the size of the heap. The heap is the area of memory where an
             * application stores objects that are referred to by reference. The contents of value 
             * types are stored on the stack. The stack automatically grows and contracts as programs
             * runs. Upon entry to a new bloc the .NET runtime will allocate space on the stack for
             * values that are declared local to that block. When the program leaves the block the
             * .NET runtime will automatically contract the stack space, which removes the memory
             * allocated for those variables. The following block will not make any work for the
             * garbage collector as the value 99 will be stored in the local stack frame. */
            {
                int i = 99;
            }
        }

        #endregion

        #region FinalizerMethodExample Method

        private static void FinalizerMethodExample()
        {
            for(long i = 0; i < 1500; i++)
            {
                Person2 p = new Person2();
                System.Threading.Thread.Sleep(3);
            }
        }

        #endregion
    }
}
