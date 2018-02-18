using System;

namespace NenrDZ1.Fuzzy
{
    public delegate double UnaryFunction(double x);
    public delegate double BinaryFunction(double x, double y);

    public static class Operations
    {
        public static UnaryFunction ZadehNot() => x => 1 - x;
        public static BinaryFunction ZadehAnd() => Math.Min;
        public static BinaryFunction ZadehOr() => Math.Max;

        public static BinaryFunction HamacherTNorm(double x) 
            => (a, b) => (a * b) / (x + (1 - x) * (a + b - a * b));
        public static BinaryFunction HamacherSNorm(double x) 
            => (a, b) => (a + b - (2 - x) * a * b) / (1 - (1 - x) * a * b);

        public static BinaryFunction Product() => (a, b) => a * b;

        public static IFuzzySet UnaryOperation(IFuzzySet set, UnaryFunction function)
        {
            var result = new MutableFuzzySet(set.GetDomain());

            foreach (var element in set.GetDomain())
            {
                result.Set(element, function(set.GetValueAt(element)));
            }

            return result;
        }

        public static IFuzzySet BinaryOperation(IFuzzySet set1, IFuzzySet set2, BinaryFunction function)
        {
            var result = new MutableFuzzySet(set1.GetDomain());

            foreach (var element in set1.GetDomain())
            {
                var x = set1.GetValueAt(element);
                var y = set2.GetValueAt(element);
                result.Set(element, function(x, y));
            }

            return result;
        }
    }
}
