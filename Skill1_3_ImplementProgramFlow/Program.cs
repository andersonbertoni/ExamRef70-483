using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill1_3_ImplementProgramFlow
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            WhileLoops = 1,
            DoWhileLoops = 2,
            ForLoops = 3,
            IterateWithFor = 4,
            IterateWithForeach = 5,
            UppercasePerson = 6,
            UsingBreak = 7,
            UsingContinue = 8,
            IfConstruction = 9,
            LogicalExpressions = 10,
            SwitchConstruction = 11,
            SwitchingOnStrings = 12,
            ExpressionEvaluation = 13
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
                    case Items.WhileLoops:
                        WhileLoopsExample();
                        break;
                    case Items.DoWhileLoops:
                        DoWhileLoopsExample();
                        break;
                    case Items.ForLoops:
                        ForLoopsExample();
                        break;
                    case Items.IterateWithFor:
                        IterateWithForExample();
                        break;
                    case Items.IterateWithForeach:
                        IterateWithForeachExample();
                        break;
                    case Items.UppercasePerson:
                        UppercasePersonExample();
                        break;
                    case Items.UsingBreak:
                        UsingBreakExample();
                        break;
                    case Items.UsingContinue:
                        UsingContinueExample();
                        break;
                    case Items.IfConstruction:
                        IfConstructionExample();
                        break;
                    case Items.LogicalExpressions:
                        LogicalExpressionsExample();
                        break;
                    case Items.SwitchConstruction:
                        SwitchConstructionExample();
                        break;
                    case Items.SwitchingOnStrings:
                        SwitchingOnStringsExample();
                        break;
                    case Items.ExpressionEvaluation:
                        ExpressionEvaluationExample();
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
        
        #region Variables

        static int counter;

        #endregion

        #region While Loops Example Method

        private static void WhileLoopsExample()
        {
            //Infinite Loop - valid loop
            //while(true)
            //{
            //    Console.WriteLine("Hello");
            //}

            //Loop that never writes Hello
            //while(false)
            //{
            //    Console.WriteLine("Hello");
            //}

            //prints Hello message 10 times
            int count = 0;
            while(count < 10)
            {
                Console.WriteLine("Hello {0}", count);
                count = count + 1;
            }
        }

        #endregion

        #region Do While Loops Example Method

        private static void DoWhileLoopsExample()
        {
            do
            {
                Console.WriteLine("Hello");
            } while (false);
        }

        #endregion

        #region For Loops Example Methods

        static void Initialize()
        {
            Console.WriteLine("Initialize called");
            counter = 0;
        }

        static void Update()
        {
            Console.WriteLine("Update called");
            counter = counter + 1;
        }

        static bool Test()
        {
            Console.WriteLine("Test called");
            return counter < 5;
        }

        private static void ForLoopsExample()
        {
            for(Initialize(); Test(); Update())
            {
                Console.WriteLine("Hello {0}", counter);
            }

            //conventional for loop
            for(int counter2 = 0; counter2 < 5; counter2++)
            {
                Console.WriteLine("Hello {0}", counter2);
            }
        }

        #endregion

        #region Iterate With For Example, Iterate With Foreach Example Method

        static string[] LoadNames()
        {
            return new string[]{ "Rob", "Mary", "David", "Jenny", "Chris", "Inogen" };
        }

        #endregion

        #region Iterate With For Example Method

        private static void IterateWithForExample()
        {
            string[] names = LoadNames();

            for(int index = 0; index < names.Length; index++)
            {
                Console.WriteLine(names[index]);
            }
        }

        #endregion

        #region Iterate With Foreach Example Method

        private static void IterateWithForeachExample()
        {
            string[] names = LoadNames();

            foreach(string name in names)
            {
                Console.WriteLine(name);
            }

            // The following code generates a compilation error, because the names array 
            // holds a collection of strings, not integers
            //foreach(int name in names)
            //{
            //    Console.WriteLine(name);
            //}

            // It isn't possible for code in a foreach construction to modify the iterating value.
            // The following code does not compile
            //foreach(string name in names)
            //{
            //    name = name.ToUpper();
            //}
        }

        #endregion

        #region Uppercase Person Example Method and class

        class Person
        {
            public string Name { get; set; }

            public Person(string name)
            {
                this.Name = name;
            }
        }

        //If the foreach loop is working on a list of references to objects, 
        //the objects on the ends of those references can be changed
        private static void UppercasePersonExample()
        {
            List<Person> people = new List<Person>();
            string[] names = LoadNames();

            foreach(string name in names)
            {
                people.Add(new Person(name));
            }

            //The following code works through a list of Person objects, changing the 
            //Name property of each person in the list to upper case. 
            foreach(Person person in people)
            {
                person.Name = person.Name.ToUpper();
            }

            foreach (Person person in people)
            {
                Console.WriteLine(person.Name);
            }
        }

        #endregion

        #region Using Break Example Method

        private static void UsingBreakExample()
        {
            string[] names = LoadNames();

            for (int index = 0; index < names.Length; index++)
            {
                Console.WriteLine(names[index]);
                if (names[index] == "David")
                    break;
            }
        }

        #endregion

        #region Using Continue Example Method

        private static void UsingContinueExample()
        {
            string[] names = LoadNames();
            for (int index = 0; index < names.Length; index++)
            {
                //The program will not print out the name "David" because the conditional statement will trigger, 
                //causing the continue statement to be performed, and abandoning that pass through the loop
                if (names[index] == "David")
                    continue;

                Console.WriteLine(names[index]);
            }
        }

        #endregion

        #region If Construction Example Method

        private static void IfConstructionExample()
        {
            if(true)
            {
                Console.WriteLine("This statement is always performed");
            }
            else
            {
                Console.WriteLine("This statement is never performed");
            }

            //it is possible to "nest" if constructions inside one another
            if (true)
            {
                Console.WriteLine("This statement is always performed");
                if (true)
                {
                    Console.WriteLine("This statement is always performed");
                }
                else
                {
                    Console.WriteLine("This statement is never performed");
                }
            }

            //The else binds to the "nearest" if. If you want to modify this binding you
            //can use braces to force different bindings.
            if (true)
            {
                Console.WriteLine("This statement is always performed");
                if (true)
                {
                    Console.WriteLine("This statement is always performed");
                }
            }
            else
            {
                Console.WriteLine("This statement is never performed");
            }
        }

        #endregion

        #region Logical Expression Example Methods

        static int mOne()
        {
            Console.WriteLine("mOne called");
            return 1;
        }

        static int mTwo()
        {
            Console.WriteLine("mTwo called");
            return 2;
        }
        
        private static void LogicalExpressionsExample()
        {
            /* The program only outputs a message from mOne.
             * This is because the condition involving the value 
             * returned by mOne evaluates to false, which means
             * there is no need to call mTwo. */
            if(mOne() == 2 && mTwo() == 1)
            {
                Console.WriteLine("Hello World");
            }

            /* The program only outputs a message from mOne.
             * This is because the condition involving the value 
             * return by mOne evaluates to true, which means
             * there is no need to call mTwo.
             * The program also outputs Hello World, because the
             * condition evaluates to true. */            
            if(mOne() == 1 || mTwo() == 2)
            {
                Console.WriteLine("Hello World");
            }
        }

        #endregion

        #region Switch Construction Example Method

        private static void SwitchConstructionExample()
        {
            Console.Write("Enter command: ");
            int command = int.Parse(Console.ReadLine());

            switch(command)
            {
                case 1:
                    Console.WriteLine("Command 1 chosen");
                    break;
                case 2:
                    Console.WriteLine("Command 2 chosen");
                    break;
                case 3:
                    Console.WriteLine("Command 3 chosen");
                    break;
                default:
                    Console.WriteLine("Please enter a command in the range 1-3");
                    break;
            }
        }

        #endregion

        #region Switching On Strings Example Method

        private static void SwitchingOnStringsExample()
        {
            Console.Write("Enter command: ");
            string commandName = Console.ReadLine().ToLower();

            switch(commandName)
            {
                case "save":
                case "s":
                    Console.WriteLine("Save command");
                    break;
                case "load":
                case "l":
                    Console.WriteLine("Load command");
                    break;
                case "exit":
                case "e":
                    Console.WriteLine("Exit command");
                    break;
                default:
                    Console.WriteLine("Please enter save, load or exit");
                    break;
            }
        }

        #endregion

        #region Expression Evaluation Example Method

        private static void ExpressionEvaluationExample()
        {
            int i = 0; //create i and set to 0
            //Monadic operators - one operand
            i++; //monadic ++ operator increment - i now 1
            i--; //monadic -- operator decrement - i now 0

            //Postfix monadic operator - perform after value given
            Console.WriteLine(i++); //writes 0 and sets i to 1
            //Prefix monadic operator - perform before value given
            Console.WriteLine(++i); //writes 2 and sets i to 2

            //Binary operators - two operands
            i = 1 + 1; //sets i to 2
            i = 1 + 2 * 3; //sets i to 7 because * performed first
            i = (1 + 2) * 3; // sets i to 9 because + performed first

            string str = "";
            str = str + "Hello"; //performs string addition

            //ternary operators - three operands
            i = true ? 0 : 1; //sets i to 0 because condition is true
        }

        #endregion
    }
}
