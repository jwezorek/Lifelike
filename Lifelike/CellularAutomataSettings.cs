using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);
      
    public class CellularAutomataSettings
    {
        private int _numStates;
        private NeighborhoodFunction _funcMapNeighbors;
        private CellStructure _cellStructure;

        public CellularAutomataSettings(int states)
        {
            _numStates = states;
            _cellStructure = new HexSixCell();
            _funcMapNeighbors = new AliveCellCount();
        }

        public NeighborhoodFunction NeighborhoodFunction
        {
            get
            {
                return _funcMapNeighbors;
            }
            set
            {
                _funcMapNeighbors = value;
            }
        }

        public CellStructure CellStructure
        {
            get
            {
                return _cellStructure;
            }
            set
            {
                _cellStructure = value;
            }
        }

        public int NumColumns
        {
            get
            {
                return _cellStructure.Columns;
            }
        }

        public int NumRows
        {
            get
            {
                return _cellStructure.Rows; 
            }
        }

        public int NumStates
        {
            get { return _numStates;  }
            set { _numStates = value; }
        }

        public Cells GetInitialCells(DiscreteProbabilityDistribution<int> stateDistribution)
        {
            return new Cells(CellStructure, stateDistribution);
        }

        public int NeighborsCount
        {
            get
            {
                return CellStructure.NeighborsCount;
            }
        }

        public CellularAutomataRules GetRandomRules(DiscreteProbabilityDistribution<int> stateTableDistribution)
        {

                return new CellularAutomataRules(
                    NeighborhoodFunction.GetRange(NeighborsCount, NumStates),
                    NumStates, stateTableDistribution);
        }

        private class GridInfo
        {
            public int Neighbors;
            public Func<int, double, Cells> CreateCellsFunc;
            public GridInfo(int n, Func<int, double, Cells> f)
            {
                Neighbors = n;
                CreateCellsFunc = f;
            }
        }
    }
}
