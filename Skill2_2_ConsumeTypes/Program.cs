﻿using Shared;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill2_2_ConsumeTypes
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            BoxingAndUnboxing = 1,
            TypeConversion = 2,
            BadDynamicCode = 3,
            UsingDynamicVariables = 4,
            InteractingWithExcel = 5
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
                    case Items.BoxingAndUnboxing:
                        BoxingAndUnboxingExample();
                        break;
                    case Items.TypeConversion:
                        TypeConversionExample();
                        break;
                    case Items.BadDynamicCode:
                        BadDynamicCodeExample();
                        break;
                    case Items.UsingDynamicVariables:
                        UsingDynamicVariablesExample();
                        break;
                    case Items.InteractingWithExcel:
                        InteractingWithExcelExample();
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

        /* The .NET runtime provides a set of conversion methods that are used to performing casting.
         * You can also write your own type conversion operators for your data classes so that programs
         * can perform implicit and explicit conversions between types.
         * The type Miles contains a double precision distance value in the property Distance. The Miles
         * class also contains an implicit operator called Kilometers, which returns a value of type 
         * Kilometers, representing the same distance. The type Miles also contains an explicit
         * conversion that returns the distance value as an integer. This conversion is explicit
         * because it is a narrowing operation that will result in a loss of data, as the fractional
         * part of the double precision distance value is truncated. */
        class Miles
        {
            public double Distance { get; }

            //Conversion operator for implicit conversion to Kilometers
            public static implicit operator Kilometers(Miles t)
            {
                Console.WriteLine("Implicit conversion from miles to kilometers");
                return new Kilometers(t.Distance * 1.6);
            }

            public static explicit operator int (Miles t)
            {
                Console.WriteLine("Explicit conversion from miles to int");
                return (int)(t.Distance + 0.5);
            }

            public Miles(double miles)
            {
                Distance = miles;
            }
        }

        class Kilometers
        {
            public double Distance { get; }

            public Kilometers(double kilometers)
            {
                Distance = kilometers;
            }
        }

        /* C# is a strongly typed language. This means that when the program is compiled the compiler ensures 
         * that all actions that are performed are valid in the context of the types that have been defined
         * in the program. As an example, if a class does not contain a method with a particular name, the C#
         * compiler will refuse to generate a call to that method. As a way of making sure that C# programs 
         * are valid at the time that they are executed, strong typing works very well. Such a strong typing
         * regime, however, can cause problems when a C# program is required to interact with systems that do
         * not have their origins in C# code. Such situations arise when using Common Object Model (COM)
         * interop, the Document Object Model (DOM), working with objects generated by C# reflection, or when
         * interworking with dynamic languages such as Javascript. 
         * In this situations, you need a way to force the compiler to interact with objects for which the strong
         * typing information that is generated from compiled C# is not available. The keyword dynamic is used
         * to identify items for which the C# compiler should suspend static type checking. The compiler will
         * then generate code that works with the items as described, without doing static checking to make sure
         * that they are valid. Note that this doesn't mean that a program using dynamic objects will always work;
         * if the description is incorrect the program will fail at run time. */
        class MessageDisplay
        {
            public void DisplayMessage(string message)
            {
                Console.WriteLine(message);
            }
        }

        #endregion

        /***************************************************************************/

        #region BoxingAndUnboxingExample Method

        /* We know that C# programs can use value types and reference types. We know that value types
         * are managed directly in terms of their value, whereas reference types are managed in terms 
         * of a reference that refers to an object that holds the data. The bult-in types int, float, 
         * and double are value types, as are structures that we create. Classes are used to define
         * reference types.
         * From a computational point of view, value types such as int and float have the advantage
         * that the computer processor can manipulate value types directly. Adding two int values
         * together can be achieved by fetching the values into the processor, performing the addition
         * operation, and then storing the result.
         * It can be useful to treat value types as reference types, and the C# runtime system provides
         * a mechanism called boxing that will perform this conversion when required.
         * The example below shows boxing and unboxing being performed in a program. The first statement
         * takes the value 99 and casts this into an object. The second statement takes the object and 
         * casts it back into an integer. The process of converting from a reference type into a value
         * type is called unboxing. */
        private static void BoxingAndUnboxingExample()
        {
            //the value 99 is boxed into an object
            object o = 99;

            //the boxed object is unboxed back into an int
            int oVal = (int)o;

            Console.WriteLine(oVal);            
        }

        #endregion

        #region TypeConversionExample Method

        private static void TypeConversionExample()
        {
            Miles m = new Miles(100.5);

            /* A program can assign a variable of type Miles value into a variable of type Kilometer.
             * During the assignment the implicit conversion operator is called automatically. */
            Kilometers k = m; //implicitly convert miles 
            Console.WriteLine("kilometers: {0}", k.Distance);

            // To use an explicit conversion the programmer must use a cast as below
            int intMiles = (int)m; //explicitly convert miles to int
            Console.WriteLine("Int miles: {0}", intMiles);            
        }

        #endregion

        #region BadDynamicCodeExample Method

        private static void BadDynamicCodeExample()
        {
            /* The class MessageDisplay contains a single method, called DisplayMessage.
             * The variable m is set to refer to an instance of this class, and the program calls the DisplayMessage
             * method on this reference. */
            MessageDisplay m = new MessageDisplay();
            m.DisplayMessage("Hello World");

            /* The variable d is declared as dynamic and set to refer to a MessageDisplay instance. The program
             * then contains a call of a method called Banana on the variable d. Normally this would not compile,
             * because the compiler can see that this method is not present in the class. Because the variable d
             * has been declared as dynamic, however, the program will compile with no errors, but when the program
             * is executed and exception will be generated when the Banana method is called. */
            dynamic d = new MessageDisplay();
            d.Banana("Hello World");
        }

        #endregion

        #region UsingDynamicVariablesExample Method

        /* The aspect of the dynamic keyword makes it possible to interact with objects that have behaviors, 
         * but not the C# type information that the C# compiler would normally use to ensure that any 
         * interaction is valid. There is, however, more to the dynamic type than this.
         * A variable declared as dynamic is allocated a type that is inferred from the context in which it 
         * is used. This is a very similar behavior to that of variables in languages such as Python or
         * Javascript. */
        private static void UsingDynamicVariablesExample()
        {
            /* The variable d is declared as dynamic and used first as an integer and secondly as a string.
             * This program will compile and execute with no errors, printing out the results that you would
             * expect. The type of the variable d will change according to what is stored in it, and the 
             * addition operator works as expected, adding an integer to an integer and a string to a string.
             * If the program behaves incorrectly (for example by trying to add "Rob" to the integer 
             * incarnation of d) an exception is thrown when the program runs. */
            dynamic d = 99;
            d = d + 1;
            Console.WriteLine(d);

            d = "Hello ";
            d = d + "Rob";
            Console.WriteLine(d);

            /* The ExpandoObject class allows a program to dynamically add properties to an object. The code
             * next, shows how this is done. The dynamic variable person is assigned to a new ExpandoObject
             * instance. The program then adds Name and Age properties to the person and then prints out 
             * these values. 
             * A program can add ExpandoObject properties to an ExpandoObject to create nested data 
             * structures. An ExpandoObject can also be queried using LINQ and can exposes the IDictionary
             * interface to allow its contents to be queried and items to be removed.
             * ExpandoObject is especially useful when creating data structures from markup languages, for
             * example when reading a JSON or XML document. */
             
            dynamic person = new ExpandoObject();
            person.Name = "Rob Miles";
            person.Age = 21;

            Console.WriteLine("Name: {0} Age: {1}", person.Name, person.Age);
        }

        #endregion

        #region InteractingWithExcelExample Method

        /* The C# code uses dynamic types to make the interaction with the Office application very easy. 
         * There is no need to cast the various elements that the program is interacting with, as they
         * are exposed by the interop as dynamic types, so conversion is performed automatically based
         * on the inferred type of an assignment destination.
         * The code below opens Excel, creates a new spreadsheet, and adds texto into two cells. */
        private static void InteractingWithExcelExample()
        {
            //Create the interop
            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            //Make the app visible
            excelApp.Visible = true;

            //Add a new workbook
            excelApp.Workbooks.Add();

            /* Obtain the active sheet from the app.
             * There is no need to cast this dynamic type. */
            Microsoft.Office.Interop.Excel.Worksheet worksheet = excelApp.ActiveSheet;

            //Write into two cells
            worksheet.Cells[1, "A"] = "Hello";
            worksheet.Cells[1, "B"] = "from C#";
        }

        #endregion
    }
}
