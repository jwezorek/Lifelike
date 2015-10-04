using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public abstract class DiscreteDistribution<T,W>
    {
        private List<Tuple<T,W>> _accumulatedWeights;
        private List<W> _weights;
        private W _totalWeight;
        private Random _rnd;
        
        public DiscreteDistribution()
        {
            _rnd = new Random();
            _accumulatedWeights = new List<Tuple<T, W>>();
            _weights = new List<W>();
            _totalWeight = Zero;
        }

        public DiscreteDistribution(IEnumerable<Tuple<T, W>> weightedItems, Random rnd = null)
        {
            W accumulator = Zero;
            _accumulatedWeights = weightedItems.Select(
                (Tuple<T, W> tup) =>
                {
                    W accumulatedValue = accumulator;
                    accumulator = Add(accumulator, tup.Item2);
                    return new Tuple<T, W>(tup.Item1, accumulatedValue);
                }
            ).ToList();
            _weights = weightedItems.Select(tup => tup.Item2).ToList();
            _totalWeight = accumulator;
            _rnd = (rnd != null) ? rnd : new Random();
        }

        public List<W> Weights
        {
            get
            {
                return _weights;
            }
        }

        public void Insert(T item, W weight)
        {
            _accumulatedWeights.Add( new Tuple<T,W>(item, _totalWeight) );
            _totalWeight = Add(_totalWeight, weight);
        }

        public int Count
        {
            get
            {
                return _accumulatedWeights.Count();
            }
        }

        public T Next()
        {
            if (_accumulatedWeights == null || _accumulatedWeights.Count() == 0)
                throw new ArgumentNullException();
            return BinarySearch(RandomNumber(_rnd, _totalWeight), 0, _accumulatedWeights.Count() - 1);
        }

        private T BinarySearch( W weight, int begin, int end)
        {
            if (end < begin)
                return _accumulatedWeights[end].Item1;
            else
            {
                int mid = (begin + end) / 2;
                if (IsGreaterThan(_accumulatedWeights[mid].Item2, weight))
                    return BinarySearch(weight, begin, mid - 1);
                else if (IsLessThan(_accumulatedWeights[mid].Item2, weight))
                    return BinarySearch(weight, mid + 1, end);
                else
                    return _accumulatedWeights[mid].Item1;
            }
        }

        protected abstract W RandomNumber(Random rnd, W total);
        protected abstract W Zero { get; }
        protected abstract W Add(W n1, W n2);
        protected abstract bool IsLessThan(W w1, W w2);
        protected abstract bool IsGreaterThan(W w1, W w2);
    }

    public class DiscreteWeightedDistribution<T> : DiscreteDistribution<T,int>
    {
        public DiscreteWeightedDistribution() : base()
        {
        }

        public DiscreteWeightedDistribution(IEnumerable<Tuple<T,int>> items, Random rnd = null) : 
            base(
                items.Where( tup => tup.Item2 > 0 ),
                rnd
            )
        {
        }

        protected override int RandomNumber(Random rnd, int total)
        {
            return rnd.Next(total);
        }

        protected override int Zero { get { return 0; } }
        protected override int Add(int n1, int n2) { return n1 + n2; }
        protected override bool IsLessThan(int w1, int w2) { return w1 < w2; }
        protected override bool IsGreaterThan(int w1, int w2) { return w1 > w2; }
    }

    public class DiscreteProbabilityDistribution<T> : DiscreteDistribution<T,double>
    {
        public DiscreteProbabilityDistribution(IEnumerable<Tuple<T, double>> items, Random rnd = null) :
            base(
                items.Where( tup => tup.Item2 > 0.0 ),
                rnd
            )
        {
        }

        protected override double RandomNumber(Random rnd, double total)
        {
            return rnd.NextDouble() * total;
        }

        protected override double Zero { get { return 0; } }
        protected override double Add(double n1, double n2) { return n1 + n2; }
        protected override bool IsLessThan(double w1, double w2) { return w1 < w2; }
        protected override bool IsGreaterThan(double w1, double w2) { return w1 > w2; }
    }

}
