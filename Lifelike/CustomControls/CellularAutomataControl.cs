using Lifelike.CustomControls;
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
        private CellularAutomataSettings _settings;
        private Timer _timer;
        private const int NUM_PER_SPRINKLE = 25;
        
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

                IndexPair colRow = _cells.GetColRowFromXy(xx , yy);
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
            _cells = _cells.ApplyRules(_rules, _settings.NeighborhoodFunction );
            _settings.CellStructure.Painter.PaintBitmap(_bmp, _cells, _ptOffset, _colors);
            Invalidate();
        }

        public CellularAutomataSettings CellularAutomataSettings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
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

        private Point CalculateOffset()
        {
            Size szWnd = ClientSize;
            Size szCells = _settings.CellStructure.Dimensions;
            int x = (szWnd.Width - szCells.Width) / 2;
            int y = (szWnd.Height - szCells.Height) / 2;
            return new Point(x, y);
        }

        private static Bitmap CreateOffscreenBitmap(Size sz)
        {
            Bitmap bmp = new Bitmap(sz.Width, sz.Height);
            using (Graphics g = Graphics.FromImage(bmp))
                g.Clear(Color.Black);
            return bmp;
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
            _ptOffset = CalculateOffset();
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

        internal void Resume()
        {
            _timer.Start();
        }
    }
}
