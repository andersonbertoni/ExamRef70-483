using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill2_3_EnforceEncapsulation
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            PublicDataMembers = 1,
            UsingAProperty = 2
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
                    case Items.PublicDataMembers:
                        PublicDataMembersExample();
                        break;
                    case Items.UsingAProperty:
                        UsingAPropertyExample();
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

        class Customer
        {
            public string Name;
        }

        /* The example below creates a Name property in the Customer2 class that performs validation
         * of the name. */
        class Customer2
        {
            private string _nameValue;

            /* A property is declared as having a get behavior and a set behavior. 
             * The Name property can throw an exception when an attempt is made to set an empty string.
             * Do this to ensure that the user of the Customer class is always made aware of any error
             * conditions. You can make the set behavior ignore invalid names, or set the name to a 
             * default string when an invalid name is provided. */
             */
            public string Name
            {
                /* The get behavior is used when a program gets a value in the property.
                 * The get behavior for the Name property returns the value of a private class member
                 * variable called _nameValue, which holds the value of the name of the customer. */
                get
                {
                    return _nameValue;
                }
                /* The set behavior is used when a program sets a value in the property.
                 * Within the set behavior for the Name property, the keyword value represents the
                 * value being assigned to the property. If this value is an empty string the set
                 * behavior throws an exception to prevent an empty string begin set as a Name.
                 * If the value is valid, the set behavior sets _nameValue to the incoming name.
                 * Note that there is a C# convention that private members of a class have identifiers
                 * that start with an underscore (_) character */                 
                set
                {
                    if (value == "")
                        throw new Exception("Invalid customer name");

                    _nameValue = value;
                }
            }
        }

        #endregion

        #region PublicDataMembersExample Method

        /* The code below shows a program that uses a class called Customer, which you might use to store
         * customer records. At the moment the Customer contains a single data member called Name, which
         * is a string containing the name of the customer. The Name member has been made public, so that
         * it can be set to the name of the customer.
         * This program works well, but it doesn't provide any control over the contents of the customer
         * name. The name of the costumer can be set to any string, including an empty string. You should
         * stop users of the Customer object from setting a customer name to an empty string. We call this
         * "enforcing business roles" on our applications. You may have other rules to enforce, which 
         * restrict the characters that can be used in a name and set limits for the minimum and maximum 
         * length of a customer name. */
        private static void PublicDataMembersExample()
        {
            Customer c = new Customer();
            c.Name = "Rob";
            Console.WriteLine("Customer name: {0}", c.Name);
        }

        #endregion

        #region UsingAPropertyExample Method

        private static void UsingAPropertyExample()
        {            
            Customer2 c = new Customer2();

            /* Adding validation to the Name property does not change how the property is used in a program.
             * The code here sets the name of customer c to Rob... */
            c.Name = "Rob";
            Console.WriteLine("Customer name: {0}", c.Name);

            try
            {
                /* ... and then tries to set the name to an empty string, which causes an exception to be
                 * thrown. */
                c.Name = ""; //this statement will throw an exception
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
