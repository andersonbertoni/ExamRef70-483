using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill2_4_CreateImplementClassHierarchy
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            IPrintableInterface = 1,
            IAccountInterface = 2,
            BabyAccount = 3
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
                    case Items.IPrintableInterface:
                        IPrintableInterfaceExample();
                        break;
                    case Items.IAccountInterface:
                        IAccountInterfaceExample();
                        break;
                    case Items.BabyAccount:
                        BabyAccountExample();
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

        /* Consider a printing service that will print objects in an application. You can create an interface 
         * containing the definition of the methods that can be used to print a document on paper. The interface
         * below allows a printer to get the text to be printed and the title of a document from an object that
         * wishes to be printed. 
         * Note that the methods in the interface do not ask a document to print itself, rather they ask the
         * document to provide the text strings that the printer will put onto paper. */
        interface IPrintable
        {
            string GetPrintableText(int pageWidth, int pageHeight);
            string GetTitle();
        }        

        /* The Report class shown below implements the IPrintable interface and contains implementations of the
         * GetPrintableText and GetTitle methods. */
        class Report : IPrintable
        {
            public string GetPrintableText(int pageWidth, int pageHeight)
            {
                return "Report text";
            }

            public string GetTitle()
            {
                return "Report title";
            }
        }

        /* A printer object can now be created that will accept and prints reports, along with any objects
         * that implements the IPrintable interface. The ConsolePrinter class below provides a method called
         * PrintItem that will output printable items onto the console. You can create other printers that 
         * print onto paper, web pages, or Adobe PDF documents. 
         * Note that the interface decouples the printer from the object being printed. If you create new 
         * document types that need to be printed, they can be added to the application without modifying the
         * printer class. Conversely, you can add new types of printer and be sure that the new printer type
         * can be used to print any document object. */
        class ConsolePrinter
        {
            public void PrintItem(IPrintable item)
            {
                Console.WriteLine(item.GetTitle());
                Console.WriteLine(item.GetPrintableText(80, 25));
            }
        }

        /* The IAccount interface is comprised of three methods: one to pay money in, another to withdraw it, 
         * and a third to return the balance on the account. From the balance management point of view this is
         * all you need. Note that at the interface level we are not saying how any of these tasks should be 
         * performed, but we are just identifying the tasks.
         * An interface is placed in a source file just like a class, and compiled in the same way. It sets out
         * a number of methods that relate to a particular task or role, which in this case is what a class must
         * do to be considered a bank account. There is a convention in C# programs that the name of an interface
         * starts with the letter I. */
        interface IAccount
        {
            void PayInFunds(decimal amount);
            bool WithdrawFunds(decimal amount);
            decimal GetBalance();
        }

        /* You can now create a class implementing the interface, so that it can be thought of as an account 
         * component, irrespective of what it really is. 
         * The code does not look much different from the previous BankAccount class. One difference is on the
         * top line. It tells the compiler that this class implements the IAccount interface. This means that
         * the class contains implementations of all the methods described in the interface. Note that all the
         * methods has the interface name, so that they are explicitly implemented. This means that these methods
         * are only exposed when the BankAccount object is referred to by a reference of type IAccount */         
        public class BankAccount : IAccount
        {
            private decimal _balance = 0;

            decimal IAccount.GetBalance()
            {
                return _balance;
            }

            void IAccount.PayInFunds(decimal amount)
            {
                _balance = _balance + amount;
            }

            bool IAccount.WithdrawFunds(decimal amount)
            {
                if (_balance < amount)
                    return false;

                _balance = _balance - amount;
                return true;
            }
        }
        
        /* A class hierarchy is used when you have an application that must manipulate items that are part
         * of a particular group. For example, consider the BankAccount that we have been working with. You
         * may need to create another type of account that is exactly the same as an ordinary account but is
         * used by very small children. This type of account, called BabyAccount works in exactly the same 
         * way as a BankAccount, but it doesn't allow withdrawals of more than 10.
         * The good news is that because the BabyAccount is a component that implements the IAccount interface,
         * it will work with any of the classes that work with accounts. When you create an account you just
         * have to ask if a standard account or a baby account is required. The rest of the system can then 
         * pick up this object and use it without caring about what it is.
         * The bad news is that you have duplicated a lot of code. The BabyAccount class contains GetBalance
         * and PayInFunds methods that are copies of the ones in the BankAccount class. */         
        public class BabyAccount : IAccount
        {
            private decimal _balance = 0;

            decimal IAccount.GetBalance()
            {
                return _balance;
            }

            void IAccount.PayInFunds(decimal amount)
            {
                _balance = _balance + amount;
            }

            bool IAccount.WithdrawFunds(decimal amount)
            {
                if(amount > 10)
                {
                    return false;
                }

                if(_balance < amount)
                {
                    return false;
                }

                _balance = _balance - amount;

                return true;
            }
        }        
        
        #endregion

        #region IPrintableInterfaceExample Method

        private static void IPrintableInterfaceExample()
        {
            Report myReport = new Report();
            ConsolePrinter printer = new ConsolePrinter();
            printer.PrintItem(myReport);
        }

        #endregion

        #region IAccountInterfaceExample Method

        /* With interfaces you are moving away from considering classes in terms of what they are, and starting
         * to think about them in terms of what they can do. In the case of the bank, this means that you want
         * to deal with objects in terms of IAccount (the set of account abilities), rather than BankAccount
         * (a particular account class).
         * In C# terms this means that you need to create reference variables that refer to objects in terms 
         * of interfaces they implement, rather than the particular type they are. */
        private static void IAccountInterfaceExample()
        {
            /* The account variable is allowed to refer to objects that implement the IAccount interface. The 
             * compiler will check to make sure that BankAccount does this, and if it does, the compilation
             * is successful.
             * Note that there will never be an instance of IAccount interface. It is simply a way that you can 
             * refer to something wich has that ability (i.e. contains the required methods). */
            IAccount account = new BankAccount();
            account.PayInFunds(50);
            Console.WriteLine("Balance: {0}", account.GetBalance());
        }

        #endregion

        #region BabyAccountExample Method

        private static void BabyAccountExample()
        {
            IAccount b = new BabyAccount();
            b.PayInFunds(50);
            Console.WriteLine("Balance: {0}", b.GetBalance());

            Console.WriteLine("Trying to withdraw 20");
            if (b.WithdrawFunds(20))
                Console.WriteLine("Balance: {0}", b.GetBalance());
            else
                Console.WriteLine("Not allowed");
        }

        #endregion
    }
}
