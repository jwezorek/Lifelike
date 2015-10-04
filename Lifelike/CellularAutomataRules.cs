using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public class CellularAutomataRules
    {
        private int[,] _stateTable;

        public CellularAutomataRules(int range, int states)
        {
            _stateTable = new int[range, states];
        }

        public CellularAutomataRules( int[,] stateTable )
        {
            _stateTable = stateTable;
        }

        public CellularAutomataRules(CellularAutomataRules ca)
        {
            _stateTable = (int[,]) ca._stateTable.Clone();
        }

        public int NumStates
        {
            get
            {
               return _stateTable.GetLength(1);
            }
        }

        public int Range
        {
            get
            {
                return _stateTable.GetLength(0);
            }
        }

        public int[,] StateTable
        {
            get
            {
                return (int[,]) _stateTable.Clone();
            }
        }

        public CellularAutomataRules(int range, int states, DiscreteProbabilityDistribution<int> stateDistribution)
        {   
            _stateTable = Util.GetRandomInt2dArray(range, states, stateDistribution);
        }

        public int GetSuccessorState(int currentState, int aliveNeighbors)
        {
            return _stateTable[aliveNeighbors, currentState];
        }

        public int this[int i, int j]
        {
            get { return _stateTable[i, j]; }
            set { _stateTable[i, j] = value; }
        }

        public bool HasSameDimensions(CellularAutomataRules r)
        {
            return NumStates == r.NumStates && Range == r.Range;
        }

        public int Fitness
        {
            get;
            set;
        }
        /*
        public CellularAutomataRules Mutate(double temperature)
        {
            var mutant = new CellularAutomataRules(this);
            int n = (int)(Range * NumStates * temperature);
            for (int i = 0; i < n; i++)
            {
                int row = Util.RndUniformInt(NumStates);
                int col = Util.RndUniformInt(Range);

                mutant[col, row] = Util.RndUniformInt(NumStates);
            }
            return mutant;
        }
        */
    }
}
