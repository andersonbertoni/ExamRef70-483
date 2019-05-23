using Shared;
using System;
using System.Collections.Generic;
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
            BoxingAndUnboxing = 1
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
    }
}
