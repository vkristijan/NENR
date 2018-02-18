using NenrDZ1.Domains;
using NenrDZ1.Fuzzy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ3
{
    class AkcelFuzzySystem : FuzzySystem
    {
        public AkcelFuzzySystem(Defuzzifier defuzzifier)
        {
            this.defuzzifier = defuzzifier;

            IDomain acceleration = Domain.IntRange(-5, 6);
            IDomain direction = Domain.IntRange(0, 2);
            IDomain distance = Domain.IntRange(0, 1301);
            IDomain velocity = Domain.IntRange(0, 100);

            MutableFuzzySet Id = new MutableFuzzySet(distance);
            MutableFuzzySet Iv = new MutableFuzzySet(velocity); 

            foreach (var element in distance){
                Id.Set(element, 1);
            }
            foreach (var element in velocity)
            {
                Iv.Set(element, 1);
            }

            IFuzzySet Slow = new CalculatedFuzzySet(velocity, StandardFuzzySets.LFunction(0, 40));
            IFuzzySet Medium = new CalculatedFuzzySet(velocity, StandardFuzzySets.LambdaFunction(37, 45, 55));
            IFuzzySet Fast = new CalculatedFuzzySet(velocity, StandardFuzzySets.GammaFunction(48, 60));

            IFuzzySet CriticalClose = new CalculatedFuzzySet(distance, StandardFuzzySets.LFunction(10, 30));
            IFuzzySet Close = new CalculatedFuzzySet(distance, StandardFuzzySets.LFunction(25, 50));
            IFuzzySet Far = new CalculatedFuzzySet(distance, StandardFuzzySets.GammaFunction(70, 100));

            IFuzzySet Backward = new MutableFuzzySet(direction).Set(DomainElement.Of(0), 1);
            IFuzzySet Forward = new MutableFuzzySet(direction).Set(DomainElement.Of(1), 1);

            IFuzzySet SpeedUp = new MutableFuzzySet(acceleration)
                .Set(DomainElement.Of(5), 1)
                .Set(DomainElement.Of(4), 0.6)
                .Set(DomainElement.Of(3), 0.3);
            IFuzzySet Neutral = new MutableFuzzySet(acceleration).Set(DomainElement.Of(0), 1);
            IFuzzySet SlowDown = new MutableFuzzySet(acceleration)
                .Set(DomainElement.Of(-5), 1)
                .Set(DomainElement.Of(-4), 0.6)
                .Set(DomainElement.Of(-3), 0.3);

            // L, D, LK, RK, V, S
            rules = new List<Rule>();
            rules.Add(new Rule(new[] { Id, Id, CriticalClose, Id, Iv, Forward }, Neutral, tNorm, implication));
            rules.Add(new Rule(new[] { Id, Id, Id, CriticalClose, Iv, Forward }, Neutral, tNorm, implication));
            rules.Add(new Rule(new[] { Id, Id, CriticalClose, Id, Iv, Backward }, Neutral, tNorm, implication));
            rules.Add(new Rule(new[] { Id, Id, Id, CriticalClose, Iv, Backward }, Neutral, tNorm, implication));

            rules.Add(new Rule(new[] { Id, Id, Id, Id, Fast, Forward}, SlowDown, tNorm, implication));
            rules.Add(new Rule(new[] { Id, Id, Id, Id, Medium, Forward}, Neutral, tNorm, implication));
            rules.Add(new Rule(new[] { Id, Id, Id, Id, Slow, Forward}, SpeedUp, tNorm, implication));

        }
    }
}
