namespace NenrDZ1.Fuzzy
{
    public delegate double IntUnaryFunction(int x);

    public static class StandardFuzzySets
    {

        public static IntUnaryFunction LFunction(int a, int b)
        {
            double IntUnaryFunction(int x)
            {
                if (x < a) return 1;
                if (x < b) return (double)(b - x) / (b - a);
                return 0;
            }

            return IntUnaryFunction;
        }

        public static IntUnaryFunction GammaFunction(int a, int b)
        {
            double IntUnaryFunction(int x)
            {
                if (x < a) return 0;
                if (x < b) return (double)(x - a) / (b - a);
                return 1;
            }

            return IntUnaryFunction;
        }

        public static IntUnaryFunction LambdaFunction(int a, int b, int c)
        {
            double IntUnaryFunction(int x)
            {
                if (x < a) return 0;
                if (x < b) return (double) (x - a) / (b - a);
                if (x < c) return (double) (c - x) / (c - b);
                return 0;
            }

            return IntUnaryFunction;
        }

       
    }
}
