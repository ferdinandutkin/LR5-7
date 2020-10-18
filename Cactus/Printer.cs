using System;
using System.Reflection;

namespace Cactus
{
    public class Printer
    {


        public static void IAmPrinting(params object[] objs)
        {
            foreach (var obj in objs)
            {
                Console.WriteLine(obj.GetType().Name + ' ' + obj);
            }

        }

        public static void PrintMethods<T>(params T[] objs)
        {


            foreach (var obj in objs)
            {

                Console.WriteLine(obj.GetType().Name + Environment.NewLine);

                Array.ForEach(
                    obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public),
                    m => Console.WriteLine(m.Name));

                Console.WriteLine();
            }

        }
    }
}