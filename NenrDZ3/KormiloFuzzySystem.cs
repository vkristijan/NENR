using NenrDZ1.Domains;
using NenrDZ1.Fuzzy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ3
{
    class KormiloFuzzySystem : FuzzySystem
    {
        public KormiloFuzzySystem(Defuzzifier defuzzifier)
        {
            this.defuzzifier = defuzzifier;

            IDomain angle = Domain.IntRange(-90, 91);
            IDomain direction = Domain.IntRange(0, 2);
            IDomain distance = Domain.IntRange(0, 1301);
            IDomain velocity = Domain.IntRange(0, 100);

            MutableFuzzySet Id = new MutableFuzzySet(distance);
            MutableFuzzySet Iv = new MutableFuzzySet(velocity);

            foreach (var element in distance)
            {
                Id.Set(element, 1);
            }
            foreach (var element in velocity)
            {
                Iv.Set(element, 1);
            }

            //IFuzzySet CriticalClose = new CalculatedFuzzySet(distance, StandardFuzzySets.LFunction(20, 40));
            IFuzzySet Close = new CalculatedFuzzySet(distance, StandardFuzzySets.LFunction(40, 90));
            IFuzzySet Far = new CalculatedFuzzySet(distance, StandardFuzzySets.GammaFunction(100, 200));
            IFuzzySet VeryFar = new CalculatedFuzzySet(distance, StandardFuzzySets.GammaFunction(300, 400));
            //IFuzzySet NotFar = Operations.UnaryOperation(Far, Operations.ZadehNot());

            IFuzzySet Backward = new MutableFuzzySet(direction).Set(DomainElement.Of(0), 1);
            IFuzzySet Forward = new MutableFuzzySet(direction).Set(DomainElement.Of(1), 1);

            IFuzzySet Left = new MutableFuzzySet(angle).Set(DomainElement.Of(90), 1);
            //IFuzzySet Left = new CalculatedFuzzySet(angle, StandardFuzzySets.LambdaFunction(0, 30, 50));
            //IFuzzySet FullLeft = new CalculatedFuzzySet(angle, StandardFuzzySets.LambdaFunction(55, 70, 80));

            IFuzzySet Neutral = new CalculatedFuzzySet(angle, StandardFuzzySets.LambdaFunction(-10, 0, 10));

            //IFuzzySet Right = new CalculatedFuzzySet(angle, StandardFuzzySets.LambdaFunction(-50, -30, 0));
            IFuzzySet Right = new MutableFuzzySet(angle).Set(DomainElement.Of(-90), 1);
            //IFuzzySet FullRight = new CalculatedFuzzySet(angle, StandardFuzzySets.LambdaFunction(-80, -70, -55));


            // L, D, LK, RK, V, S
            rules = new List<Rule>();
            rules.Add(new Rule(new[] { Id, Close, Id, Id, Iv, Forward }, Left, tNorm, implication));
            rules.Add(new Rule(new[] { Close, Id, Id, Id, Iv, Forward }, Right, tNorm, implication));

            rules.Add(new Rule(new[] { Id, Id, Id, Close, Iv, Forward }, Left, tNorm, implication));
            rules.Add(new Rule(new[] { Id, Id, Close, Id, Iv, Forward }, Right, tNorm, implication));

            rules.Add(new Rule(new[] { VeryFar, Id, Id, Id, Iv, Backward }, Left, tNorm, implication));
            rules.Add(new Rule(new[] { Id, VeryFar, Id, Id, Iv, Backward }, Right, tNorm, implication));


            //rules.Add(new Rule(new[] { Far, Close, Id, Id, Iv, Forward }, Left, tNorm, implication));
            //rules.Add(new Rule(new[] { Id, Close, Id, CriticalClose, Iv, Forward }, FullLeft));
            //rules.Add(new Rule(new[] { Far, Close, Id, Id, Iv, Backward }, Left, tNorm, implication));

            //rules.Add(new Rule(new[] { Far, Far, Id, Id, Iv, Forward }, Neutral));

            //rules.Add(new Rule(new[] { Close, Far, Id, Id, Iv, Forward }, Right, tNorm, implication));
            //rules.Add(new Rule(new[] { Close, Id, CriticalClose, Id, Iv, Forward }, FullRight));
            //rules.Add(new Rule(new[] { Close, Far, Id, Id, Iv, Backward }, Right, tNorm, implication));


            //rules.Add(new Rule(new[] { Far, Id, Close, Close, Iv, Forward }, Left, tNorm, implication));
            //rules.Add(new Rule(new[] { Id, Far, Close, Close, Iv, Forward }, Right, tNorm, implication));
            //rules.Add(new Rule(new[] { VeryFar, VeryFar, Close, Close, Iv, Forward }, Right, tNorm, implication));

            //rules.Add(new Rule(new[] { Far, Id, Close, Close, Iv, Backward }, Right, tNorm, implication));
            ////rules.Add(new Rule(new[] { Id, Far, Close, Close, Iv, Backward }, Left, tNorm, implication));


            //rules.Add(new Rule(new[] { Close, Close, Id, Close, Iv, Forward }, Left, tNorm, implication));
            //rules.Add(new Rule(new[] { Close, Close, Close, Id, Iv, Forward }, Right, tNorm, implication));

            //rules.Add(new Rule(new[] { Id, Id, Id, Id, Iv, Backward }, Right, tNorm, implication));

            //rules.Add(new Rule(new[] { VeryFar, Id, Close, Close, Iv, Forward }, Right, tNorm, implication));
            //rules.Add(new Rule(new[] { Id, VeryFar, Close, Close, Iv, Forward }, Left, tNorm, implication));

        }
    }
}
