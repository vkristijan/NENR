using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ5
{
    static class Utility
    {
        private static readonly Random Rnd = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            
            while (n > 1)
            {
                int k = Rnd.Next(0, n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
