using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NenrDZ5.Neural;

using static NenrDZ5.Neural.ActivationFunctions;

namespace NenrDZ5
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DatasetCreator());
        }
    }

    static class Training
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int[] layout = ReadLayout(args[1]);
            var activationFunctions = new ActivationFunction[layout.Length - 1];
            for (int i = 0; i < layout.Length - 1; ++i)
            {
                activationFunctions[i] = Sigmoid();
            }

            FFANN ffann = new FFANN(layout, activationFunctions);
            Console.WriteLine(ffann.WeightCount());

            int n = ffann.WeightCount();
            double[] weights = new double[n];
            Random rnd = new Random();
            for (int i = 0; i < n; ++i)
            {
                weights[i] = rnd.NextDouble();
            }
            ffann.SetWeights(weights);

            Dataset dataset = new PointDataset(layout[0] / 2);
            dataset.Read(args[0]);
            Console.WriteLine(ffann.CalculateError(dataset));

            Backpropagation train = new Backpropagation(ffann, 0.2, dataset);
            train.MaxIteration = 5000;
            train.MaxError = 1e-6;

            int batchSize;
            switch (args[2])
            {
                case "1":
                    batchSize = dataset.Size;
                    break;
                case "2":
                    batchSize = 1;
                    break;
                default:
                    batchSize = 20;
                    break;         
            }
            train.Train(batchSize);

            Application.Run(new Recognition(ffann));
        }

        private static int[] ReadLayout(string str)
        {
            string[] values = str.Trim().Split('x');
            int n = values.Length;
            int[] layout = new int[n];

            for (int i = 0; i < n; ++i)
            {
                layout[i] = int.Parse(values[i].Trim());
            }

            return layout;
        }
    }

    static class TrainingTest
    {
        static void Main(string[] args)
        {
            int[] layout = { 1, 6, 1 };
            ActivationFunction[] activationFunctions = { Sigmoid(), Sigmoid()};
            FFANN ffann = new FFANN(layout, activationFunctions);

            int n = ffann.WeightCount();
            double[] weights = new double[n];
            Random rnd = new Random();
            for (int i = 0; i < n; ++i)
            {
                weights[i] = rnd.NextDouble();
            }
            ffann.SetWeights(weights);

            Dataset dataset = new Dataset();
            dataset.Read("dummyData.txt");
            Console.WriteLine(ffann.CalculateError(dataset));

            Backpropagation train = new Backpropagation(ffann, 0.1, dataset);
            train.MaxIteration = 100000;
            train.MaxError = 1e-6;
            train.Train(1);

            for (int i = 0; i < dataset.Size; ++i)
            {
                Console.WriteLine(ffann.GetOutput(dataset.GetInput(i))[0].ToString("0.0000") + "  " + dataset.GetOutput(i)[0] );
            }
        }
    }
}
