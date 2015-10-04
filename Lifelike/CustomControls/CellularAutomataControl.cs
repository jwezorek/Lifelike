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

        private const int CELL_DIM = 2;
        private Color[] _colors = new Color[] {
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
            UpdateBitmap();
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

        private void UpdateBitmap()
        {
            BitmapData data = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                for (int row = 0; row < _cells.Rows; row++)
                    for (int col = 0; col < _cells.Columns; col++)
                    {
                        var pt = _cells.GetXyCoordinates( col, row, CELL_DIM );
                        if (pt.X + CELL_DIM < _cells.Columns*CELL_DIM)
                            PaintCell(ptr, _ptOffset.X + pt.X, _ptOffset.Y + pt.Y, data.Stride, _colors[_cells[col, row]]);
                        else
                        {
                            PaintHalfCell(ptr, _ptOffset.X + pt.X, _ptOffset.Y + pt.Y, data.Stride, _colors[_cells[col, row]]);
                            PaintHalfCell(ptr, _ptOffset.X, _ptOffset.Y + pt.Y, data.Stride, _colors[_cells[col, row]]);
                        }
                    }
            }
            _bmp.UnlockBits(data);
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
