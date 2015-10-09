using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lifelike
{
    public partial class CellularAutomataControl : Control
    {
        private Cells _cells;
        private Point _ptOffset;
        private Bitmap _bmp;
        private CellularAutomataRules _rules;
        private Timer _timer;
        private const int NUM_PER_SPRINKLE = 25;

        private const int CELL_DIM = 2;
        private List<Color> _colors = new List<Color> {
            Color.Black,
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Violet,
            Color.DarkRed,
            Color.DarkOrange,
            Color.DarkGoldenrod,
            Color.DarkGreen,
            Color.DarkBlue,
            Color.DarkViolet,
            Color.White
        };

        public event Action CaRulesChanged;

        public CellularAutomataControl()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);

            _timer = new Timer();
            _timer.Interval = 1;
            _timer.Tick += DoCellularAutomata;
        }

        bool IsDragging
        {
            get
            {
                return this.Capture;
            }
        }

        override protected void OnMouseDown(MouseEventArgs e)
        {
 	         base.OnMouseDown(e);
             this.Capture = true;
        }

        override protected void OnMouseMove(MouseEventArgs e)
        {
 	         base.OnMouseDown(e);
             if (IsDragging)
                SprinkleLife(e.X, e.Y);
        }

        private void SprinkleLife(int x, int y)
        {
            if (_cells == null)
                return;

            x -= _ptOffset.X;
            y -= _ptOffset.Y;

            for (int i = 0; i < NUM_PER_SPRINKLE; i++)
            {
                double angle = 2.0 * Math.PI * Util.RndUniformReal;
                double radius = Util.RndNormalReal( 10.0, 3.0 );
                int xx = (int)(Math.Round(x + radius * Math.Cos(angle)));
                int yy = (int)(Math.Round(y + radius * Math.Sin(angle)));

                IndexPair colRow = _cells.GetColRowFromXy(xx , yy, CELL_DIM);
                _cells[colRow] = Util.RndUniformInt( _rules.NumStates );
            }
        }

        override protected void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Capture = false;
        }

        protected void OnChanged()
        {
            if (CaRulesChanged != null)
                CaRulesChanged();
        }

        public List<Color> Colors
        {
            get
            {
                return _colors.ToList();
            }

            set
            {
                for (int i = 0; i < value.Count(); i++)
                    _colors[i] = value[i];
            }
        }

        private void DoCellularAutomata(object sender, EventArgs e)
        {
            _cells = _cells.ApplyRules(_rules, CellularAutomataSettings.NeighborhoodFunction );
            PaintBitmap(_bmp, _cells, _ptOffset, _colors);
            Invalidate();
        }

        public CellularAutomataSettings CellularAutomataSettings
        {
            get;
            set;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle r = this.ClientRectangle;
            pe.Graphics.DrawImage(_bmp,0,0);
        }

        protected override void OnResize(EventArgs e)
        {
            _bmp = CreateOffscreenBitmap(ClientSize);
            base.OnResize(e);
        }

        public Cells Cells
        {
            get
            {
                return _cells;
            }
        }

        public static void PaintBitmap(Bitmap bmp, Cells cells, Point ptOffset, List<Color> colors)
        {
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                for (int row = 0; row < cells.Rows; row++)
                    for (int col = 0; col < cells.Columns; col++)
                    {
                        var pt = cells.GetXyCoordinates(col, row, CELL_DIM);
                        if (pt.X + CELL_DIM < cells.Columns * CELL_DIM)
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

        private unsafe static void PaintCell( byte* ptr, int x, int y, int stride, Color color)
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

        private static Bitmap CreateOffscreenBitmap(Size sz)
        {
            Bitmap bmp = new Bitmap(sz.Width, sz.Height);
            using (Graphics g = Graphics.FromImage(bmp))
                g.Clear(Color.Black);
            return bmp;
        }

        private void CalculateOffset()
        {
            Size sz = ClientSize;
            int wdCells = _cells.Columns * CELL_DIM;
            int hgtCells = _cells.Rows * CELL_DIM;
            int x = (sz.Width - wdCells) / 2;
            int y = (sz.Height - hgtCells) / 2;
            _ptOffset = new Point(x, y);
        }

        public CellularAutomataRules Rules
        {
            get
            {
                return _rules;
            }

            private set
            {
                _rules = value;
                OnChanged();
            }
        }

        public void Run(Cells cells, CellularAutomataRules rules)
        {
            _cells = cells;
            CalculateOffset();
            Rules = (rules != null) ? rules : _rules;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        internal void Clear()
        {
            Stop();
            Rules = null;
        }
    }
}
