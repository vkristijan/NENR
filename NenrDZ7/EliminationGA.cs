using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ7.Chromosomes;
using NenrDZ7.Crossover;
using NenrDZ7.Evaluation;
using NenrDZ7.Mutation;
using NenrDZ7.Neural;
using NenrDZ7.Selection;

namespace NenrDZ7
{
    class EliminationGA
    {
        private const int MaxIteration = 1_500_000;
        private const int PopulationSize = 15;
        private static readonly int ChromosomeSize;
        private const double StopCondition = 1e-6;

        private static readonly IEvaluator Evaluator;
        private const string DataPath = @"C:\Users\krist\source\repos\NenrDZ1\NenrDZ7\data\zad7-dataset.txt";

        private static readonly ISelection Selection;
        private const int TournamentSize = 3;

        private static readonly ICrossover Crossover;

        private static readonly IMutation Mutation;
        private const double Sigma1 = 0.3;
        private const double P1 = 0.05;
        private const double Sigma2 = 1.0;
        private const double P2 = 0.1;
        private const double P = 0.9;

        private static FFANN Ffann;

        static EliminationGA()
        {
            Ffann = new FFANN(new[] { 2, 8, 3 }, new[] { ActivationFunctions.Sigmoid(), ActivationFunctions.Sigmoid() });
            ChromosomeSize = Ffann.WeightCount();

            Evaluator = new Evaluator(DataPath, Ffann);
            Selection = new TournamentSelection(TournamentSize);

            var discreteRecombination = new DiscreteRecombination();
            var simpleArithmetic = new SimpleArithmeticRecombination();
            var singleArithmetic = new SingleArithmeticRecombination();
            Crossover = new MixedCrossover(discreteRecombination, simpleArithmetic, singleArithmetic);

            var gausMutation = new GausMutation(Sigma1, P1);
            var newGausMutation = new NewGausMutation(Sigma2, P2);
            Mutation = new MixedMutation(gausMutation, newGausMutation, P);
        }

        private static void Main(string[] args)
        {
            var population = InitialPopulation();
            int iteration = 0;

            Chromosome best = FindBest(population);
            while (iteration < MaxIteration && best.Cost > StopCondition)
            {
                iteration++;

                var (a, b) = Selection.Select(population, out var toKill);
                Chromosome c = Crossover.Crossover(a, b);
                Mutation.Mutate(c);

                for (int i = 0; i < ChromosomeSize; ++i)
                {
                    toKill[i] = c[i];
                }
                Evaluator.Evaluate(toKill);

                best = FindBest(population);
                if (iteration % 1000 == 0)
                {
                    Console.WriteLine("Iteration: " + iteration + " - " + best.Cost);
                }
            }

            Console.WriteLine(" ----- ");
            Console.WriteLine(best);
            Console.WriteLine(" ----- ");
            Console.WriteLine("Error: " + Evaluator.FinalError(best));
            Console.ReadKey();
        }

        private static List<Chromosome> InitialPopulation()
        {
            var population = new List<Chromosome>();

            for (int i = 0; i < PopulationSize; ++i)
            {
                Chromosome chromosome = new Chromosome(ChromosomeSize);
                Evaluator.Evaluate(chromosome);
                population.Add(chromosome);
            }

            return population;
        }

        private static Chromosome FindBest(List<Chromosome> population) 
            => population.OrderBy(c => c.Cost)
                         .FirstOrDefault();
    }
}
