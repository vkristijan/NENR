using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ6
{
    class ANFIS
    {
        private readonly List<Rule> _rules;

        public ANFIS(int n)
        {
            _rules = new List<Rule>(n);
            for (int i = 0; i < n; ++i)
            {
                _rules.Add(new Rule());
            }
        }

        public void Run(List<Sample> samples, int maxIter, double eta, int batchSize)
        {
            for (int iter = 0; iter <= maxIter; ++iter)
            {
                for (int sx = 0; sx < samples.Count; sx += batchSize)
                {
                    double[][] ruleParam = new double[_rules.Count][];
                    for (int i = 0; i < _rules.Count; ++i)
                    {
                        ruleParam[i] = new double[7];
                    }


                    for (int s = sx; s < sx + batchSize; ++s)
                    {
                        Sample sample = samples[s];

                        for (int i = 0; i < _rules.Count; ++i)
                        {
                            double[] updates = RuleUpdates(sample, _rules[i]);
                            for (int j = 0; j < 7; ++j)
                            {
                                ruleParam[i][j] += updates[j];
                            }
                        }


                    }

                    for (int i = 0; i < _rules.Count; ++i)
                    {
                        for (int j = 0; j < 7; ++j)
                        {
                            _rules[i][j] += eta * ruleParam[i][j];
                        }
                    }
                }
                

                double error = 0;
                foreach (var sample in samples)
                {
                    error += Math.Pow(sample.Z - GetO(sample), 2);
                }
                error /= (2 * samples.Count);
                Console.WriteLine("Iteration: " + iter + "   - " + error);
            }

            Console.WriteLine(" --- ");
            foreach (var rule in _rules)
            {
                Console.WriteLine(rule);
            }
        }

        private double[] RuleUpdates(Sample s, Rule r)
        {
            double[] values = new double[7];

            values[0] = (s.Z - GetO(s));
            values[0] *= (SumAlphaZ(s, r) / Math.Pow(SumAlpha(s), 2));
            values[0] *= r.B(s.Y);
            values[0] *= r[1] * r.A(s.X) * (1 - r.A(s.X));

            values[1] = (s.Z - GetO(s));
            values[1] *= (SumAlphaZ(s, r) / Math.Pow(SumAlpha(s), 2));
            values[1] *= r.B(s.Y);
            values[1] *= -(s.X - r[0]) * r.A(s.X) * (1 - r.A(s.X));

            values[2] = (s.Z - GetO(s));
            values[2] *= (SumAlphaZ(s, r) / Math.Pow(SumAlpha(s), 2));
            values[2] *= r.A(s.X);
            values[2] *= r[3] * r.B(s.Y) * (1 - r.B(s.Y));

            values[3] = (s.Z - GetO(s));
            values[3] *= (SumAlphaZ(s, r) / Math.Pow(SumAlpha(s), 2));
            values[3] *= r.A(s.X);
            values[3] *= -(s.Y - r[2]) * r.B(s.Y) * (1 - r.B(s.Y));

            values[4] = (s.Z - GetO(s));
            values[4] *= r.TNorm(s.X, s.Y) / SumAlpha(s);
            values[4] *= s.X;

            values[5] = (s.Z - GetO(s));
            values[5] *= r.TNorm(s.X, s.Y) / SumAlpha(s);
            values[5] *= s.Y;

            values[6] = (s.Z - GetO(s));
            values[6] *= r.TNorm(s.X, s.Y) / SumAlpha(s);

            return values;
        }

        private double SumAlpha(Sample s) => _rules.Sum(rule => rule.TNorm(s.X, s.Y));

        private double SumAlphaZ(Sample s, Rule r)
        {
            double sum = 0;
            double z = r.Z(s.X, s.Y);

            foreach (var rule in _rules)
            {
                sum += rule.TNorm(s.X, s.Y) * (z - rule.Z(s.X, s.Y));
            }

            return sum;
        }

        private double GetO(Sample s)
        {
            double o = 0;
            double tSum = 0;

            foreach (var rule in _rules)
            {
                double t = rule.TNorm(s.X, s.Y);
                o += t * rule.Z(s.X, s.Y);
                tSum += t;
            }

            return o / tSum;
        }
    }
}
