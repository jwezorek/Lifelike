using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public class Cells
    {
        private int[,] _ary;
        private CellStructure _cellStruct;

        public Cells(CellStructure cellStruct)
        {
            _cellStruct = cellStruct;
            _ary = new int[cellStruct.Columns, cellStruct.Rows];
        }

        public Cells(CellStructure cellStruct, DiscreteProbabilityDistribution<int> stateDistribution)
        {
            _cellStruct = cellStruct;
            _ary = Util.GetRandomInt2dArray(cellStruct.Columns, cellStruct.Rows, stateDistribution);
        }

        public Point GetXyCoordinates(int col, int row)
        {
            return _cellStruct.GetXyCoordinates(this, col, row);
        }

        public IndexPair GetColRowFromXy(int x, int y)
        {
            return _cellStruct.GetColRowFromXy(this, x, y);
        }

        public int Columns
        {
            get
            {
                return _ary.GetLength(0);
            }
        }

        public int Rows
        {
            get
            {
                return _ary.GetLength(1);
            }
        }

        public int this[int i, int j]
        {
            get
            {
                return _ary[i, j];
            }
            set { _ary[i, j] = value; }
        }

        public int this[IndexPair p]
        {
            get { return _ary[p.Column, p.Row]; }
            set { _ary[p.Column, p.Row] = value; }
        }

        public Cells ApplyRules(CellularAutomataRules rules, NeighborhoodFunction func)
        {
            Cells cells = (Cells) this.Clone();
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                    cells[col, row] = rules.GetSuccessorState(this[col, row], func.Map(this.Neighbors(col, row), rules.NumStates));
            return cells;
        }

        public Cells Clone()
        {
            Cells clone = new Cells(_cellStruct);
            clone._ary = (int[,])_ary.Clone();

            return clone;
        }

        public IEnumerable<int> Neighbors(int col, int row)
        {
            return _cellStruct.Neighbors(this, col, row);
        }

        public int WrapRow(int row)
        {
            if (row < 0)
                return Rows + row;
            else if (row >= Rows)
                return row - Rows;
            return row;
        }

        public int WrapColumn(int column)
        {
            if (column < 0)
                return Columns + column;
            else if (column >= Columns)
                return column - Columns;
            return column;
        }
    }
}
