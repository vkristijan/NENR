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

namespace NenrD74
{
    class GenerationGA
    {
        private const int MaxIteration = 20_000;
        private const int PopulationSize = 20;
        private static readonly int ChromosomeSize;
        private const int Elitism = 3;
        private const double StopCondition = 0.00001;

        private static readonly IEvaluator Evaluator;
        private const string DataPath = @"C:\Users\krist\source\repos\NenrDZ1\NenrDZ7\data\zad7-dataset.txt";

        private static readonly ISelection Selection;
        private const int TournamentSize = 3;

        private static readonly ICrossover Crossover;

        private static readonly IMutation Mutation;
        private const double Sigma1 = 0.2;
        private const double P1 = 0.1;
        private const double Sigma2 = 1.0;
        private const double P2 = 0.05;
        private const double P = 0.95;

        private static FFANN Ffann;
        static GenerationGA()
        {
            Ffann = new FFANN(new []{2, 8,3}, new []{ActivationFunctions.Sigmoid() });
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
                var newPopulation = new List<Chromosome>();
                newPopulation.AddRange(population.OrderBy(c => c.Cost).Take(Elitism));

                while (newPopulation.Count < PopulationSize)
                {
                    var (a, b) = Selection.Select(population);
                    Chromosome c = Crossover.Crossover(a, b);
                    Mutation.Mutate(c);
                    Evaluator.Evaluate(c);
                    newPopulation.Add(c);
                }
                population = newPopulation;

                best = FindBest(population);
                Console.WriteLine("Iteration: " + iteration + " - " + best.Cost);
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
