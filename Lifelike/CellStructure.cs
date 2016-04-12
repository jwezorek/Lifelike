using Lifelike.CustomControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public abstract class CellStructure
    {
        protected CellPainter _painter;
        private static List<CellStructure> _registry = new List<CellStructure>
        {
            new HexSixCell(),
            new HexTwelveCell(),
            new SquareFourCell(),
            new SquareEightCell(),
            new TriangularThreeCell(),
            new TriangularTwelveCell()
        };

        public static CellStructure Get(int index)
        {
            return _registry[index];
        }

        public static CellStructure Get(string name)
        {
            return _registry.Find(cs => name == cs.Name);
        }

        public static List<string> Names
        {
            get { return _registry.Select(item => item.Name).ToList(); }
        }

        protected string _name;

        public CellStructure(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name;  }
        }

        public virtual Size Dimensions
        {
            get
            {
                return new Size(Columns * 2, Rows * 2);
            }
        }

        public virtual int Rows
        {
            get 
            { 
                return 310; 
            }
        }

        public virtual int Columns
        {
            get
            {
                return 310;
            }
        }

        public virtual CellPainter Painter
        {
            get
            {
                return new SquareCellPainter();
            }
        }

        public abstract Point GetXyCoordinates(Cells cells, int col, int row);
        public abstract IndexPair GetColRowFromXy(Cells cells, int col, int row);
        public abstract IEnumerable<int> Neighbors(Cells cells, int col, int row);

        public abstract int NeighborsCount
        {
            get;
        }
    }

    class HexSixCell : CellStructure
    {
        public HexSixCell() : base( "Hex, 6-cell" )
        { }

        public override Point GetXyCoordinates(Cells cells, int col, int row)
        {

            int x = (col * 2 + row) % (cells.Columns * 2);
            int y = row * 2;
            return new Point(x, y);
        }

        public override IndexPair GetColRowFromXy(Cells cells, int x, int y)
        {
            int row = y / 2;
            int col = (x - y / 2) / 2;

            if (col < 0)
                col += cells.Columns;
            else if (col >= cells.Columns)
                col -= cells.Columns;

            if (row < 0)
                row += cells.Rows;
            else if (row >= cells.Rows)
                row -= cells.Rows;

            return new IndexPair(col, row);
        }

        public override IEnumerable<int> Neighbors(Cells cells, int col, int row)
        {
            yield return cells[col, cells.WrapRow(row - 1)];
            yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row - 1)];
            yield return cells[cells.WrapColumn(col - 1), row];
            yield return cells[cells.WrapColumn(col + 1), row];
            yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row + 1)];
            yield return cells[col, cells.WrapRow(row + 1)];
        }

        public override int NeighborsCount
        {
            get
            {
                return 6;
            }
        }
    }

    class HexTwelveCell : CellStructure
    {
        public HexTwelveCell()
            : base("Hex, 12-cell")
        { }

        public override Point GetXyCoordinates(Cells cells, int col, int row)
        {
            int x = (col * 2 + row ) % (cells.Columns * 2);
            int y = row * 2;

            return new Point(x, y);
        }

        public override IndexPair GetColRowFromXy(Cells cells, int x, int y)
        {
            int row = y / 2;
            int col = (x - y / 2) / 2;

            if (col < 0)
                col += cells.Columns;
            else if (col >= cells.Columns)
                col -= cells.Columns;

            if (row < 0)
                row += cells.Rows;
            else if (row >= cells.Rows)
                row -= cells.Rows;

            return new IndexPair(col, row);
        }

        public override IEnumerable<int> Neighbors(Cells cells, int col, int row)
        {
            yield return cells[col, cells.WrapRow(row - 1)];
            yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row - 1)];
            yield return cells[cells.WrapColumn(col - 1), row];
            yield return cells[cells.WrapColumn(col + 1), row];
            yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row + 1)];
            yield return cells[col, cells.WrapRow(row + 1)];
            yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row - 2)];
            yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row - 1)];
            yield return cells[cells.WrapColumn(col - 2), cells.WrapRow(row + 1)];
            yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row + 2)];
            yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row + 1)];
            yield return cells[cells.WrapColumn(col + 2), cells.WrapRow(row - 1)];
        }

        public override int NeighborsCount
        {
            get
            {
                return 12;
            }
        }
    }

    class SquareFourCell : CellStructure
    {
        public SquareFourCell()
            : base("Square, 4-cell")
        { }

        public override Point GetXyCoordinates(Cells cells, int col, int row)
        {
            return new Point(2*col, 2*row);
        }

        public override IndexPair GetColRowFromXy(Cells cells, int x, int y)
        {
            int row = y / 2;
            int col = x / 2;

            if (col < 0)
                col += cells.Columns;
            else if (col >= cells.Columns)
                col -= cells.Columns;

            if (row < 0)
                row += cells.Rows;
            else if (row >= cells.Rows)
                row -= cells.Rows;

            return new IndexPair(col, row);
        }

        public override IEnumerable<int> Neighbors(Cells cells, int col, int row)
        {
            yield return cells[col, cells.WrapRow(row - 1)];
            yield return cells[cells.WrapColumn(col + 1), row];
            yield return cells[col, cells.WrapRow(row + 1)];
            yield return cells[cells.WrapColumn(col - 1), row];
        }

        public override int NeighborsCount
        {
            get
            {
                return 4;
            }
        }
    }

    class SquareEightCell : CellStructure
    {
        public SquareEightCell()
            : base("Square, 8-cell")
        { }

        public override Point GetXyCoordinates(Cells cells, int col, int row)
        {
            return new Point(2 * col, 2 * row);
        }

        public override IndexPair GetColRowFromXy(Cells cells, int x, int y)
        {
            int row = y / 2;
            int col = y / 2;

            if (col < 0)
                col += cells.Columns;
            else if (col >= cells.Columns)
                col -= cells.Columns;

            if (row < 0)
                row += cells.Rows;
            else if (row >= cells.Rows)
                row -= cells.Rows;

            return new IndexPair(col, row);
        }

        public override IEnumerable<int> Neighbors(Cells cells, int col, int row)
        {
            yield return cells[col, cells.WrapRow(row - 1)];
            yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row - 1)];
            yield return cells[cells.WrapColumn(col + 1), row];
            yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row + 1)];
            yield return cells[col, cells.WrapRow(row + 1)];
            yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row + 1)];
            yield return cells[cells.WrapColumn(col - 1), row];
            yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row - 1)];
        }

        public override int NeighborsCount
        {
            get
            {
                return 8;
            }
        }
    }

    class TriangularThreeCell : CellStructure
    {
        public TriangularThreeCell()
            : base("Triangular, 3-cell")
        { }

        public override Point GetXyCoordinates(Cells cells, int col, int row)
        {
            int x = col * TriangleCellPainter.HALF_WD_CEIL - TriangleCellPainter.HALF_WD;
            int y = row * TriangleCellPainter.CELL_HGT;
            return new Point(x, y);
        }

        public override IndexPair GetColRowFromXy(Cells cells, int x, int y)
        {
            float fX = x;
            float fY = y;
            float fColumn = (fX + (float)TriangleCellPainter.HALF_WD) / (float)TriangleCellPainter.HALF_WD_CEIL;
            float fRow = fY / (float)TriangleCellPainter.CELL_HGT;
            return new IndexPair(
                cells.WrapColumn((int)Math.Round(fColumn)), 
                cells.WrapRow((int)Math.Round(fRow))
            );
        }

        public override IEnumerable<int> Neighbors(Cells cells, int col, int row)
        {
            bool isRightSideUp = (col + row) % 2 == 0;
            if (isRightSideUp)
            {
                yield return cells[cells.WrapColumn(col - 1), row];
                yield return cells[cells.WrapColumn(col + 1), row];
                yield return cells[col, cells.WrapRow(row+1)];
            }
            else
            {
                yield return cells[cells.WrapColumn(col - 1), row];
                yield return cells[cells.WrapColumn(col + 1), row];
                yield return cells[col, cells.WrapRow(row - 1)];
            }
        }

        public override int NeighborsCount
        {
            get
            {
                return 3;
            }
        }

        public override int Rows
        {
            get
            {
                //TODO: generate this programatically
                return 158;
            }
        }

        public override int Columns
        {
            get
            {
                //TODO: generate this programatically
                return 212;
            }
        }

        public override Size Dimensions
        {
            get
            {
                return new Size(Columns * 3, Rows * 4);
            }
        }

        public override CellPainter Painter
        {
            get
            {
                return new TriangleCellPainter();
            }
        }
    }

    class TriangularTwelveCell : TriangularThreeCell
    {
        public TriangularTwelveCell()
        {
            _name = "Triangular, 12-cell";
        }

        public override IEnumerable<int> Neighbors(Cells cells, int col, int row)
        {
            bool isRightSideUp = (col + row) % 2 == 0;
            if (isRightSideUp)
            {
                yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row -1)];
                yield return cells[cells.WrapColumn(col), cells.WrapRow(row -1)];
                yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row - 1)];

                yield return cells[cells.WrapColumn(col - 2), cells.WrapRow(row + 1)];
                yield return cells[cells.WrapColumn(col - 2), row];
                yield return cells[cells.WrapColumn(col - 1), row];

                yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row + 1)];
                yield return cells[cells.WrapColumn(col), cells.WrapRow(row + 1)];
                yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row + 1)];

                yield return cells[cells.WrapColumn(col + 2), cells.WrapRow(row + 1)];
                yield return cells[cells.WrapColumn(col + 2), row];
                yield return cells[cells.WrapColumn(col + 1), row];
            }
            else
            {
                yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row + 1)];
                yield return cells[cells.WrapColumn(col), cells.WrapRow(row + 1)];
                yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row + 1)];

                yield return cells[cells.WrapColumn(col - 2), cells.WrapRow(row - 1)];
                yield return cells[cells.WrapColumn(col - 2), row];
                yield return cells[cells.WrapColumn(col - 1), row];

                yield return cells[cells.WrapColumn(col - 1), cells.WrapRow(row - 1)];
                yield return cells[cells.WrapColumn(col), cells.WrapRow(row - 1)];
                yield return cells[cells.WrapColumn(col + 1), cells.WrapRow(row - 1)];

                yield return cells[cells.WrapColumn(col + 2), cells.WrapRow(row - 1)];
                yield return cells[cells.WrapColumn(col + 2), row];
                yield return cells[cells.WrapColumn(col + 1), row];
            }
        }

        public override int NeighborsCount
        {
            get
            {
                return 12;
            }
        }
    }
}
