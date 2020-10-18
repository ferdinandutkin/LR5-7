using System;
using System.Collections.Generic;
using System.Text;

namespace Cactus
{
    static class RandomDay
    {

        public static DateTime Get(DateTime from, DateTime to) => from.AddDays(new Random().Next((to - from).Days));
      
        static public IEnumerable<DateTime> Generate(DateTime from, DateTime to)
        {
            while (true)
                yield return Get(from, to);
        }

    }
    
}
