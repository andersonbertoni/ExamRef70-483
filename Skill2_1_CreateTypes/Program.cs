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
            UsingGenericTypes = 5,
            Constructors = 6,
            OverloadedConstructors = 7,
            CallingConstructors = 8,
            StaticConstructors = 9,
            StaticVariables = 10,
            SimpleMethod = 11,
            ExtensionMethods = 12,
            NamedParameters = 13,
            OptionalParameters = 14,
            IndexedProperties = 15,
            IndexingOnStrings = 16,
            OverloadedDateTimeConstructor = 17,
            MethodOverriding = 18,
            UsingBase = 19
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
                    case Items.Constructors:
                        ConstructorsExample();
                        break;
                    case Items.OverloadedConstructors:
                        OverloadedConstructorsExample();
                        break;
                    case Items.CallingConstructors:
                        CallingConstructorsExample();
                        break;
                    case Items.StaticConstructors:
                        StaticConstructorsExample();
                        break;
                    case Items.StaticVariables:
                        StaticVariablesExample();
                        break;
                    case Items.SimpleMethod:
                        SimpleMethodExample();
                        break;
                    case Items.ExtensionMethods:
                        ExtensionMethodsExample();
                        break;
                    case Items.NamedParameters:
                        NamedParametersExample();
                        break;
                    case Items.OptionalParameters:
                        OptionalParametersExample();
                        break;
                    case Items.IndexedProperties:
                        IndexedPropertiesExample();
                        break;
                    case Items.IndexingOnStrings:
                        IndexingOnStringsExample();
                        break;
                    case Items.OverloadedDateTimeConstructor:
                        OverloadedDateTimeConstructorExample();
                        break;
                    case Items.MethodOverriding:
                        MethodOverridingExample();
                        break;
                    case Items.UsingBase:
                        UsingBaseExample();
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

        class AlienCons
        {
            public int X;
            public int Y;
            public int Lives;

            /* A constructor has the same name as the object it is part of but does not have a return type */
            public AlienCons(int x, int y)
            {
                /* Constructors can perform validation of their parameters to ensure that any objects that are created contain
                 * valid information. If the validation fails, the constructor must throw an exception to prevent the creation
                 * of an invalid object.
                 * The code below throws an exception if an attempt is made to create an AlienCons with negative coordinate
                 * values */
                if (x < 0 || y < 0)
                    throw new ArgumentOutOfRangeException("Invalid position");

                X = x;
                Y = y;
                Lives = 3;
            }

            public override string ToString()
            {
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }

        /* The class below allows an alien to be constructed with a particular number of lives, or a value of three lives, 
         * depending on which constructor is called */
        class AlienOC
        {
            public int X;
            public int Y;
            public int Lives;

            public AlienOC(int x, int y)
            {
                if (x < 0 || y < 0)
                    throw new ArgumentOutOfRangeException("Invalid position");

                X = x;
                Y = y;
                Lives = 3;
            }

            public AlienOC(int x, int y, int lives)
            {                
                if (x < 0 || y < 0)
                    throw new ArgumentOutOfRangeException("Invalid position");

                X = x;
                Y = y;
                Lives = lives;
            }

            public override string ToString()
            {
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }

        /* A program can avoid code repetition by making one constructor call another constructor by use 
         * of the keyword this. It forms a call of another constructor in the object. In the program below
         * the parameters to the call of one constructor are passed into a call of another, along with an
         * additional lives value. Note that this means the actual body of the constructor is empty, because
         * all of the work is performed by the call to the other constructor. 
         * When creating objects that are part of a class hierarchy, a programmer must ensure that information
         * required by the constructor of a parent object is passed into a parent constructor. */
        class AlienOC2
        {
            public int X;
            public int Y;
            public int Lives;

            public AlienOC2(int x, int y) : this(x, y, 3)
            {
            }

            public AlienOC2(int x, int y, int lives)
            {
                if (x < 0 || y < 0)
                    throw new ArgumentOutOfRangeException("Invalid position");

                X = x;
                Y = y;
                Lives = lives;
            }

            public override string ToString()
            {
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }

        class AlienSC
        {
            public int X;
            public int Y;
            public int Lives;

            /* A class can contain a static constructor method. This is called once before the creation of the very first 
             * instance of the class. The code below contains is a static constructor that prints a message whe it is 
             * called. When the programs runs, the message is printed once, before the first alien is created. The static
             * constructor is not called when the second alien is created. */
            static AlienSC()
            {
                Console.WriteLine("Static AlienSC constructor running");
            }

            public AlienSC(int x, int y) : this(x, y, 3)
            {
            }

            public AlienSC(int x, int y, int lives)
            {
                if (x < 0 || y < 0)
                    throw new ArgumentOutOfRangeException("Invalid position");

                X = x;
                Y = y;
                Lives = lives;
            }

            public override string ToString()
            {
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }

        class AlienML
        {
            /* A static variable is a member of a type, but is not created for each instance of a type. A variable in a class is
             * made static by using the keyword static in the declaration of that variable.
             * The variable below is used to set a maximum for the number of lives that an alien is allowed to have. This is a 
             * value that should be stored once for all aliens. */
            public static int Max_Lives = 99;

            public int X;
            public int Y;
            public int Lives;
                        
            public AlienML(int x, int y) : this(x, y, 3)
            {
            }

            public AlienML(int x, int y, int lives)
            {
                if (x < 0 || y < 0)
                    throw new ArgumentOutOfRangeException("Invalid position");

                /* The static variable is used in the constructor to reject any attempts to create aliens with too many lives. */
                if (lives > Max_Lives)
                    throw new ArgumentOutOfRangeException("Invalid lives");

                X = x;
                Y = y;
                Lives = lives;
            }

            public override string ToString()
            {
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }

        class AlienMethod
        {            
            public static int Max_Lives = 99;

            public int X;
            public int Y;
            public int Lives;

            public AlienMethod(int x, int y) : this(x, y, 3)
            {
            }

            public AlienMethod(int x, int y, int lives)
            {
                if (x < 0 || y < 0)
                    throw new ArgumentOutOfRangeException("Invalid position");
                                
                if (lives > Max_Lives)
                    throw new ArgumentOutOfRangeException("Invalid lives");

                X = x;
                Y = y;
                Lives = lives;
            }

            /* A method is a member of a class. It has a signature and a body. The signature defines the type
             * and number of parameters that the method will accept. The body is a block of code that is performed
             * when the method is called. If the method has a type other than void, all code paths through the body
             * of the code must end with a return statement that returns a value of the type of the method. 
             * The method below is called to remove lives from an alien. The method is provided with a parameter that
             * gives the number of lives to remove. The method is of type Boolean and returns true if the alien is 
             * still alive and false if it is not. */
            public bool RemoveLives(int livesToRemove)
            {
                Lives = Lives - livesToRemove;

                /* If the number of lives that are left is less than zero, the lives is set to zero and the alien is 
                 * moved off the display screen so that it is not visible any more. */
                if(Lives <= 0)
                {
                    Lives = 0;
                    X = -1000;
                    Y = -1000;
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public override string ToString()
            {
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }        

        /* A program can access a particular array element by using an index value that identifies the element. 
         * A class can use the same indexing mechanism to provide indexed property values. 
         * The class below is a wrapper around an integer array. The indexer property accepts an integer value
         * (called i in the class), which is used to index the array that stores the value. */
        class IntArrayWrapper
        {
            //Create an array to store the values
            private int[] array = new int[100];

            //Declare an indexer property
            public int this[int i]
            {
                get { return array[i]; }
                set { array[i] = value; }
            }
        }

        /* Note that there is nothing stopping the use of other types in indexed properties. This is how the 
         * Dictionary collection is used to index on a particular type of key. The class below shows an 
         * indexer property that is extended to allow all of the elements in the integer array to be accessed
         * by name. */
        class NamedIntArray
        {
            //Create an array to store the values
            private int[] array = new int[100];

            //Only the first couple of values have been implemented for reasons of space
            public int this[string name]
            {
                get
                {
                    switch(name)
                    {
                        case "zero":
                            return array[0];
                        case "one":
                            return array[1];
                        default:
                            return -1;
                    }
                }
                set
                {
                    switch(name)
                    {
                        case "zero":
                            array[0] = value;
                            break;
                        case "one":
                            array[1] = value;
                            break;
                    }
                }
            }
        }

        /* The overriding of methods takes place when class hierarchies are used. In a class hierarchy a child 
         * class is derived from a parent or base class. A method in base class is overridden by a method in a 
         * child when the child class contains a method with exactly the same name and signature as a method in 
         * the parent class. Only methods that have been marked as virtual in the parent class can be overridden.
         * The underlying principle of a class hierarchy is that classes at the top of the hierarchy are more
         * abstract, and classes toward the bottom of the hierarchy are more specific. The example below, the base
         * class are called Document and child class are called Invoice. 
         * The Document class will hold all of the behaviors that are common to all documents, for example, all 
         * documents must provide a method that gets the date when the document was created. However, the print 
         * behavior of an Invoice will have to be different from the print behavior of the more abstract Document
         * type. Overriding allows us to create a print method in the Invoice that overrides that in the Document. */
        class Document
        {
            /* The GetDate method is declared in Document. */
            //All documents have the same GetDate behavior so this method will not be overriden
            public void GetDate()
            {
                Console.WriteLine("Hello from GetDate in Document");
            }

            /* The DoPrint is declared in Document and overridden in Invoice. */
            //A document may have its own DoPrint behavior so this method is virtual so it can be overriden
            public virtual void DoPrint()
            {
                Console.WriteLine("Hello from DoPrint in Document");
            }
        }

        class Invoice : Document
        {
            public override void DoPrint()
            {
                Console.WriteLine("Hello from DoPrint in Invoice");
            }
        }

        /* If you create a PrePaidInvoice to represent pre-paid invoices, you can extend the Invoice class and provide
         * an override of the DoPrint method in the PrePaidInvoice class. The DoPrint method in PrePaidInvoice can use
         * the DoPrint method in the parent class by using the base keyword. */
        class PrePaidInvoice : Invoice
        {
            public override void DoPrint()
            {
                base.DoPrint();
                Console.WriteLine("Hello from DoPrint in PrePaidInvoice");
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

        #region ConstructorsExample Method
        
        private static void ConstructorsExample()
        {
            AlienCons x = new AlienCons(100, -100);
            Console.WriteLine("x {0}", x);
        }

        #endregion

        #region OverloadedConstructorsExample Method

        private static void OverloadedConstructorsExample()
        {
            AlienOC x = new AlienOC(100, 100);
            AlienOC y = new AlienOC(90, 90, 4);

            Console.WriteLine("x {0}", x);
            Console.WriteLine("y {0}", y);
        }

        #endregion

        #region CallingConstructorsExample Method

        private static void CallingConstructorsExample()
        {
            AlienOC2 x = new AlienOC2(100, 100);
            AlienOC2 y = new AlienOC2(90, 90, 4);

            Console.WriteLine("x {0}", x);
            Console.WriteLine("y {0}", y);
        }

        #endregion

        #region StaticConstructorsExample Method

        private static void StaticConstructorsExample()
        {
            /* The static constructor is called before the first alien is created. */
            AlienSC x = new AlienSC(100, 100);
            /* The static constructor is not called when the second alien is created. */
            AlienSC y = new AlienSC(90, 90, 4);
                        
            Console.WriteLine("x {0}", x);
            Console.WriteLine("y {0}", y);
        }

        #endregion

        #region StaticVariablesExample Method

        private static void StaticVariablesExample()
        {
            try
            {
                AlienML x = new AlienML(100, 100);
                AlienML y = new AlienML(100, 100, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} - {1}", ex.GetType().ToString(), ex.Message);
            }

            try
            {
                /* Code outside of the AlienML class must refer to the Max_Lives static variable via the class name, 
                 * rather than the name of any particular instance of the class. The statement next change the value
                 * of Max_Lives to 150. 
                 * Making a variable static does not stop it from being changed when the program runs (to achieve this
                 * use the const keyword or make the variable readonly). Rather, the word static in this context means
                 * that the variable is "always present". A program can use a static variable from a type without 
                 * needing to have created any instances of that type. */
                AlienML.Max_Lives = 150;
                AlienML z = new AlienML(100, 100, 100);

                Console.WriteLine("z {0}", z);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} - {1}", ex.GetType().ToString(), ex.Message);
            }
        }

        #endregion

        #region SimpleMethodExample Method

        private static void SimpleMethodExample()
        {
            AlienMethod x = new AlienMethod(100, 100);
            Console.WriteLine("x {0}", x);

            /* The name of the method is best expressed in a "verb-noun" manner, with an action followed by the thing 
             * that the action is acting on. When talking about the method signature and the code body of a method we 
             * will talk in terms of the parameters used in the method. In the case of the call of a method we will 
             * talk in terms of the arguments supplied to the call. In other words, the parameter to the method is
             * called livesToRemove and the argument to the method call is the value 2. */
            if(x.RemoveLives(4))
            {
                Console.WriteLine("Still alive");
            }
            else
            {
                Console.WriteLine("Alien destroyed");
            }

            Console.WriteLine("x {0}", x);
        }

        #endregion

        #region ExtensionMethodsExample Method

        /* The class with the extension method is in the MyExtensions.cs file. */
        /* Once the extension method has been created it can be used from the namespace in which the class
         * containing the method is declared. When the programs calls an extension method the compiler 
         * searches the included namespaces for a matching method for that type, and then generates a call
         * of that method. When the program below runs, it prints out the number of lines in the string text. */
        private static void ExtensionMethodsExample()
        {
            string text = @"A rocket explorer called Wright, 
Once travelled much faster than light,
He set out one day,
In a relative way,
And returned on the previous night";

            Console.WriteLine(text.LineCount());
        }

        #endregion

        #region NamedParametersExample Methods

        /* When you create a method with parameters, the signature of the method gives the name and the type
         * of each parameter in turn. 
         * The method below has been defined as having three parameters. A call of the method must have three
         * argument values: a prompt, a low value, and a high value. */
        static int ReadValue(int low, int high, string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);

                int number;
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Invalid number text");
                    continue;
                }

                if(number < low || number > high)
                {
                    Console.WriteLine("Number should be less than {0} and greater than {1}", high, low);
                    continue;
                }

                return number;
            }            
        }

        private static void NamedParametersExample()
        {
            // The following call of ReadValue is rejected by the compiler:
            //int age = ReadValue("Enter your age: ", 1, 100);
            /* This is because the prompt string is defined as the last parameter to the method call, not the 
             * first. If you want to make method calls without worrying about the order of the arguments, you
             * can name each one when you call the method: */
            int age = ReadValue(prompt: "Enter your age: ", low: 1, high: 100);

            /* Now the compiler is using the name of each argument, rather than its position in the list. 
             * Another programmer reading your code can now see the meaning of each argument value. Using
             * this format also removes the possibility of any confusion of the ordering of the values in 
             * the method call. */

            Console.WriteLine("Your age is: {0}", age);
        }

        #endregion

        #region OptionalParametersExample Methods

        /* Sometimes the value of an argument might have a sensible default value. For example, if you only want 
         * the ReadValue to fetch a value from the user and not display a prompt, you can do this by providing 
         * an empty string:
         * 
         *      x = ReadValue(low: 25, high: 100, prompt: "");
         *      
         * This, however, is a bit messy. Instead, you can change the definition of the method to give a default
         * value for the prompt parameter as shown below. 
           Optional parameters must be provided after all of the required ones. */
        static int ReadValue(double low, double high, string prompt = "")
        {
            while (true)
            {
                Console.Write(prompt);

                int number;

                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Invalid number text");
                    continue;
                }

                if (number < low || number > high)
                {
                    Console.WriteLine("Number should be less than {0} and greater than {1}", high, low);
                    continue;
                }
                return number;
            }
        }

        private static void OptionalParametersExample()
        {
            /* When the method runs, the prompt will be set to an empty string if the user doesn't provide 
             * a value. */
            int age = ReadValue(1, 100);
            Console.WriteLine("You entered: {0}", age);
        }

        #endregion

        #region IndexedPropertiesExample Method

        private static void IndexedPropertiesExample()
        {
            IntArrayWrapper x = new IntArrayWrapper();
            x[0] = 99;
            Console.WriteLine(x[0]);
        }

        #endregion

        #region IndexingOnStringsExample Method

        private static void IndexingOnStringsExample()
        {
            /* A program can access elements in the array by specifying a text indexer for the location.
             * This program stores the value 99 in location "zero" */
            NamedIntArray x = new NamedIntArray();
            x["zero"] = 99;
            Console.WriteLine(x["zero"]);
        }

        #endregion

        #region OverloadedDateTimeConstructorExample Method

        /* Overloading - "providing a method with the same name, but a different signature in a given type".
         * It is useful when you want to provide several ways of performing a particular behavior, depending
         * on the circumstances in which the behavior is being used. 
         * The DateTime structure provided by .NET has a large number of overloaded constructors, because
         * there are many different ways a programmer might want to initialize a DateTime value.
         * You might have the number of ticks since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.
         * Alternatively, and perhaps more likely, you might have year, month, and day values. */
        private static void OverloadedDateTimeConstructorExample()
        {            
            /* This program shows how these values are used to create DateTime values. Both of the DateTime
             * values are set to the same date and time by this code. */
            DateTime d0 = new DateTime(ticks: 636679008000000000);           
            DateTime d1 = new DateTime(year: 2018, month: 7, day: 23);
         
            Console.WriteLine(d0);
            Console.WriteLine(d1);
        }

        #endregion

        #region MethodOverridingExample Method

        private static void MethodOverridingExample()
        {
            /* The program creates an instance of Invoice... */
            Invoice c = new Invoice();

            /* ... and then calls the GetDate and DoPrint methods on the instance.
             * When GetDate is called, the GetDate from Document is called. When 
             * DoPrint is called, the DoPrint from Invoice is called, because this
             * overrides the DoPrint in the parent class. */
            c.GetDate();
            c.DoPrint();
        }

        #endregion

        #region UsingBaseExample Method

        private static void UsingBaseExample()
        {
            PrePaidInvoice p = new PrePaidInvoice();
            p.GetDate();
            /* When the DoPrint method is called on an instance of the PrePaidInvoice class, it first
             * makes a call of the DoPrint method in the parent object. */
            p.DoPrint();
        }

        #endregion
    }
}
