using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Skill1_5_ImplementExceptionHandling
{
    class Program
    {
        private enum Items
        {
            Sair = -1,
            None = 0,
            TryCatch = 1,
            ExceptionObject = 2,
            ExceptionsTypes = 3,
            FinallyBlock = 4,
            ThrowingException = 5,
            RethrowingException = 6,
            InnerException = 7,
            CustomExceptions = 8,
            ConditionalClauses = 9,
            HandlingInnerExceptions = 10,
            AggregateExceptions = 11
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
                    case Items.TryCatch:
                        TryCatchExample();
                        break;
                    case Items.ExceptionObject:
                        ExceptionObjectExample();
                        break;
                    case Items.ExceptionsTypes:
                        ExceptionsTypesExample();
                        break;
                    case Items.FinallyBlock:
                        FinallyBlockExample();
                        break;
                    case Items.ThrowingException:
                        ThrowingExceptionExample();
                        break;
                    case Items.RethrowingException:
                        RethrowingExceptionExample();
                        break;
                    case Items.InnerException:
                        InnerExceptionExample();
                        break;
                    case Items.CustomExceptions:
                        CustomExceptionsExample();
                        break;
                    case Items.ConditionalClauses:
                        ConditionalClausesExample();
                        break;
                    case Items.HandlingInnerExceptions:
                        HandlingInnerExceptionsExample();
                        break;
                    case Items.AggregateExceptions:
                        AggregateExceptionsExample();
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
        
        #region Class

        /* The name of the exception class should end with "Exception" */
        private class CalcException : Exception
        {
            /* The CalcException class contains an error code value
             * that is set when the exception is constructed */
            public enum CalcErrorCodes
            {
                InvalidNumberText,
                DivideByZero
            }

            public CalcErrorCodes Error { get; set; }

            /* The error code value is set when the exception is constructed */
            public CalcException(string message, CalcErrorCodes error)
                : base(message)
            {
                Error = error;
            }
        }

        #endregion

        #region Try Catch Example Method

        private static void TryCatchExample()
        {
            try
            {
                Console.Write("Enter an integer: ");
                string numberText = Console.ReadLine();

                int result;
                result = int.Parse(numberText);
                Console.WriteLine("You entered: {0}", result);
            }
            catch
            {
                Console.WriteLine("Invalid number entered");
            }
        }

        #endregion

        #region Exception Object Example Method

        private static void ExceptionObjectExample()
        {
            try
            {
                Console.Write("Enter an integer: ");
                string numberText = Console.ReadLine();

                int result;
                result = int.Parse(numberText);

                Console.WriteLine("You entered: {0}", result);
            }
            /* The catch keyword is followed by the type of the exception to be caught and the name
             * to be used to refer to the exception object during the exception handler */
            catch (Exception ex)
            {
                /* The value of ex is set to refer to the exception that is generated by the Parse 
                 * method if it fails. */

                /* The first line of the output gives the error message */
                Console.WriteLine("Message: {0}", ex.Message);
                /* The StackTrace gives the position in the program at which the error occurred */
                Console.WriteLine("StackTrace: {0}", ex.StackTrace);
                /* The HelpLink property can be set to give further information about the exception. */
                Console.WriteLine("HelpLink: {0}", ex.HelpLink);
                /* The TargetSite property gives the name of the method that causes the exception */
                Console.WriteLine("TargetSite: {0}", ex.TargetSite);
                /* The Source property gives the name of the application that caused the error, 
                 * or the name of the assembly if the application name has not been set */
                Console.WriteLine("Source: {0}", ex.Source);
            }
        }

        #endregion

        #region Exception Types Example Method

        private static void ExceptionsTypesExample()
        {
            try
            {
                Console.Write("Enter an integer: ");
                string numberText = Console.ReadLine();
                int result;
                result = int.Parse(numberText);
                Console.WriteLine("You entered: {0}", result);
                int sum = 1 / result;
                Console.WriteLine("Sum is: {0}", sum);
            }
            /* The user did not enter a valid number */
            catch (NotFiniteNumberException nx)
            {
                Console.WriteLine("Invalid number");
            }
            /* The user entered the value 0, which will cause a divide by zero exception */
            /* Note that not all arithmetic errors will throw an exception at this point 
             * in the code; if the same division is performed using the floating point or
             * double precision type, the result will be evaluated as "infinity" (8) */
            catch (DivideByZeroException zx)
            {
                Console.WriteLine("Divide by zero");
            }
            /* There is a third catch element, which will catch any other exceptions that might
             * be thrown by this code.
             * The order of the catch element is important. If the first catch element caught 
             * the Exception type the compiler would produce the error "A previous catch clause 
             * already catches all exceptions of this or of a super type ('Exception')". 
             * You must put the most abstract exception type last in the sequence. */
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception");
            }
        }

        #endregion

        #region Finally Block Example Method

        private static void FinallyBlockExample()
        {
            try
            {
                Console.Write("Enter an integer: ");
                string numberText = Console.ReadLine();
                int result;
                result = int.Parse(numberText);
                Console.WriteLine("You entered: {0}", result);
                int sum = 1 / result;
                Console.WriteLine("Sum is: {0}", sum);
            }            
            catch (NotFiniteNumberException nx)
            {
                Console.WriteLine("Invalid number");
            }
            catch (DivideByZeroException zx)
            {
                Console.WriteLine("Divide by zero");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception");
            }
            /* The finally element identifies code that will be executed irrespective 
             * of whatever happens in the try construction.
             * Note that code in this block is guaranteed to run irrespective of what 
             * happens during the try construction. This includes situations where the
             * code in the construction returns to a calling method or code in the 
             * exception handlers cause other exceptions to be thrown. The finally
             * block is where a program can release any resources that it may be using.
             * The only situation in which a finally block will not be executed are:
             *  - If preceding code (in either the try block or an exception handler)
             *  enters an infinite loop;
             *  - If the programmer uses the Environment.FailFast method in the code
             *  protected by the try construction to explicitly request that any 
             *  finally elements are ignored. */
            finally
            {
                Console.WriteLine("Thanks for using my program.");
            }
        }

        #endregion

        #region Throwing Exception Example Method

        private static void ThrowingExceptionExample()
        {
            try
            {
                /* A program can create and throw its own exceptions by using the throw statement
                 * to throw an exception instance. The Exception object constructor accepts a 
                 * string that is used to deliver a descriptive message to the exception handler.
                 * The example below throws an exception ... */
                throw new Exception("I think you should know that I'm feeling very depressed.");
            }
            /* ... and catches it, displaying the message. */
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Rethrowing Exception Methods
        
        private static int CalledMethod()
        {
            try
            {
                int number = 0;
                Console.Write("Enter a number: ");
                number = int.Parse(Console.ReadLine());

                return 1 / number;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred in CalledMethod: {0}", ex.Message);
                /* An error can be rethrown by using the keyword throw with no parameter.
                 * You might think that when re-throwing an exception, you should give 
                 * the exception object to be re-thrown, as shown here:
                 * 
                 * throw ex;
                 * 
                 * This is bad practice because it will remove the stack trace information
                 * that is part of the original exception and replace it with stack trace 
                 * information that describes the position reached in the exception handler
                 * code. This will make it harder to work out what is going on when the error
                 * occurs, because the location of the error will be reported as being in 
                 * your handler, rather than the point at which the original exception was
                 * generated. */
                throw;
            }
        }
        
        private static void RethrowingExceptionExample()
        {
            try
            {
                int number = CalledMethod();

                Console.WriteLine("The number is {0}", number);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: {0}", ex.Message);
                Console.WriteLine("Stack Trace: {0}", ex.StackTrace);                
            }            
        }

        #endregion

        #region Inner Exception Example Methods
        
        private static int CalledMethod2()
        {
            try
            {
                int number = 0;
                Console.Write("Enter a number: ");
                number = int.Parse(Console.ReadLine());

                return 1 / number;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred in CalledMethod: {0}", ex.Message);

                /* The Exception class contains an InnerException property that can be set when 
                 * exception is constructed. The constructor for the newly created exception is
                 * given a reference to the original exception. */
                throw new Exception("Something bad happened", ex);
            }
        }

        private static void InnerExceptionExample()
        {
            try
            {
                int number = CalledMethod2();

                Console.WriteLine("The number is {0}", number);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: {0}", ex.Message);

                if (ex.InnerException != null)
                    Console.WriteLine("Inner Exception Message: {0}", ex.InnerException.Message);
            }
        }

        #endregion

        #region Custom Exceptions Example Methods        

        private static void CustomExceptionsExample()
        {
            try
            {
                throw new CalcException("Calc failed", CalcException.CalcErrorCodes.InvalidNumberText);
            }
            catch (CalcException ce)
            {
                /* The error code can then be used in the exception handler */
                Console.WriteLine("Error: {0}", ce.Error);
            }
        }

        #endregion

        #region Conditional Clauses Example Method
        
        private static void ConditionalClausesExample()
        {
            try
            {
                //throw new CalcException("Calc failed", CalcException.CalcErrorCodes.DivideByZero);

                /* If the error value in the throw statement is changed from DivideByZero to 
                 * InvalidNumberText, the program will fail with an unhandled exception */
                throw new CalcException("Calc failed", CalcException.CalcErrorCodes.InvalidNumberText);
            }
            /* the when keyword is followed by a conditional clause that performs a test on the exception 
             * object. The exception handler will only trigger in the event of an exception being thrown
             * that has an Error property set to DivideByZero. And exception with any other error code is
             * ignored, and in the case of the example program, will cause the program to terminate
             * immediately as an unhandled exception has been thrown. */
            catch (CalcException ce) when (ce.Error == CalcException.CalcErrorCodes.DivideByZero)
            {
                Console.WriteLine("Divide by zero error");
            }
        }

        #endregion

        #region Handling Inner Exceptions Example Method

        /* The program below contains an exception handler that throws
         * a new exception containing an inner exception that describes
         * the error. If the user enters text that cannot be parsed into
         * an integer, an exception is thrown that is caught, and then a 
         * new exception is raised with the error "Calculator Failure".
         * The new exception contains the original exception as an inner
         * exception. */
        private static void HandlingInnerExceptionsExample()
        {
            try
            {
                try
                {
                    Console.Write("Enter an integer: ");
                    string numberText = Console.ReadLine();

                    int result;
                    result = int.Parse(numberText);
                }
                catch (Exception ex)
                {
                    throw new Exception("Calculator failure", ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine(ex.InnerException.StackTrace);
            }
        }

        #endregion

        #region Aggregate Exceptions Example Methods

        private async static Task<string> FetchWebPage(string url)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
                
        private static void AggregateExceptionsExample()
        {
            try
            {
                /* Shows a situation in which aggregate exceptions 
                 * are used to deliver results from a method that
                 * is called to read the text from a web page. */
                Console.Write("Informe uma URL: ");
                string url = Console.ReadLine();

                if (!url.Contains("http://"))
                    url = "http://" + url;

                Task<string> getPage = FetchWebPage(url);
                getPage.Wait();
                Console.WriteLine(getPage.Result);
            }
            catch (AggregateException ag)
            {
                /* The AggregateException is caught and the message
                 * from each exception is displayed */
                foreach (Exception e in ag.InnerExceptions)
                    Console.WriteLine(e.Message);
            }
        }

        #endregion
    }
}
