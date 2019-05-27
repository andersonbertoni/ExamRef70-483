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
            UsingAProperty = 2,
            CreatingAccessorMethods = 3,
            ProtectedAccess = 4,
            PrintingInterface = 5
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
                    case Items.CreatingAccessorMethods:
                        CreatingAccessorMethodsExample();
                        break;
                    case Items.ProtectedAccess:
                        ProtectedAccessExample();
                        break;
                    case Items.PrintingInterface:
                        PrintingInterfaceExample();
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

        #region Classes and Interfaces

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
            public string Name
            {
                /* The get behavior is used when a program gets a value in the property.
                 * The get behavior for the Name property returns the value of a private class member
                 * variable called _nameValue, which holds the value of the name of the customer. 
                 * This value is called the backing value of the property. */
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

            /* If you just want to implement a class member as a property, but don't want to get control
             * when the property is accessed, you can use auto-implemented properties. The statement below
             * creates an integer property called Age. The C# compiler automatically creates the backing
             * values. If you want to add get and set behaviors and your own backing value later, you can
             * do this.
            
                public int Age { get; set; }

            */
        }

        /* The class below shows how public and private access modifiers can be used to create a very simple
         * bank account application that uses methods to provide access to the account balance value. */
        class BankAccount
        {
            /* The member variable _accountBalance is made private to the BankAccount class. This means that
             * it cannot be accessed by code running outside the BankAccount. */
            private decimal _accountBalance = 0;

            /* The method members PayInFunds, WithdrawFunds, and GetBalance are declared as public, which 
             * means that code running outside the BankAccount class can use these methods to interact with
             * the account balance value in a managed way. */
            public void PayInFunds(decimal amountToPayIn)
            {
                _accountBalance = _accountBalance + amountToPayIn;
            }

            public bool WithdrawFunds(decimal amountToWithdraw)
            {
                if (amountToWithdraw > _accountBalance)
                    return false;

                _accountBalance = _accountBalance - amountToWithdraw;
                return true;
            }

            public decimal GetBalance()
            {
                return _accountBalance;
            }
        }

        /* Making a member of a class private will prevent code in any external class from having access
         * to that data member. The protected access modifier makes a class member useable in any classes
         * that extend the parent (base) class in which the member is declared. The classes below shows an
         * OverdraftAccount that adds an overdraft facility to the BankAccount2 class. */
        class BankAccount2
        {
            /* The _accountBalance member has the protected access modifier. 
             * It isn't recommended that you use the protected access modifier to control access to highly
             * important data members of a class such as a bank balance. Doing this makes it very easy for
             * a maliciou programmer to gain access to the protected member by extending the parent class.
             * Tent to use the protected access modifier to limit access to helper methods that have no 
             * meaningful use outside of the class hierarchy. */
            protected decimal _accountBalance = 0;

            public void PayInFunds(decimal amountToPayIn)
            {
                _accountBalance = _accountBalance + amountToPayIn;
            }

            public virtual bool WithdrawFunds(decimal amountToWithdraw)
            {
                if (amountToWithdraw > _accountBalance)
                    return false;

                _accountBalance = _accountBalance - amountToWithdraw;
                return true;
            }

            public decimal GetBalance()
            {
                return _accountBalance;
            }
        }
        
        class OverdraftAccount : BankAccount2
        {
            decimal overdraftLimit = 100;

            /* The OverdraftAccount contains an override of the WithdrawFunds method that
             * allows the account holder to draw out more than they have in their account, 
             * up to the limit of their overdraft. This works because the _accountBalance
             * member of the BankAccount2 class has the protected access modifier. */
            public override bool WithdrawFunds(decimal amountToWithdraw)
            {
                if (amountToWithdraw > _accountBalance + overdraftLimit)
                    return false;

                _accountBalance = _accountBalance - amountToWithdraw;
                return true;
            }
        }

        /* The code below shows how a BankAccount3 class could contain an Address class that is used to hold
         * address information for account holders. If this Address class is made protected it can only be used
         * in the BankAccount3 class and in classes that extend that class. */
        class BankAccount3
        {
            protected class Address
            {
                public string FirstLine;
                public string Postcode;
            }

            protected decimal accountBalance = 0;
        }

        /* The class OverdraftAccount2 class can contain a variable of type Address because it is a child of 
         * BankAccount3. The Overdraft2 account contains a member called GuarantorAddress that gives the address
         * of the person nominated by the account holder to guarantee the overdraft. */
        class OverdraftAccount2 : BankAccount3
        {
            decimal overdraftLimit = 100;

            Address GuarantorAddress;
        }
                
        /* Consider an IPrintable interface that specifies methods used to print any object. This is a good idea, 
         * because now a printer can be asked to print any item that is referred to by a reference of IPrintable
         * type. In other words, any object that implements the methods in IPrintable can be printed. */
        interface IPrintable
        {
            string GetPrintableText(int pageWidth, int pageHeight);
            string GetTitle();
        }

        interface IDisplay
        {
            string GetTitle();
        }

        /* When a class implements an interface it contains methods with signatures that match the ones specified
         * in the interface. You can use an explicit interface implementation to make the methods implementing an
         * interface only visible when the object is accessed via an interface reference.
         * Whena class implements an interface it must contain an implementation of all methods that are defined
         * in the interface. Sometimes a class may implement multiple interfaces, in which case it must contain 
         * all the methods defined in all the interfaces. This can lead to problems, in that two interfaces might
         * contain a method with the same name. For example the IPrintable and IDisplay interfaces might both 
         * specify a method called GetTitle to get the title of a document. The designers of the interfaces will
         * have added these methods to the interface because a printed page and an on-screen menu might both need
         * to have titles that need to be displayed. This can lead to confusion when an object implements both
         * interfaces; either a document or a menu might end up with the incorrect title. The class below shows
         * how it can contain explicit implementations of both GetTitle methods. */
        class Report : IPrintable, IDisplay
        {
            /* The methods in the IPrintable interface only have meaning when being used by something trying to 
             * print an object. It is not sensible to call the printing methods in any other  context than via the 
             * IPrintable reference. You can achieve this by making the implementation of the printing methods 
             * explicit, thus adding the interface name to the declaration of the method body. */
            string IPrintable.GetPrintableText(int pageWidth, int pageHeight)
            {
                return "Report text to be printed";
            }

            string IPrintable.GetTitle()
            {
                return "Report title to be printed";
            }

            string IDisplay.GetTitle()
            {
                return "Report title to be displayed";
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

        #region CreatingAccessorMethodsExample Method

        private static void CreatingAccessorMethodsExample()
        {
            BankAccount a = new BankAccount();
            Console.WriteLine("Pay in 50");
            a.PayInFunds(50);

            if (a.WithdrawFunds(10))
                Console.WriteLine("Withdraw 10");

            Console.WriteLine("Account balance is: {0}", a.GetBalance());

            /* Making a data member of a class private will stop direct access to that data member.
             * In other words, the following statement will not compile:
             * 
                    a._accountBalance = a._accountBalance + 1000000;
             *
             * The cunning attempt to increase the balance of an account by a million is not permitted
             * by the compiler because the _accountBalance member has been declared as private to the
             * BankAccount class. */
        }

        #endregion

        #region ProtectedAccessExample Method

        private static void ProtectedAccessExample()
        {
            OverdraftAccount a = new OverdraftAccount();
            Console.WriteLine("Pay in 50");
            a.PayInFunds(50);

            if (a.WithdrawFunds(60))
                Console.WriteLine("Withdraw 60");

            Console.WriteLine("Account balance is: {0}", a.GetBalance());
        }

        #endregion

        #region PrintingInterfaceExample Method

        private static void PrintingInterfaceExample()
        {
            Report myReport = new Report();

            /* Once the methods have been made explicit implementations of the interface, the only way
             * to access these methods in a Report instance is by a reference of a IPrintable type. */
            IPrintable printItem = myReport;
            Console.WriteLine(printItem.GetTitle());
            Console.WriteLine(printItem.GetPrintableText(80, 23));

            IDisplay displayItem = myReport;
            Console.WriteLine(displayItem.GetTitle());
        }

        #endregion
    }
}
