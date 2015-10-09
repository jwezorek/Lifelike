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
        private static List<CellStructure> _registry = new List<CellStructure>
        {
            new HexSixCell(),
            new HexTwelveCell(),
            new SquareFourCell(),
            new SquareEightCell()
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

        private string _name;

        public CellStructure(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name;  }
        }

        public abstract Point GetXyCoordinates(Cells cells, int col, int row, int scale);
        public abstract IndexPair GetColRowFromXy(Cells cells, int col, int row, int scale);
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

        public override Point GetXyCoordinates(Cells cells, int col, int row, int scale)
        {
            int halfScale = scale / 2;
            int x = (col * scale + row * halfScale) % (cells.Columns * scale);
            int y = row * scale;
            return new Point(x, y);
        }

        public override IndexPair GetColRowFromXy(Cells cells, int x, int y, int scale)
        {
            int row = y / scale;
            int col = (x - y / 2) / scale;

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

        public override Point GetXyCoordinates(Cells cells, int col, int row, int scale)
        {
            int halfScale = scale / 2;
            int x = (col * scale + row * halfScale) % (cells.Columns * scale);
            int y = row * scale;

            return new Point(x, y);
        }

        public override IndexPair GetColRowFromXy(Cells cells, int x, int y, int scale)
        {
            int row = y / scale;
            int col = (x - y / 2) / scale;

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

        public override Point GetXyCoordinates(Cells cells, int col, int row, int scale)
        {
            return new Point(scale*col, scale*row);
        }

        public override IndexPair GetColRowFromXy(Cells cells, int x, int y, int scale)
        {
            int row = y / scale;
            int col = x / scale;

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

        public override Point GetXyCoordinates(Cells cells, int col, int row, int scale)
        {
            return new Point(scale * col, scale * row);
        }

        public override IndexPair GetColRowFromXy(Cells cells, int x, int y, int scale)
        {
            int row = y / scale;
            int col = y / scale;

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
}
