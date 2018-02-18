using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NenrDZ1.Domains;
using NenrDZ1.Fuzzy;

namespace NenrDZ2
{
    public static class Relations
    {
        public static double TOLERANCE = 0.0001;

        public static bool IsUTimesURelation(IFuzzySet relation)
        {
            IDomain domain = relation.GetDomain();
            if (domain.GetNumberOfComponents() != 2) return false;

            IDomain a = domain[0];
            IDomain b = domain[1];

            return a.All(element => b.HasElement(element));
        }

        public static bool IsSymmetric(IFuzzySet relation)
        {
            if (!IsUTimesURelation(relation)) return false;

            IDomain domain = relation.GetDomain();

            foreach (var domainElement in domain)
            {
                int a = domainElement[0];
                int b = domainElement[1];

                var value1 = relation.GetValueAt(domainElement);
                var value2 = relation.GetValueAt(DomainElement.Of(b, a));
                if (Math.Abs(value1 - value2) > TOLERANCE)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsReflexive(IFuzzySet relation)
        {
            if (!IsUTimesURelation(relation)) return false;

            IDomain domain = relation.GetDomain()[0];

            foreach (var element in domain)
            {
                int value = element[0];
                if (Math.Abs(1 - relation.GetValueAt(DomainElement.Of(value, value))) > TOLERANCE)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsMaxMinTransitive(IFuzzySet relation)
        {
            if (!IsUTimesURelation(relation)) return false;

            IDomain domain = relation.GetDomain()[0];

            foreach (var x in domain)
            {
                foreach (var z in domain)
                {
                    var value = 0.0;

                    foreach (var y in domain)
                    {
                        var xy = relation.GetValueAt(DomainElement.Of(x[0], y[0]));
                        var yz = relation.GetValueAt(DomainElement.Of(y[0], z[0]));

                        value = Math.Max(value, Math.Min(xy, yz));
                    }
                    if (value > relation.GetValueAt(DomainElement.Of(x[0], z[0])))
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public static IFuzzySet CompositionOfBinaryRelations(IFuzzySet r1, IFuzzySet r2)
        {
            IDomain xDomain = r1.GetDomain()[0];
            IDomain yDomain = r1.GetDomain()[1];
            IDomain zDomain = r2.GetDomain()[1];

            var result = new MutableFuzzySet(Domain.Combine(xDomain, zDomain));
            var or = Operations.ZadehOr();
            var and = Operations.ZadehAnd();
            foreach (var x in xDomain)
            {
                foreach (var z in zDomain)
                {
                    var value = 0.0;

                    foreach (var y in yDomain)
                    {
                        var value1 = r1.GetValueAt(DomainElement.Of(x[0], y[0]));
                        var value2 = r2.GetValueAt(DomainElement.Of(y[0], z[0]));

                        value = or(value, and(value1, value2));
                    }

                    result.Set(DomainElement.Of(x[0], z[0]), value);
                }
            }

            return result;
        }

        public static bool IsFuzzyEquivalence(IFuzzySet relation) => 
            IsReflexive(relation) && IsSymmetric(relation) && IsMaxMinTransitive(relation);
    }
}
