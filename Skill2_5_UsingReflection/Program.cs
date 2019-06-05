/* If the TERSE symbol is defined the body of the TerseReport method will be obeyed when the method
 * is called. */
#define TERSE

/* If the VERBOSE symbol is defined the body of the VerboseReport method will be obeyed. */ 
#define VERBOSE

using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill2_5_UsingReflection
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            ConditionalAttribute = 1
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
                    case Items.ConditionalAttribute:
                        ConditionalAttributeExample();
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

        /* The Serializable attribute doesn't actually hold any data, it is the fact that a
         * class has a Serializable atttribute instance attached to it means that the class
         * is may be opened and read by a serializer.
         * A serializer takes the entire contents of a class and sends it into a stream. There
         * are possible security implications in serializing a class and so C# requires that a 
         * class should "opt in" to the serialization process.
         * The class below contains name and age information. The Person class has the 
         * Serializable attribute attached to it; this is expressed by giving the name of the
         * attribute enclosed in square brackets, just before the declaration of the class.
         * The Person class also contains a NonSerialized attribute that is applied to the
         * screenPosition member variable. This member of the class is only used to manage
         * the display of a Person object and should not be saved when it is serialized. This
         * tells the serializer not to save the value of screenPosition. */
        [Serializable]
        public class Person
        {
            public string Name;
            public int Age;

            [NonSerialized]
            //No need to save this
            private int screenPosition;

            public Person(string name, int age)
            {
                this.Name = name;
                this.Age = age;
                this.screenPosition = 0;
            }
        }

        #endregion

        #region ConditionalAttributeExample Methods

        /* You can use the Conditional attribute to activate and deactivate the contents of 
         * methods. This attribute is declared in the System.Diagnostics namespace.
         * The symbols TERSE and VERBOSE can be used to select the level of logging that is
         * performed by a program.
         * the body of the ReportHeader method will be obeyed if either the TERSE or the 
         * VERBOSE symbols are defined because two attributes are combined before that method
         * definition.
         * Note that the Conditional attribute controls whether or not the body of a given
         * method is obeyed whe the method is called, it does not control whether or not the
         * method itself is passed to the compiler. The Conditional attribute does not perform
         * the same function as conditional compilation in languages such as C and C++, it does
         * not prevent code from being passed to the compiler, rather it controls wheter code is
         * executed when it runs. */
        [Conditional("VERBOSE"), Conditional("TERSE")]
        static void ReportHeader()
        {
            Console.WriteLine("This is the header for the report");
        }

        [Conditional("VERBOSE")]
        static void VerboseReport()
        {
            Console.WriteLine("This is output from the verbose report");
        }

        [Conditional("TERSE")]
        static void TerseReport()
        {
            Console.WriteLine("This is output from the terse report");
        }

        private static void ConditionalAttributeExample()
        {
            ReportHeader();
            TerseReport();
            VerboseReport();
        }

        #endregion
    }
}
