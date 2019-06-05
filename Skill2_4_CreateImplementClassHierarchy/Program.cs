using Shared;
using System;
using System.Collections;
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
            BaseMethod = 5,
            BankAccountConstructor = 6,
            ComparingBankAccounts = 7,
            TypedIComparable = 8,
            GetAnEnumerator = 9,
            UsingForeach = 10,
            CreatingAnEnumerableType = 11,
            UsingYield = 12,
            UsingIDisposable = 13
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
                    case Items.BankAccountConstructor:
                        BankAccountConstructorExample();
                        break;
                    case Items.ComparingBankAccounts:
                        ComparingBankAccountsExample();
                        break;
                    case Items.TypedIComparable:
                        TypedIComparableExample();
                        break;
                    case Items.GetAnEnumerator:
                        GetAnEnumeratorExample();
                        break;
                    case Items.UsingForeach:
                        UsingForeachExample();
                        break;
                    case Items.CreatingAnEnumerableType:
                        CreatingAnEnumerableTypeExample();
                        break;
                    case Items.UsingYield:
                        UsingYieldExample();
                        break;
                    case Items.UsingIDisposable:
                        UsingIDisposableExample();
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

            public decimal GetBalance()
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
                     
                    return base.WithdrawFunds(amount);
                }
            }
        }

        public class BabyAccount5: BankAccount2, IAccount
        {
            /* C# allows a program to replace a method in a base class
             * by simply creating a new method in the child class. In 
             * this situation there is no overriding, you have just
             * supplied a new version of the method. In fact, the C#
             * compiler will give you a warning that indicates how you
             * should provide the keyword new to indicate this. */
            public new bool WithdrawFunds(decimal amount)
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
            /* Overriding/replacing is not always desirable. Consider the GetBalance
             * method in the BankAccount class. This is never going to need a replacement.
             * And yet a malicious programmer can write their own and override or replace
             * the one in the parent. */
            public new decimal GetBalance()
            {
                return 1000000000;
                /* This is the banking equivalent of the bottle of beer that is never empty.
                 * No matter how much cash is drawn out of the account, it always has a
                 * balance value of a million pounds. What this means is that you need a way
                 * to mark some methods as not being able to be overridden. C# does this by
                 * giving us a sealed keyword which means "You can't override this method 
                 * any more". */
            }
        }

        /* You can only seal an overriding method and sealing a method does not prevent a
         * child class from replacing a method in a parent. However, you can also mark a 
         * class as sealed. This means that the class cannot be extended, so it cannot be
         * used as the basis for another class. The BabyAccount6 below cannot be the base
         * of any other classes. */
        public sealed class BabyAccount6 : BankAccount2, IAccount
        {

        }

        public class BankAccount3 : IAccount
        {
            private decimal _balance;

            /* A constructor is a method which gets control during the process of object creation.
             * It is used to allow initial values to be set into an object. You can add a 
             * constructor to the BankAccount3 class that allows an initial balance to be set
             * when an account is created.
             * Unfortunately, adding a constructor like this to a base class in a class hierarchy
             * has the effect of breaking all the child classes. The reason for this is that 
             * creating a child class instance involves creating an instance of the base class. */
            public BankAccount3(decimal initialBalance)
            {
                _balance = initialBalance;
            }

            decimal IAccount.GetBalance()
            {
                return _balance;
            }

            void IAccount.PayInFunds(decimal amount)
            {
                _balance = _balance + amount;
            }

            public virtual bool WithdrawFunds(decimal amount)
            {
                if(_balance < amount)
                {
                    return false;
                }
                _balance = _balance - amount;
                return true;
            }
        }

        /* When the program tries to create a BabyAccount7 it must first create a BankAccount3.
         * Creating a BankAccount3 involves the use of its constructor to set the initial 
         * balance of the BankAccount3. The BabyAccount7 class must contain a constructor that
         * calls the constructor in the parent object to set that up. */
        public class BabyAccount7 : BankAccount3, IAccount
        {
            /* The code below shows how this would work. The constructor for the BabyAccount7
             * makes a call of the constructor for the base class and passes the initial
             * balance into that constructor.
             * In previous examples we discussed the use of the keyword this when writing
             * constructors. We saw that the keyword is used to allow a constructor in a class
             * to call other constructors in that class. The base keyword in this context is
             * analogous to the this keyword, except that the constructor is in the base class.
             * Note that in this case, the actual constructor body for the BabyAccount7 does
             * nothing. However, it might be that other information needs to be stored in
             * BabyAccount7 (perhaps the name of a parent or guardian of the account holder).
             * This can be set by the BabyAccount7 constructor in the BabyAccount7 constructor
             * body. */             
            public BabyAccount7(decimal initialBalance) : base(initialBalance)
            {

            }

            public override bool WithdrawFunds(decimal amount)
            {
                if (amount > 10)
                {
                    return false;
                }
                else
                {
                    return base.WithdrawFunds(amount);
                }
            }
        }

        /* At the moment we are using overriding to modify the behavior of an existing parent method.
         * However, it is also possible to use overriding in a slightly different context. You can 
         * use it to force a set of behaviors on items in a class hierarchy. If there are some things
         * that an account must do then we can make these abstract and then force the child classes
         * to provide the implementation.
         * For example, in the context of the bank application you might want to provide a method that
         * creates the text of a warning letter to the customer telling them that their account is
         * overdraw. This will have to be different for each type of account (you don't want to use
         * the same language to a baby account holder as you do for a normal account). This means
         * that at the time you create the bank account system you know that you need this method, but
         * you don't know what it does in every situation.
         * You can provide a virtual "default" method in the BankAccount4 class and then rely on the 
         * programmers overriding this witha  more specific message, but you then have no way of making
         * sure that they really do perform the override. C# provides a way of flagging a method as 
         * abstract. This means that the method body is not provided in this class, but will be provided
         * in a child class.
         * The fact that the BankAccount4 class contains an abstract method means that the class itself
         * is abstract (and must be marked as such). It is not possible to make an instance of an 
         * abstract class. If you think about it this is sensible. An instance of BankAccount4  would
         * not know what to do if the WarningLetterString method was ever called.
         * An abstract class can be tought of as a kind of template. If you want to make an instance 
         * of a class based on an abstract parent you must provide implementations of all the abstract
         * methods given in the parent.
         * Abstract classes are different from interfaces in that they can contain fully implemented
         * methods alongside the abstract ones. This can be useful because it means you don't have
         * to repeatedly implement the same methods in each of the components that implement a
         * particular interface. */         
        public abstract class BankAccount4
        {
            public abstract string WarningLetterString();
        }

        /* The IComparable interface is used by .NET to determine the ordering of objects 
         * when they are sorted. The interface contains a single method, CompareTo, wich
         * compares this object with another. The CompareTo method returns an integer:
         *      < 0 - indicates that this object should be placed before the one it is being
         *      compared with;
         *      = 0 - indicates that this object should be placed at the same position as
         *      the one it is being compared with;
         *      > 0 - means that this object should be placed after the one it is being
         *      compared with.
         * We can allow the bank accounts to be sorted in order of balance by making the
         * BankAccount5 class implement the IComparable interface and adding a CompareTo
         * method to the BankAccount5 class. */
        public class BankAccount5 : IAccount, IComparable
        {
            private decimal _balance;

            /* The CompareTo method is supplied with an object reference, which must be
             * converted into an IAccount reference so that a BankAccount5 instance can
             * be compared with any other object that implements the IAccount reference.
             * The CompareTo method is simplified by using the CompareTo method of the 
             * decimal balance method to create the result. */             
            public int CompareTo(object obj)
            {
                //If we are being compared with a null object we are definitely after it
                if (obj == null)
                    return 1;

                //Convert the object reference into an account reference
                IAccount account = obj as IAccount;

                //as generates null if the conversion fails
                if (account == null)
                    throw new ArgumentException("Object is not an account");

                //use the balance value as the basis of the comparison
                return this._balance.CompareTo(account.GetBalance());
            }

            decimal IAccount.GetBalance()
            {
                return _balance;
            }

            void IAccount.PayInFunds(decimal amount)
            {
                _balance = _balance + amount;
            }

            public virtual bool WithdrawFunds(decimal amount)
            {
                if (_balance < amount)
                    return false;

                _balance = _balance - amount;
                return true;
            }

            public BankAccount5(decimal initialBalance)
            {
                _balance = initialBalance;
            }
        }

        /* The IComparable interface uses a CompareTo method that accepts an object reference as a parameter.
         * This reference should refer to an object of the same type as the object that is doing the comparing,
         * i.e. the CompareTo method in type x should be supplied with a reference to type x as a parameter.
         * In the previous example, the CompareTo method for the BankAccount5 will throw an exception if a
         * program attempts to compare a bank account with something that isn't a bank account. However, this
         * error will be produced when the program runs, not when the program is compiled. The following two
         * statements compile correctly even though they are clearly incorrect because they are trying to 
         * compare a bank account to a string:
         *      BankAccount5 b = new BankAccount5(100);
         *      b.CompareTo("hello");
         *               
         * 
         */
        public interface IAccount2 : IComparable<IAccount2>
        {
            void PayInFunds(decimal amount);
            bool WithdrawFunds(decimal amount);
            decimal GetBalance();
        }

        public class BankAccount6 : IAccount2
        {
            private decimal _balance;

            /* There is another version of the IComparable interface that accepts a type. This can be used to create
             * a CompareTo that only accepts parameters of a specified type. The class below shows how this works.
             * The CompareTo method now accepts a parameter of type IAccount2. There is no need to cast this to a
             * IAccount2 before performing the comparison and does not have to throw an exception if an invalid 
             * type is supplied. 
             * Note that for the use of typed IComparable to be made to work with objects managed by the IAccount2
             * interface we had to change the definition of the IAccount2 interface so that it extends the IComparable
             * interface. This ensures that any objects that implement the IAccount2 interface also implement the
             * IComparable interface. From this we can see that it is possible to create interface hierarchies as well
             * class hierarchies. */
            public int CompareTo(IAccount2 account)
            {
                //if we are being compared with a null object we are definitely after it
                if (account == null) return 1;

                //use the balance value as the basis of the comparison
                return this._balance.CompareTo(account.GetBalance());
            }

            public BankAccount6(decimal initialBalance)
            {
                this._balance = initialBalance;
            }

            decimal IAccount2.GetBalance()
            {
                return _balance;
            }

            void IAccount2.PayInFunds(decimal amount)
            {
                _balance = _balance + amount;
            }

            bool IAccount2.WithdrawFunds(decimal amount)
            {
                if (_balance < amount)
                    return false;

                _balance = _balance - amount;
                return true;
            }
        }

        /* The IEnumerable interface allows you to create objects that can be enumerated within your programs, for
         * example by the foreach loop construction. Collection classes, and results returned by LINQ queries 
         * implement this interface.
         * The class below implements both the IEnumerable interface (meaning it can be enumerated) and the 
         * IEnumerator<int> interface (meaning that it contains a call of GetEnumerator to get an enumerator from it).
         * An EnumeratorThing instance performs an iteration up to a limit that was set when it was created. Note 
         * that the EnumeratorThing class contains the Current property and the MoveNext behavior that was used in
         * previous example when we wrote code that consumed an enumerator. */
        class EnumeratorThing : IEnumerator<int>, IEnumerable
        {
            int count;
            int limit;

            public int Current
            {
                get
                {
                    return count;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return count;
                }
            }            

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (++count == limit)
                    return false;
                else
                    return true;
            }

            public void Reset()
            {
                count = 0;
            }

            public IEnumerator GetEnumerator()
            {
                return this;
            }

            public EnumeratorThing(int limit)
            {
                count = 0;
                this.limit = limit;
            }
        }

        /* We can create enumerators as we did in last example, but this is quite complicated. To make it easier to 
         * create iterators C# includes the yield keyword. The keyword yield is followed by the return keyword and 
         * precedes the value to be returned for the current iteration. The C# compiler generates all the Current 
         * and MoveNext behaviors that make the iteration work, and also records the state of the iterator method
         * so that the iterator method resumes at the statement following the yield statement when the next iteration
         * is requested. The EnumeratorThing2 class provides exactly the same behavior as the EnumeratorThing class, 
         * but it is much simpler. 
         * The yield keyword does two things. It specifies the value to be returned for a given iteration, and it also
         * returns control to the iterating method. */         
        class EnumeratorThing2 : IEnumerable<int>
        {
            private int limit;

            public IEnumerator<int> GetEnumerator()
            {
                for (int i = 1; i < limit; i++)
                    yield return i;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public EnumeratorThing2(int limit)
            {
                this.limit = limit;
            }
        }
        /* You can express an iterator that returns the values 1, 2, 3 as follows. */
        class EnumeratorThing3 : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                /* When the first yield is reached the enumerator returns the value 1. */
                yield return 1;
                /* The next time that the enumerator is called (in other words the next time round the loop) 
                 * the enumerator resumes at the statement following the first yield. This is another yield 
                 * that return 2. */
                yield return 2;
                /* This continues, with the value 3 being returned by the third yield. When the enumerator 
                 * method ends this has the effect of ending the loop. */
                yield return 3;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /* C# provides memory management for applications, but it can't control how programs use other 
         * resources such as file handles, database connections and lock objects. While an object can
         * get control when it is removed by the garbage collector, it is hard to know precisely when
         * this happens. It may not even happen until the program ends.
         * The IDisposable interface provides a way that an object can indicate that it contains an
         * explicit Dispose method that can be used to tidy up an object when an application has
         * finished using it. A disposed object may exist in memory, but any attempts to use it will
         * result in the ObjectDisposedException being thrown.
         * Note that the action of the Dispose method depends entirely on the needs of the application.
         * Note also that the Dispose method is not called automatically when an object is deleted from
         * memory. There are two ways to make sure that Dispose is called correctly: we can call the
         * method ourself in the application, or we can make use of the C# using construction.
         * In the class below we create a class that allocates resources that must be disposed of.
         * The CrucialConnection class implements IDisposable and contains a Dispose method which, in
         * this case, just prints a message. */
        class CrucialConnection : IDisposable
        {
            public void Dispose()
            {
                Console.WriteLine("Dispose called");
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

        #region BankAccountConstructorExample Method

        private static void BankAccountConstructorExample()
        {
            /* You can now set the initial balance of an account when one is created.
             * The following statement creates a new bank account with an initial
             * balance of 100. */
            IAccount a = new BankAccount3(100);
            IAccount b = new BabyAccount7(100);

            Console.WriteLine(a.GetBalance());
            Console.WriteLine(b.GetBalance());
        }

        #endregion

        #region ComparingBankAccountsExample Method

        
        /* Once the BankAccount5 has been made to implement the IComparable interface,
         * we can use the Sort behaviors provided by collection types. The code below
         * creates a list of 20 accounts and sorts them using the Sort method provided
         * by the List collection type. */
        private static void ComparingBankAccountsExample()
        {
            List<IAccount> accounts = new List<IAccount>();
            Random rand = new Random(1);

            //Create 20 accounts with random balances
            for(int i = 0; i < 20; i++)
            {
                IAccount account = new BankAccount5(rand.Next(0, 10000));
                accounts.Add(account);
            }

            //Sorts the accounts
            accounts.Sort();

            //Display the sorted accounts
            foreach (IAccount account in accounts)
                Console.WriteLine(account.GetBalance());
        }

        #endregion

        #region TypedIComparableExample Method

        private static void TypedIComparableExample()
        {
            //Create 20 accounts with random balances
            List<IAccount2> accounts = new List<IAccount2>();
            Random rand = new Random(1);
            for(int i = 0; i< 20; i++)
            {
                IAccount2 account = new BankAccount6(rand.Next(0, 10000));
                accounts.Add(account);
            }

            //Sort the accounts
            accounts.Sort();

            //Display the sorted accounts
            foreach(IAccount2 account in accounts)
            {
                Console.WriteLine(account.GetBalance());
            }
        }

        #endregion

        #region GetAnEnumeratorExample Method

        /* Programs spend a lot of time consuming lists and other collections of items.
         * This is called iterating or enumerating.
         * Any C# object caan implement the IEnumerable interface that allows other programs
         * to get an enumerator from that object. The enumerator object can then be used
         * to enumerate (or iterate) on the object.
         * The string type supports enumeration, and so a program can call the GetEnumerator
         * method on a string instance to get an enumerator. 
         * The program prints out the "Hello World" string one character at a time. The while
         * construction uses the MoveNext and Current members of the enumerator to do this. */ 
        private static void GetAnEnumeratorExample()
        {
            /* The enumerator exposes the method
             * MoveNext, which returns the value true if it was able to move onto another 
             * item in the enumeration. */
          //Get an enumerator that can iterate through a string
          var stringEnumerator = "Hello World".GetEnumerator();

            while (stringEnumerator.MoveNext())
            {
                /* The enumerator also exposes a property called Current, which is a
                 * reference to the currently selected item in the enumerator. */
                Console.Write(stringEnumerator.Current);
                Console.Write(' ');
            }

            Console.WriteLine();
        }

        #endregion

        #region UsingForeachExample Method
        
        /* You can consume all the iterators as showed in the previous example, but C# makes life
         * easier by providing the foreach construction, which automatically gets the enumerator 
         * from the object and the works through it. The next example shows how we would iterate 
         * through the same string using a foreach construction. */
        private static void UsingForeachExample()
        {
            foreach(char ch in "Hello World")
            {
                Console.Write(ch);
                Console.Write(' ');
            }
            Console.WriteLine();
        }

        #endregion

        #region CreatingAnEnumerableTypeExample Method

        private static void CreatingAnEnumerableTypeExample()
        {
            /* You can use an EnumeratorThing instance in a foreach loop */
            EnumeratorThing e = new EnumeratorThing(10);

            foreach(int i in e)
            {
                Console.WriteLine(i);
            }
        }

        #endregion

        #region UsingYieldExample Method

        private static void UsingYieldExample()
        {
            EnumeratorThing2 e = new EnumeratorThing2(10);
            foreach (int i in e)
                Console.WriteLine(i);

            EnumeratorThing3 e3 = new EnumeratorThing3();
            foreach (int i in e3)
                Console.WriteLine(i);
        }

        #endregion

        #region UsingIDisposableExample Method

        private static void UsingIDisposableExample()
        {
            /* The using construction creates an instance of the CrucialConnection class and is then 
             * followed by the block of code that uses this instance. When the program exits the using 
             * block the Dispose method is called on the CrucialConnection instance. When the program 
             * runs it will print the message "Dispose called". 
             * The C# using construction is a good way to ensure that Dispose is called correctly
             * because it incorporates exception handling so that if an exception is thrown by the
             * code using the object the Dispose method is automatically called on the object being
             * used. If you have a number of objects that need to be disposed you can nest using blocks
             * inside each other. */
            using (CrucialConnection c = new CrucialConnection())
            {
                Console.WriteLine("Doing something with the crucial connection");
            }
        }

        #endregion
    }
}
