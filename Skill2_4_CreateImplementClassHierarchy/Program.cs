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
            BabyAccount = 3,
            OverridenWithdrawFunds = 4,
            BaseMethod = 5
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
                    case Items.OverridenWithdrawFunds:
                        OverridenWithdrawFundsExample();
                        break;
                    case Items.BaseMethod:
                        BaseMethodExample();
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

        /* What you really want to do is pick up all the behaviors in the BankAccount and then just change the
         * one method that needs to behave differently. This can be done in C# using inheritance. 
         * The key thing here is the word BankAccount after the class name. This is the name of the class that
         * BabyAccount2 is extending. This means that everything that BankAccount can do, BabyAccount2 can do.
         * BankAccount is called the base or parent class of BabyAccount2. */         
        public class BabyAccount2 : BankAccount, IAccount
        {

        }

        public class BankAccount2 : IAccount
        {   
            /* The WithdrawFunds method in the BabyAccount3 class makes use of the _balance value that
             * is declared in the parent BankAccount2 class. To make this possible the _balance value
             * has had its access modifier changed from private to protected so that it can be used in
             * classes that extend the BankAccount2 class. 
             * The ability to override a method is very powerful. It means that you can make more general
             * classes (BankAccount) and customize it to make them more specific (BabyAccount). */
            protected decimal _balance = 0;

            decimal IAccount.GetBalance()
            {
                return _balance;
            }

            void IAccount.PayInFunds(decimal amount)
            {
                _balance = _balance + amount;
            }

            /* The C# compiler needs to know if a method is going to be overriden. This is because it
             * must call an overridden method in a slightly different way from a "normal" one. The 
             * WithdrawFunds method in the BankAccount class has been declared as virtual so that the 
             * compiler knows it may be overridden. It might be overridden in classes which are children 
             * of the parent class. 
             * The C# language does not allow the overriding of explicit implementations of interface 
             * methods. This means that you have to sacrifice a slight measure of encapsulation in order
             * to use class hierarchies in this manner. You can see that the WithdrawFunds method is
             * declared as virtual, but it has not been declared as an interface method. 
             * You can make the WithdrawFunds method virtual because you discovered that different 
             * accounts might need to withdraw funds in a different way. Note that the PayInFunds and
             * GetBalance methods have not been made virtual because you will always be using the 
             * versions of these declared in the BankAccount2 class. */
            public virtual bool WithdrawFunds(decimal amount)
            {
                if (_balance < amount)
                    return false;

                _balance = _balance - amount;
                return true;
            }
        }

        /* Overriding replaces a method in a base class with a version that provides the behavior appropriate
         * to a child class. In the case of the BabyAccount3, you want to change the behavior of the one method
         * that you are interested in. You want to replace the WithdrawFunds method with a new one. */
            public class BabyAccount3: BankAccount2, IAccount
        {
            /* The keyword override means "use this version of the method in preference to the one in the base 
             * class". 
             * The WithdrawFunds method in the BabyAccount3 class contains all the code of the method in the 
             * parent class. 
             * We don't like this much, in that it means that the balance value has to be made more exposed
             * than we might like. It is now protected rather than private to just the BankAccount2 class. */
            public override bool WithdrawFunds(decimal amount)
            {
                if (amount > 10)
                    return false;

                if (_balance < amount)
                    return false;

                _balance = _balance - amount;

                return true;
            }
        }

        public class BabyAccount4 : BankAccount2, IAccount
        {            
            public override bool WithdrawFunds(decimal amount)
            {
                if (amount > 10)
                {
                    return false;
                }
                else
                {
                    /* The word base in this context means "a reference to the thing which has been overridden".
                     * I can use this to make the WithdrawFunds method in my BabyAccount4 much simpler. 
                     * This line make a call to the original WithdrawFunds method in the parent class. This
                     * attempts to perform a withdrawal and returns true if the withdrawal works. 
                     * It's important to understand what we're doing here, and why we're doing it:
                     *      - We don't want to have to write the same code twice;
                     *      - We don't want to make the _balance value visible outside the BankAccount class.
                     * The use of the word base to call the overridden method solves both of these problems 
                     * rather beautifully. Because the method call returns a bool result you can just send
                     * whatever it delivers. By making this change you can put the _balance back to private
                     * in the BankAccount because it is not accessed by the WithdrawFunds method.
                     * Note that there are other useful spin-offs here. If I need to fix a bug in the behavior
                     * of the WithdrawFunds method I just fix it once, in the top-level class, and then it 
                     * is fixed for all the classes which call back to it. */
                     */
                    return base.WithdrawFunds(amount);
                }
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

            /* You can write the code below: */
            IAccount b2 = new BabyAccount2();
            b2.PayInFunds(50);
            Console.WriteLine("Balance: {0}", b.GetBalance());

            /* This works because, although BabyAccount2 does not have a PayInFunds method, the base class does.
             * This means that the PayInFunds method from the BankAccount class is used at this point.
             * Instances of the BabyAccount2 class have abilities which they pick up from their base class. In 
             * fact, at the moment, the BabyAccount2 class has no behaviors of its own; it gets everything from
             * its base class. */
        }

        #endregion

        #region OverridenWithdrawFundsExample Method

        private static void OverridenWithdrawFundsExample()
        {
            IAccount b = new BabyAccount3();
            /* When the code runs the call, PayInFunds will use the method in the parent (since that has not 
             * been overridden), ... */
            b.PayInFunds(50);

            /* ... but the call of WithdrawFunds will use the method in BabyAccount3. */
            if (b.WithdrawFunds(10))
                Console.WriteLine("Withdraw succeeded");
            else
                Console.WriteLine("Withdraw failed");

            Console.WriteLine(b.GetBalance());            
        }

        #endregion

        #region BaseMethodExample Method

        private static void BaseMethodExample()
        {
            IAccount b = new BabyAccount4();            
            b.PayInFunds(50);
                        
            if (b.WithdrawFunds(10))
                Console.WriteLine("Withdraw succeeded");
            else
                Console.WriteLine("Withdraw failed");

            Console.WriteLine(b.GetBalance());
        }

        #endregion
    }
}
