using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public abstract class NeighborhoodFunction
    {
        private static List<NeighborhoodFunction> _registry = new List<NeighborhoodFunction>
        {
            new AliveCellCount(),
            new MajorityRules(),
            new ZeroLowMediumHigh(),
            new SumOfStates(),
            new TwoStateCount(),
            new StateBasedBinary(),
            new StateBasedTrinary(),
            new HighState(),
            new LowNonZeroState()
        };
        
        public static NeighborhoodFunction Get(int index)
        {
            return _registry[index];
        }

        public static NeighborhoodFunction Get(string name)
        {
            return _registry.Find(cs => name == cs.Name);
        }

        public static List<string> FunctionNames
        {
            get { return _registry.Select( item => item.Name).ToList(); }
        }

        private string _name;
        public abstract int Map(IEnumerable<int> neighbors, int states);
        public abstract int GetRange(int neighborsSize, int states);



        public NeighborhoodFunction(string name)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }

    public class AliveCellCount : NeighborhoodFunction
    {
        public AliveCellCount()
            : base("Alive cell count")
        {
        }

        public override int Map(IEnumerable<int> neighbors, int states)
        {
            int sum = 0;
            foreach (int state in neighbors)
                sum += (state > 0) ? 1 : 0;
            return sum;
        }

        public override int GetRange(int neighborsSize, int states)
        {
            return neighborsSize + 1;
        }
    }

    public class MajorityRules : NeighborhoodFunction
    {
        public MajorityRules()
            : base("Majority rules")
        {
        }

        static int[] count = new int[20];

        public override int Map(IEnumerable<int> neighbors, int states)
        {   
             if (states > 20)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < states; i++)
                count[i] = 0;

            foreach (int state in neighbors)
                count[state]++;

            int max = -1;
            int stateWithMax = 0;
            for (int i = 1; i<states; i++)
            {
                if ((count[i] > max) || (count[i] == max && i > stateWithMax))
                {
                    max = count[i];
                    stateWithMax = i;
                }
            }
            if (max <= 0 || stateWithMax == 0)
                return 0;
            return stateWithMax;
        }

        public override int GetRange(int neighborsSize, int states)
        {
            return states;
        }
    }
    
   
    public class ZeroLowMediumHigh : NeighborhoodFunction
    {
        public ZeroLowMediumHigh()
            : base("0/Low/Medium/High, 2-state")
        {
        }


        public override int Map(IEnumerable<int> neighbors, int states)
        {
            int count = 0;
            int ones = 0;
            int twos = 0;
            foreach (int state in neighbors)
            {
                ones += (state == 1) ? 1 : 0;
                twos += (state > 1) ? 1 : 0;
                count++;
            }
            int third = count / 3;

            int onesZeroLowMedHigh = (ones + (third - 1)) / third;
            int twosZeroLowMedHigh = (twos + (third - 1)) / third;

            return TwoStateCount.TwoStateCountFunction(onesZeroLowMedHigh, twosZeroLowMedHigh);
        }

        public override int GetRange(int neighborsSize, int states)
        {
            return TwoStateCount.TwoStateCountRange(4);
        }
    }
    public class SumOfStates : NeighborhoodFunction
    {
        public SumOfStates()
            : base("Sum of states")
        {
        }

        public override int Map(IEnumerable<int> neighbors, int states)
        {
            return neighbors.Sum();
        }

        public override int GetRange(int neighborsSize, int states)
        {
            return neighborsSize * (states-1) + 1;
        }
    }

    public class TwoStateCount : NeighborhoodFunction
    {
        public TwoStateCount()
            : base("2-state count")
        {
        }

        // 0 2 5 9 14
        // 1 4 8 13
        // 3 7 12
        // 6 11
        // 10

        static public int TwoStateCountFunction(int ones, int twos)
        {
            return ((ones + twos) * (ones + twos + 1)) / 2 + twos;
        }

        // for an n-cell neighborhood, the range for the two state count mapping function
        // equals the (n+1)th triangular number... For example, if we had a 3 cell neighborhood
        // then TwoStateCountRange(3) = 10 because there are 10 pairs (x,y) -- (0,0), (0, 1), 
        // (0,2), (0,3), (1, 0), (1, 1), (1, 2), (2,0), (2,1), and (3,0) -- where x+y <= 3 which 
        // can be interpretted like 0 ones and 0 twos, 0 ones and 1 twos, etc. 
        // The 4th triangular number is 10.

        static public int TwoStateCountRange(int neighborhoodSize)
        {
            return ((neighborhoodSize + 1) * (neighborhoodSize + 2)) / 2;
        }

        public override int Map(IEnumerable<int> neighbors, int states)
        {
            int ones = 0;
            int twos = 0;
            foreach (int state in neighbors)
            {
                ones += (state == 1) ? 1 : 0;
                twos += (state > 1) ? 1 : 0;
            }
            return TwoStateCountFunction(ones, twos);
        }

        public override int GetRange(int neighborsSize, int states)
        {
            return TwoStateCountRange(neighborsSize);
        }
    }

    public class StateBasedBinary : NeighborhoodFunction
    {
        public StateBasedBinary()
            : base("State based binary")
        {
        }
        static int[] ary = new int[20];

        public override int Map(IEnumerable<int> neighbors, int states)
        {
            if (states > 20)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < states; i++)
                ary[i] = 0;

            foreach (int neighbor in neighbors)
                ary[neighbor] = 1;

            int powerOfTwo = 1;
            int sum = 0;
            foreach (int state in ary)
            {
                sum += powerOfTwo * state;
                powerOfTwo *= 2;
            }

            return sum-1;
        }

        public override int GetRange(int neighborsSize, int states)
        {
            return (int)Math.Pow(2, states)-1;
        }
    }

    public class StateBasedTrinary : NeighborhoodFunction
    {
        public StateBasedTrinary()
            : base("State based trinary")
        {
        }

        static int[] ary = new int[20];

        public override int Map(IEnumerable<int> neighbors, int states)
        {
            if (states > 20)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < states; i++)
                ary[i] = 0;

            int count = 0;
            foreach (int neighbor in neighbors)
            {
                ary[neighbor] += 1;
                count++;
            }
            for (int i = 0; i < count; i++)
                ary[i] = (ary[i] > 2) ? 2 : ary[i];

            int powerOfThree = 1;
            int sum = 0;
            foreach (int digit in ary)
            {
                sum += powerOfThree * digit;
                powerOfThree *= 3;
            }

            return sum - 1;
        }

        public override int GetRange(int neighborsSize, int states)
        {
            return (int)Math.Pow(3, states) - 1;
        }
    }

    public class HighState : NeighborhoodFunction
    {
        public HighState()
            : base("High state")
        {
        }

        public override int Map(IEnumerable<int> neighbors, int states)
        {
            return neighbors.Max();
        }

        public override int GetRange(int neighborsSize, int states)
        {
            return states;
        }
    }

    public class LowNonZeroState : NeighborhoodFunction
    {
        public LowNonZeroState()
            : base("Low non-zero state")
        {
        }

        public override int Map(IEnumerable<int> neighbors, int states)
        {
            int low = int.MaxValue;
            foreach (int state in neighbors)
                if (state > 0 && state < low)
                    low = state;
            return (low != int.MaxValue) ? low : 0;
        }

        public override int GetRange(int neighborsSize, int states)
        {
            return states;
        }
    }

}
