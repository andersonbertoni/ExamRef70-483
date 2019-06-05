/* If the TERSE symbol is defined the body of the TerseReport method will be obeyed when the method
 * is called. */
#define TERSE

/* If the VERBOSE symbol is defined the body of the VerboseReport method will be obeyed. */ 
#define VERBOSE

using Shared;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
            ConditionalAttribute = 1,
            TestingForAnAttribute = 2,
            ReadAnAttribute = 3,
            GetTypeMethod = 4,
            InvestingatingAType = 5,
            ReflectionMethodCall = 6,
            FindingComponents = 7,
            LINQComponents = 8,
            CodeDOMObject = 9
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
                    case Items.TestingForAnAttribute:
                        TestingForAnAttributeExample();
                        break;
                    case Items.ReadAnAttribute:
                        ReadAnAttributeExample();
                        break;
                    case Items.GetTypeMethod:
                        GetTypeMethodExample();
                        break;
                    case Items.InvestingatingAType:
                        InvestingatingATypeExample();
                        break;
                    case Items.ReflectionMethodCall:
                        ReflectionMethodCallExample();
                        break;
                    case Items.FindingComponents:
                        FindingComponentsExample();
                        break;
                    case Items.LINQComponents:
                        LINQComponentsExample();
                        break;
                    case Items.CodeDOMObject:
                        CodeDOMObjectExample();
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

        /* We can create our own attribute classes to help manage elements of our application.
         * These classes can serve as markers in the same way as the Serializable attribute
         * specifies that a class can be serialized, or you can store data in attribute instances
         * to give information about the items the attributes area attached to. The data values
         * stored in an attribute instances are set from the class metadata when the attribute is
         * loaded. A program can change them as it runs, but these changes will be lost when the
         * program ends.
         * The class below is an attribute class that can be used to tag items in our program with
         * the name of the programmer that created it. We can add other details to the attribute,
         * for example the name of the tester and the date the code was last updated. */
        public class ProgrammerAttribute : Attribute
        {
            private string programmerValue;

            /* Note that the programmer name is stored as a read-only property of the attribute.
             * We could have made the programmer name a writable property (added a set behavior
             * to the Programmer property), but this is not sensible, because changes to the
             * programmer name are not persisted when the program ends. */
            public string Programmer
            {
                get
                {
                    return programmerValue;
                }
            }

            public ProgrammerAttribute(string programmer)
            {
                this.programmerValue = programmer;
            }
        }

        /* We can add the Programmer attribute to elements in the same way as when adding the
         * Serializable attribute, although in this case the attribute constructor must be 
         * called to set the programmer name. */         
        [ProgrammerAttribute("Anderson")]
        class Person2
        {
            [ProgrammerAttribute("Anderson")]
            public string Name { get; set; }
        }

        /* The ProgrammerAttribute created earlier can be added to any item in the program, including
         * member variables, methods, and properties. It can also be added to any type of object. When
         * we create an attribute class, the proper practice is to add attribute usage information to
         * the declaration of the attribute class, so that the compiler can make sure that the attribute
         * is only used in meaningful situations. Perhaps we don't want to be able to assign Programmer
         * attributes to the methods in a class. We only want to assign a Programmer attribute to the 
         * class itself.
         * This is done by adding an attribute to the declaration of the attribute class. The attribute
         * is called AttributeUsage and this is set with a number of values that can control how the 
         * attribute is used.
         * The AttributeUsage below only allow the Programmer2 attribute to be applied to class 
         * declarations. */
        [AttributeUsage(AttributeTargets.Class)]
        public class Programmer2Attribute : Attribute
        {
            private string programmerValue;

            public string Programmer
            {
                get
                {
                    return programmerValue;
                }

            }
            public Programmer2Attribute(string programmer)
            {
                this.programmerValue = programmer;
            }
        }

        [Programmer2Attribute("Anderson")]
        class Person3
        {
            /* If we try to use the Programmer2Attribute on anything other than a class we will 
             * find that the compiler will generate errors. Note that this means that the compiler
             * is performing reflection on our code as it compiles it, and we will find that
             * Visual Studio does the same thing, in that invalid attempts to add attributes will
             * be flagged as errors in the editor. */
            //This would cause a compilation error as we are only allowed to apply this attribute to classes
            //[Programmer2Attribute("Anderson")]
            public string Name { get; set; }
        }

        /* We can set values of the AttributeUsage class to control whether children of a class 
         * can be given the attribute, specify which elements of our program can have the attribute
         * assigned to them, identify specific types to be given the attribute and specify whether
         * the given attribute can be applied multiple times to the same item. We can also use the
         * or operator (|) to set multiple targets for a given attribute. The attribute class 
         * FieldOrProp below can be applied to properties or fields in a class. */
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
        class FieldOrProp : Attribute
        {

        }

        /****************************************************************************************************
         * CLASSES AND INTERFACES TO USE WITH THE EXAMPLE FindingComponentsExample
         ****************************************************************************************************/
        public interface IAccount
        {
            void PayInFunds(decimal amount);
            bool WithdrawFunds(decimal amount);
            decimal GetBalance();
        }

        public class BankAccount : IAccount
        {
            protected decimal balance = 0;

            public virtual bool WithdrawFunds(decimal amount)
            {
                if (balance < amount)
                {
                    return false;
                }
                balance = balance - amount;
                return true;
            }

            void IAccount.PayInFunds(decimal amount)
            {
                balance = balance + amount;
            }

            decimal IAccount.GetBalance()
            {
                return balance;
            }
        }

        public class BabyAccount : BankAccount, IAccount
        {
            public override bool WithdrawFunds(decimal amount)
            {
                if (amount > 10)
                {
                    return false;
                }

                if (balance < amount)
                {
                    return false;
                }
                balance = balance - amount;
                return true;
            }
        }

        /****************************************************************************************************
         * CLASSES AND INTERFACES TO USE WITH THE EXAMPLE FindingComponentsExample
         ****************************************************************************************************/

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

        #region TestingForAnAttributeExample Method
                
        private static void TestingForAnAttributeExample()
        {
            /* A program can check that a given class has a particular attribute class attached to it 
             * by using the IsDefined method, which is a static member of the Attribute class. The 
             * IsDefined method accepts two parameters: the first is the type of the class being tested
             * and the second type of the attribute class that the test is looking for.
             * The example below checks whether the Person class is Serializable. Note that although the
             * attribute is called Serializable when it is used in the source code, the name of the class
             * that implements the attribute has the text Attribute appended to it, so that the attribute
             * class we are looking for is called SerializableAttribute. The convention of adding the
             * "Attribute" to the end of attribute classes is one that should be followed when creating
             * our own attributes. */
            if (Attribute.IsDefined(typeof(Person), typeof(SerializableAttribute)))
                Console.WriteLine("Person can be serialized");
        }

        #endregion

        #region ReadAnAttributeExample Method

        /* We know how to add attributes to objects, so now we need to know how the program can read
         * the attributes back. We saw that the Attribute class provides a static method called
         * IsDefined that be used to determine if a class has a particular attribute defined. The
         * Attribute class also provides a method called GetCustomAttribute to get an attribute from
         * a particular type. It has the same parameters as IsDefined (the type of the class and the 
         * type of the attribute class) and returns a reference to the attribute. If the attribute
         * is not defined on the class, GetCustomAttribute returns null. In the code below the
         * Attribute returned is cast to a ProgrammerAttribute and the name of the programmer is
         * printed. */
        private static void ReadAnAttributeExample()
        {
            Attribute a = Attribute.GetCustomAttribute(typeof(Person2), typeof(ProgrammerAttribute));

            ProgrammerAttribute p = (ProgrammerAttribute)a;

            Console.WriteLine("Programmer: {0}", p.Programmer);
        }

        #endregion

        #region GetTypeMethodExample Method

        private static void GetTypeMethodExample()
        {
            Person3 p = new Person3();

            /* We can start considering reflection by looking at the GetType method. All objects
             * in a C# program expose this method, which will return a reference to the type that
             * defines the object. */
            Type type = p.GetType();

            Console.WriteLine("Person3 type: {0}", type.ToString());
        }

        #endregion

        #region InvestingatingATypeExample Method
        
        /* The type of an object contains all the fields of an object, along with all the metadata
         * describing the object. We can use methods and objects in the System.Reflection namespace
         * to work with Type objects. The code below extract information about all the fields in the
         * Person2 type. It prints all the members of the Person2. 
         * Note that the Name property has been implemented by the compiler as a pair of get and set
         * methods (set_Name and get_Name), and the class contains all the methods that are exposed
         * by an object, including ToString and, of course, the GetType method. */         
        private static void InvestingatingATypeExample()
        {
            Person2 p = new Person2();
            Type type = p.GetType();

            foreach(MemberInfo member in type.GetMembers())
            {
                Console.WriteLine(member.ToString());
            }
        }

        #endregion

        #region ReflectionMethodCallExample Method

        /* We can use the information provided by a type to create a call to a method in that type. */
        private static void ReflectionMethodCallExample()
        {
            Person2 p = new Person2();
            Type t = p.GetType();

            /* The code below will set the name of a person by using the set_Name behavior of the Name
             * property in the Person2 class. It does this by finding the MethodInfo for this method and
             * then calling the Invoke method on this reference. */
            MethodInfo setMethod = t.GetMethod("set_Name");

            /* The Invoke method is supplied with a reference to the Person2 that is the target of the 
             * method invocation and an array of object references which will be used as the arguments
             * to that method call. 
             * This code would, of course, be much slower than just setting the Name property to the value
             * "Anderson", but it illustrates the flexibility provided by reflection. A program can now 
             * obtain a reference to an object, find out what behaviors that object exposes, and then
             * make use of the behaviors that it needs. */
            setMethod.Invoke(p, new object[] { "Anderson" });

            Console.WriteLine(p.Name);
        }

        #endregion

        #region FindingComponentsExample Method

        /* We have seen how a program can dynamically locate members of a class but to implement
         * plugins we need to be able to search the classes in an assembly and find components
         * that implement particular interfaces. This behavior is the basis of the Managed
         * Extensibility Framework (MEF).
         * The code below shows how we cam search an assembly for a particular type of component
         * class. It can be used in a banking application to find all the classes that implement
         * the IAccount interface. It searches the currently executing assembly for types that
         * implement IAccount. The Assembly type provides a method that will get the currently 
         * executing assembly. It is then possible to iterate through all the types in this assembly
         * looking for ones that are not interfaces and implement the IAccount interface. The code
         * uses the IsAssignableFrom method to decide wheter or not a given type implements the
         * IAccount interface. 
         * If we run the program it will print the names of the BabyAccount and BankAccount types, 
         * because these are defined in the same assembly as the program. */         
        private static void FindingComponentsExample()
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            List<Type> accountTypes = new List<Type>();

            foreach(Type t in thisAssembly.GetTypes())
            {
                if (t.IsInterface)
                    continue;

                if(typeof(IAccount).IsAssignableFrom(t))                
                    accountTypes.Add(t);                
            }

            foreach (Type t in accountTypes)
                Console.WriteLine(t.Name);

        }

        #endregion

        #region LINQComponentsExample Method

        /* We can simplify the identification of the types by using a LINQ query as shown below. */
        private static void LINQComponentsExample()
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            var accountTypes = from type in thisAssembly.GetTypes()
                               where typeof(IAccount).IsAssignableFrom(type) && !type.IsInterface
                               select type;

            foreach (Type t in accountTypes)
                Console.WriteLine(t.Name);
        }

        #endregion

        #region CodeDOMObjectExample Method

        /* A Document Object Model (DOM) is a way of representing the structure of a particular type of document.
         * The object contains collections of other objects that represent the contents of the document. There are
         * document object models for XML, JSON and HTML documents, and there is also one that is used to represent
         * the structure of a class. This is called CodeDOM object.
         * A CodeDOM object can be parsed to create a source file or an executable assembly. The constructions that
         * are used in a CodeDOM object represent the logical structure of the code to be implemented and are 
         * independent of the code syntax of the high-level language that is used to create the document. In other
         * words, you can create either Visual Basic.NET or C# source files from a given CodeDOM object and you can
         * create CodeDOM objects using either language. 
         * There are a range of types that can be created and added to a document to allow you to programmatically
         * create enumerated types expressions, method calls, properties and all the elements of a complete program.
         * Note that you would normally create such a document model on the basis of some data structure that you 
         * were parsing. */
        private static void CodeDOMObjectExample()
        {
            /* The outer level CodeCompileUnit instance is created first. This serves as a container for CodeNamespace
             * objects that can be added to it. */
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            /* A CodeNamespace can contain a number of CodeTypeDeclarations... */
            //Create a namespace to hold the types we are going to create
            CodeNamespace personnelNameSpace = new CodeNamespace("Personnel");

            //Import the system namespace
            personnelNameSpace.Imports.Add(new CodeNamespaceImport("System"));

            /* ... and a class is a kind of CodeTypeDeclaration. The class can contain a number of fields. */
            //Create a Person class
            CodeTypeDeclaration personClass = new CodeTypeDeclaration("Person");
            personClass.IsClass = true;
            personClass.TypeAttributes = System.Reflection.TypeAttributes.Public;

            //Add the Person class to personnelNamespace
            personnelNameSpace.Types.Add(personClass);

            /* A single data field is created, but there are also types to represent methods, statements within methods
             * and expressions that the statements can work with. */
            //Create a field to hold the name of a person
            CodeMemberField nameField = new CodeMemberField("String", "name");
            nameField.Attributes = MemberAttributes.Private;

            //Add the name field to the Person class
            personClass.Members.Add(nameField);

            //Add the namespace to the document
            compileUnit.Namespaces.Add(personnelNameSpace);

            /* Once the CodeDOM object has been created we can create a CodeDomProvider to parse the code document and
             * produce the program code from it. The code here shows how this works. It send the program code to a string
             * and the displays the string. */
            //Now we need to send our document somewhere
            //Create a proveider to parse the document
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            //Give the provider somewhere to send the parsed output
            StringWriter s = new StringWriter();

            //Set some options for the parse - we can use the defaults
            CodeGeneratorOptions options = new CodeGeneratorOptions();

            //Generate the C# source from the CodeDOM
            provider.GenerateCodeFromCompileUnit(compileUnit, s, options);

            //Close the output stream
            s.Close();

            //Print the C# output
            Console.WriteLine(s.ToString());
        }

        #endregion
    }
}
