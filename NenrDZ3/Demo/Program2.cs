﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ3
{
    class Program2
    {
        static void Main(string[] args)
        {
            int[] input = new int[6];
            int akcel, kormilo;

            Defuzzifier def = new COADefuzzifier();

            FuzzySystem fsAkcel = new AkcelFuzzySystem(def);
            FuzzySystem fsKormilo = new KormiloFuzzySystem(def);

            while (true)
            {
                String str = Console.ReadLine();
                if (str[0] == 'K') break;
                String[] p = str.Split(' ');
                input[0] = int.Parse(p[0]);
                input[1] = int.Parse(p[1]);
                input[2] = int.Parse(p[2]);
                input[3] = int.Parse(p[3]);
                input[4] = int.Parse(p[4]);
                input[5] = int.Parse(p[5]);

                // fuzzy magic ...

                akcel = fsAkcel.Solve(input, true);
                kormilo = fsKormilo.Solve(input, true);
                Console.Write(akcel.ToString() + " " + kormilo.ToString() + "\r\n");
                Console.Out.Flush();
            }
        }
    }
}
