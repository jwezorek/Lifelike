using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike.CustomControls
{
    public abstract class CellPainter
    {
        public abstract void PaintBitmap(Bitmap bmp, Cells cells, Point ptOffset, List<Color> colors);
    }

    public class SquareCellPainter : CellPainter
    {

        public SquareCellPainter()
        {
        }

        public override void PaintBitmap(Bitmap bmp, Cells cells, Point ptOffset, List<Color> colors)
        {
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                for (int row = 0; row < cells.Rows; row++)
                    for (int col = 0; col < cells.Columns; col++)
                    {
                        var pt = cells.GetXyCoordinates(col, row);
                        if (pt.X + 2 < cells.Columns * 2)
                            PaintCell(ptr, ptOffset.X + pt.X, ptOffset.Y + pt.Y, data.Stride, colors[cells[col, row]]);
                        else
                        {
                            PaintHalfCell(ptr, ptOffset.X + pt.X, ptOffset.Y + pt.Y, data.Stride, colors[cells[col, row]]);
                            PaintHalfCell(ptr, ptOffset.X, ptOffset.Y + pt.Y, data.Stride, colors[cells[col, row]]);
                        }
                    }
            }
            bmp.UnlockBits(data);
        }

        private unsafe static void PaintCell(byte* ptr, int x, int y, int stride, Color color)
        {
            int x2 = x + 1;
            int y2 = y + 1;
            ptr[(x * 3) + y * stride] = color.B;
            ptr[(x * 3) + y * stride + 1] = color.G;
            ptr[(x * 3) + y * stride + 2] = color.R;
            ptr[(x2 * 3) + y * stride] = color.B;
            ptr[(x2 * 3) + y * stride + 1] = color.G;
            ptr[(x2 * 3) + y * stride + 2] = color.R;
            ptr[(x * 3) + y2 * stride] = color.B;
            ptr[(x * 3) + y2 * stride + 1] = color.G;
            ptr[(x * 3) + y2 * stride + 2] = color.R;
            ptr[(x2 * 3) + y2 * stride] = color.B;
            ptr[(x2 * 3) + y2 * stride + 1] = color.G;
            ptr[(x2 * 3) + y2 * stride + 2] = color.R;
        }

        private unsafe static void PaintHalfCell(byte* ptr, int x, int y, int stride, Color color)
        {
            int y2 = y + 1;
            ptr[(x * 3) + y * stride] = color.B;
            ptr[(x * 3) + y * stride + 1] = color.G;
            ptr[(x * 3) + y * stride + 2] = color.R;
            ptr[(x * 3) + y2 * stride] = color.B;
            ptr[(x * 3) + y2 * stride + 1] = color.G;
            ptr[(x * 3) + y2 * stride + 2] = color.R;
        }
    }

    public class TriangleCellPainter : CellPainter
    {
        public TriangleCellPainter()
        {
        }

        public const int CELL_WD = 5;
        public const int CELL_HGT = 4;
        public const int HALF_WD = CELL_WD / 2;
        public const int HALF_WD_CEIL = HALF_WD + 1;

        public override void PaintBitmap(Bitmap bmp, Cells cells, Point ptOffset, List<Color> colors)
        {
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                for (int row = 0; row < cells.Rows; row++)
                    for (int col = 0; col < cells.Columns; col++)
                    {
                        var pt = cells.GetXyCoordinates(col, row);
                        PaintTriangle(ptr, ptOffset.X + pt.X, ptOffset.Y + pt.Y, data.Stride, colors[cells[col, row]], (col + row) % 2 == 0);
                    }
            }
            bmp.UnlockBits(data);
        }

        static readonly List<int>[] _scanLines = new List<int>[]
        {
            new List<int>{2},
            new List<int>{1,2,3 },
            new List<int> {1,2,3},
            new List<int> {0,1,2,3,4}
        };

        private unsafe static void PaintRow(byte* ptr, int index, int x, int yOffset, Color color)
        {
            foreach(int s in _scanLines[index])
            {
                int xOffset = ((x + s) * 3);
                ptr[xOffset + yOffset] = color.B;
                ptr[xOffset + yOffset + 1] = color.G;
                ptr[xOffset + yOffset + 2] = color.R;
            }
        }

        private unsafe static void PaintTriangle(byte* ptr, int x, int y, int stride, Color color, bool rigthSideUp)
        {
            int yOffset = stride * y;
            if (rigthSideUp)
            {
                for (int i = 0; i < _scanLines.Length; i++)
                {
                    PaintRow(ptr, i, x, yOffset, color);
                    yOffset += stride;
                }
            }
            else
            {
                for (int i = _scanLines.Length-1; i >= 0; i--)
                {
                    PaintRow(ptr, i, x, yOffset, color);
                    yOffset += stride;
                }
            }
        }
    }
}
