using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill2_1_CreateTypes
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            ValueAndReferenceTypes = 1,
            CreatingAStructure = 2,
            CreatingAnEnum = 3,
            CreatingAReference = 4,
            UsingGenericTypes = 5
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
                    case Items.ValueAndReferenceTypes:
                        ValueAndReferenceTypesExample();
                        break;
                    case Items.CreatingAStructure:
                        CreatingAStructureExample();
                        break;
                    case Items.CreatingAnEnum:
                        CreatingAnEnumExample();
                        break;
                    case Items.CreatingAReference:
                        CreatingAReferenceExample();
                        break;
                    case Items.UsingGenericTypes:
                        UsingGenericTypesExample();
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
        
        #region Classes, Structs and Variables

        struct StructStore
        {
            public int Data { get; set; }
        }

        class ClassStore
        {
            public int Data { get; set; }
        }

        struct Alien
        {
            public int X;
            public int Y;
            public int Lives;
            
            /* An Alien has an X and Y position on the screen, which is set when the alien is created,
             * along with a number of lives set to 3 by the Alien constructor. */
            public Alien(int x, int y)
            {
                X = x;
                Y = y;
                Lives = 3;
            }

            /* Note that the structure definition contains an override for the ToString method. This is perfectly acceptable;
             * although a structure cannot be used in a class hierarchy, because it is possible to override methods from the 
             * parent type of struct. */
            public override string ToString()
            {
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }

        /* Unless specified otherwise, an enumerated type is based on the int type and the enumerated values are numbered
         * starting at 0. You can modify this by adding extra information to the declaration of the enum.
         * You would do this if you want to set particular values to be used in JSON and XML files when enumerated variables
         * are stored. The code below creates an AlienState enum that is stored in a byte type, and has the given values for
         * sleeping, attacking, and destroyed. */
        enum AlienState : byte
        {
            Sleeping = 1,
            Attacking = 2,
            Destroyed = 3
        }
        
        class AlienC
        {
            public int X;
            public int Y;
            public int Lives;

            public AlienC(int x, int y)
            {
                X = x;
                Y = y;
                Lives = 3;
            }

            public override string ToString()
            {
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }

        /* The class below shows how generics can be used to create a stack type that can be used to hold a stack of any 
         * type of object. The generic type to be used is specified in the declaration of MyStack(in the class below it 
         * is given the name T). The name T is then used within the MyStack declaration to represent the type that will 
         * be supplied when a variable of type MyStack is declared. The Push and Pop methods in the MyStack class also
         * work with objects of type T. */
        class MyStack<T>
        {
            int stackTop = 0;
            T[] items = new T[100];

            public void Push(T item)
            {
                if (stackTop == items.Length)
                    throw new Exception("Stack full");
                items[stackTop] = item;
                stackTop++;
            }

            public T Pop()
            {
                if (stackTop == 0)
                    throw new Exception("Stack empty");
                stackTop--;
                return items[stackTop];
            }
        }

        #endregion

        #region ValueAndReferenceTypesExample Method

        private static void ValueAndReferenceTypesExample()
        {
            /* Structure variables are managed by value (changes to xStruct do not affect yStruct) */
            /* Value types are great for working with objects that you want to think of in terms of
             * values */
            StructStore xs, ys;
            ys = new StructStore();
            ys.Data = 99;
            xs = ys;
            xs.Data = 100;
            Console.WriteLine("xStruct: {0}", xs.Data);
            Console.WriteLine("yStruct: {0}", ys.Data);
            /* Outputs: 
             * xStruct: 100
             * yStruct: 99 */
             

            /* Class variables are managed by reference (xClass and yClass both refer to the same object, 
             * so changes via the xClass reference will affect the value referred to by yClass) */
            /* Reference types are great for working with objects that you want to manage by reference */
            ClassStore xc, yc;
            yc = new ClassStore();
            yc.Data = 99;
            xc = yc;
            xc.Data = 100;
            Console.WriteLine("xClass: {0}", xc.Data);
            Console.WriteLine("yClass: {0}", yc.Data);
            /* Outputs:
             * xClass: 100
             * yClass: 100 */
        }

        #endregion

        #region CreatingAStructureExample Method

        private static void CreatingAStructureExample()
        {
            /* The first alien is declared and then has it X, Y and Lives data members set up. */
            Alien a;
            a.X = 50;
            a.Y = 50;
            a.Lives = 3;
            Console.WriteLine("a {0}", a.ToString());

            /* The second alien is created at the position 100, 100 on the screen. */
            Alien x = new Alien(100, 100);            
            Console.WriteLine("x {0}", x.ToString());

            /* Then an array of aliens are created. Each element in the array is initialized with the default values 
             * for each data member type (in other words the values of X, Y and Lives will be 0). */
            Alien[] swarm = new Alien[100];
            Console.WriteLine("swarm[0] {0}", swarm[0].ToString());
        }

        #endregion

        #region CreatingAnEnumExample Method

        private static void CreatingAnEnumExample()
        {
            AlienState x = AlienState.Attacking;
            /* The program prints the ToString result returned by the AlienState variable, which will output the
             * string "Attacking" in this case. */
            Console.WriteLine(x);
        }

        #endregion

        #region CreatingAReferenceExample Method

        private static void CreatingAReferenceExample()
        {
            /* A single AlienC reference was created, which is made to refer to a new AlienC object */
            AlienC x = new AlienC(100, 100);
            Console.WriteLine("x {0}", x);

            /* This is followed by the creation of an array, which contains 100 AlienC references. Each
             * reference in the array is then made to refer to a new AlienC object. The use of the new 
             * keyword to create a new object is said to create a new instance of a class.
             * Note that when a variable of type AlienC is declared, the variable is now a reference to 
             * an AlienC, and initially the reference does not refer to anything. */ 

            AlienC[] swarm = new AlienC[100];
            for (int i = 0; i < swarm.Length; i++)
            {
                /* When we created the alien swarm we had to explicitly set each element in the array 
                 * to refer to an AlienC instance */
                swarm[i] = new AlienC(0, 0);
            }

            Console.WriteLine("swarm[0] {0}", swarm[0]);
        }

        #endregion

        #region UsingGenericTypesExample Method

        private static void UsingGenericTypesExample()
        {
            MyStack<string> nameStack = new MyStack<string>();
            nameStack.Push("Rob");
            nameStack.Push("Mary");

            Console.WriteLine(nameStack.Pop());
            Console.WriteLine(nameStack.Pop());
        }

        #endregion
    }
}
