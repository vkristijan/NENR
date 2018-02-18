using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;
using NenrDZ4.Crossover;
using NenrDZ4.Evaluation;
using NenrDZ4.Mutation;
using NenrDZ4.Selection;

namespace NenrDZ4
{
    class GenerationGA
    {
        private const int MaxIteration = 10_000;
        private const int PopulationSize = 75;
        private const int ChromosomeSize = 5;
        private const int Elitism = 3;
        private const double StopCondition = 0.0001;

        private static readonly IEvaluator Evaluator;
        private const string DataPath = @"C:\Users\krist\source\repos\NenrDZ1\NenrDZ4\data\zad4-dataset1.txt";

        private static readonly ISelection Selection;
        private const int TournamentSize = 3;

        private static readonly ICrossover Crossover;

        private static readonly IMutation Mutation;
        private const double Sigma = 0.05;
        private const double SingleSigma = 0.25;

        static GenerationGA()
        {
            Evaluator = new Evaluator(DataPath);
            Selection = new TournamentSelection(TournamentSize);

            var discreteRecombination = new DiscreteRecombination();
            var simpleArithmetic = new SimpleArithmeticRecombination();
            var singleArithmetic = new SingleArithmeticRecombination();
            Crossover = new MixedCrossover(discreteRecombination, simpleArithmetic, singleArithmetic);

            var gausMutation = new GausMutation(Sigma);
            var singleGausMutation = new SingleGausMutation(SingleSigma);
            Mutation = new MixedMutation(gausMutation, singleGausMutation);
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
