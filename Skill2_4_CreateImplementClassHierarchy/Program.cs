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
            IPrintableInterface = 1
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

        class ConsolePrinter
        {
            public void PrintItem(IPrintable item)
            {
                Console.WriteLine(item.GetTitle());
                Console.WriteLine(item.GetPrintableText(80, 25));
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
    }
}
