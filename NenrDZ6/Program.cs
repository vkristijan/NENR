using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ6
{
    class Program
    {
        static void Main(string[] args)
        {
            int numRules = Int32.Parse(args[0]);
            List<Sample> samples = Sample.GenerateTrainingSet();
            ANFIS anfis = new ANFIS(numRules);
            anfis.Run(samples, maxIter:10000, eta:0.00025, batchSize:1);

            Console.ReadKey();
        }
    }
}
