using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill2_1_CreateTypes
{
    public static class MyExtensions
    {
        /* The code below creates an extension method for the String class. The first parameter to the method
         * specifies the type that the extension method should be added to, by using the keyword this followed
         * by the name of the type. The extension method created, counts the number of lines in a string, and 
         * then returns this result as an integer. The method works by splitting the string on the linefeed
         * character and then counting the number of elements in the resulting array. The extension method is
         * declared in a static class (in this case called MyExtensions). 
         * Note that extension methods are never part of the object they are attached to, since they don't 
         * have any access to private members of the method class. An extension method can never be used to 
         * replace an existing method in a class. In other words, if the String type already contained a method
         * called LineCount, the extension method created is not called. Instead, the LineCount method inside
         * String is used. */
        public static int LineCount(this String str)
        {
            return str.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
