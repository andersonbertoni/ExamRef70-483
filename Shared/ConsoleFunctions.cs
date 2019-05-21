using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ConsoleFunctions
    {
        public static int ShowMenuOptions<T>()
        {
            Type genericType = typeof(T);

            int i = -1;

            if (genericType.IsEnum)
            {
                while (i < 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Exam Ref 70-483 - Examples");
                    Console.WriteLine("--------------------------");

                    foreach (var j in Enum.GetValues(typeof(T)))
                    {
                        if (Convert.ToInt32(j) > 0)
                        {
                            Console.WriteLine("{0} - {1};", Convert.ToInt32(Enum.Parse(typeof(T), j.ToString())),
                                                            Enum.GetName(typeof(T), j));
                        }
                    }

                    Console.WriteLine("S - Sair");

                    string valor = Console.ReadLine().ToLower();

                    if (valor == "s")
                    {                        
                        i = 99;
                    }
                    else if (!int.TryParse(valor, out i))
                    {
                        i = -1;
                    }
                }                
            }

            return i;
        }
    }
}
