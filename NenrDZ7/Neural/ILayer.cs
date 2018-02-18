using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ7.Neural
{
    public abstract class ILayer
    {
        protected ILayer _previous;
        public ILayer Previous
        {
            protected get => _previous;
            set
            {
                _previous = value;
                value._next = this;
            }
        }

        protected ILayer _next;
        public ILayer Next
        {
            protected get => _next;
            set
            {
                _next = value;
                value._previous = this;
            }
        }

        public double[] Values { get; protected set; }
        public int Size => Values.Length;
        public abstract void CalculateValues();
        public abstract int WeightCount();
        public abstract void SetInput(double[] input);

        public double[] Weights
        {
            get => getWeights();
            set => setWeights(value);
        }

        protected abstract double[] getWeights();
        protected abstract void setWeights(double[] weights);
    }
}
