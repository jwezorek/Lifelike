using MB.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lifelike
{
    public partial class DistributionBox : Form
    {
        private Dictionary<ColorSlider, int> _dictSliders;
        private Dictionary<Button, int> _dictButtons;
        private List<ColorSlider> _sliders;
        private ColorSlider _extraSlider;
        private Label _extraSliderLabel;
        private List<Label> _labels;
        private int _currentBalancingSlider = 0;

        private const int MARG = 10;
        private const int BTN_HGT = 35;
        private const int PANEL_HGT = 40;
        private const int LBL_COL_WD = 75;

        public DistributionBox()
        {
            InitializeComponent();
        }

        public static DistributionInfo ShowDistribution(IWin32Window wnd, string title, string label, List<Tuple<string, double>> data, 
            int lblColWd = -1, string extraSlider = null, double extraValue = double.NaN)
        {
            return ShowDistribution(
                wnd,
                title, 
                label,
                data.Select( t => new Tuple<string,double,Color>(t.Item1, t.Item2, Color.Black) ).ToList(),
                false,
                lblColWd,
                extraSlider,
                extraValue
            );
        }

        public static DistributionInfo ShowDistribution(IWin32Window wnd, string title, string label, 
            List<Tuple<string, double, Color>> data, bool editColors, int lblColWd = -1, string extraSlider = null, 
            double extraValue = double.NaN )
        {
            var dlg = new DistributionBox();
            dlg.Init(title, label, data, extraSlider, extraValue, editColors, lblColWd);

            DialogResult dr = dlg.ShowDialog(wnd);
            DistributionInfo result = (dr == DialogResult.OK) ?
                new DistributionInfo(dlg.Values, dlg.Colors, dlg.ExtraValue) :
                null;
            dlg.Close();

            return result;
        }

        private void Init(string title, string label, List<Tuple<string, double, Color>> data, 
            string extraSlider, double extraValue, bool editColors, int labelColWd = -1)
        {
            _dictSliders = new Dictionary<ColorSlider, int>();
            _dictButtons = new Dictionary<Button, int>();
            _sliders = new List<ColorSlider>();
            _labels = new List<Label>();

            this.Text = title;
            lblMain.Text = label;
            int wdPanel = pnlHistogram.ClientSize.Width;
            int index = 0;
            labelColWd = (labelColWd > 0) ? labelColWd: LBL_COL_WD;
            foreach (Tuple<string, double, Color> tup in data)
            {
                var sliderAndLbl = AddSlider(
                    pnlHistogram, wdPanel,
                    labelColWd,
                    tup.Item2,
                    tup.Item1,
                    tup.Item3,
                    editColors
                );
                _dictSliders[sliderAndLbl.Item1] = index;
                if (sliderAndLbl.Item3 != null)
                    _dictButtons[sliderAndLbl.Item3] = index;
                _sliders.Add(sliderAndLbl.Item1);
                _labels.Add(sliderAndLbl.Item2);
                index++;
            }
            if (!string.IsNullOrEmpty(extraSlider))
                AddExtraSlider(extraSlider, extraValue, wdPanel, labelColWd);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }

        private const int PCNT_LABEL_WD = 45;

        private void AddExtraSlider(string label, double value, int parentWidth, int leftColWd)
        {
            var panel = new FlowLayoutPanel();
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.ClientSize = new Size(0, PANEL_HGT);
            panel.Width = parentWidth - SystemInformation.VerticalScrollBarWidth - MARG;

            var lbl = new Label();
            lbl.Size = new Size(leftColWd, PANEL_HGT);
            lbl.Text = label;
            lbl.TextAlign = ContentAlignment.MiddleRight;
            lbl.Parent = panel;

            _extraSlider = new ColorSlider();
            _extraSlider.BarOuterColor = Color.Gray;
            _extraSlider.BarInnerColor = Color.LightGray;
            _extraSlider.ElapsedInnerColor = GetHiliteColor(Color.Black);
            _extraSlider.ElapsedOuterColor = Color.Black;
            _extraSlider.Minimum = 0;
            _extraSlider.Maximum = 100;
            _extraSlider.Value = (int) Math.Round(100.0 * value);
            _extraSlider.ValueChanged += extraSlider_ValueChanged;
            _extraSlider.Size = new Size(panel.Width - leftColWd - PCNT_LABEL_WD - 2 * MARG, 35);
            _extraSlider.Parent = panel;

            _extraSliderLabel = new Label();
            _extraSliderLabel.Size = new Size(PCNT_LABEL_WD, PANEL_HGT);
            _extraSliderLabel.TextAlign = ContentAlignment.MiddleRight;
            _extraSliderLabel.Text = string.Format("{0}%", _extraSlider.Value);
            _extraSliderLabel.Parent = panel;

            panel.Parent = this;
            panel.Location = new Point(10, pnlHistogram.Bounds.Bottom);
        }

        private Tuple<ColorSlider,Label,Button> AddSlider(FlowLayoutPanel pnlHistogram, int parentWidth, int leftColWd,
                            double value, string text, Color color, bool editColors)
        {

            int scrollWd = SystemInformation.VerticalScrollBarWidth;

            var slider = new ColorSlider();
            slider.BarOuterColor = Color.Gray;
            slider.BarInnerColor = Color.LightGray;
            slider.ElapsedInnerColor = GetHiliteColor(color);
            slider.ElapsedOuterColor = color;
            slider.Minimum = 0;
            slider.Maximum = 100;
            slider.Value = (int)Math.Round(100.0 * value);
            slider.ValueChanged += slider_ValueChanged;

            var panel = new FlowLayoutPanel();
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.ClientSize = new Size(0, PANEL_HGT);
            panel.Width = parentWidth - scrollWd - MARG;

            Button btn = null;
            if (!editColors)
            {
                var label = new Label();
                label.Size = new Size(leftColWd, PANEL_HGT);
                label.Text = text;
                label.TextAlign = ContentAlignment.MiddleRight;
                label.Parent = panel;
            }
            else
            {
                btn = new Button();
                btn.Size = new Size(leftColWd, BTN_HGT);
                btn.Text = text;
                btn.TextAlign = ContentAlignment.MiddleRight;
                btn.Parent = panel;
                btn.Click += btn_Click;
            }

            slider.Size = new Size(panel.Width - leftColWd - PCNT_LABEL_WD - 2 * MARG, PANEL_HGT);
            slider.Parent = panel;
            slider.Height = 35;
            panel.Parent = pnlHistogram;

            var lblPcnt = new Label();
            lblPcnt.Size = new Size(PCNT_LABEL_WD, PANEL_HGT);
            lblPcnt.TextAlign = ContentAlignment.MiddleRight;
            lblPcnt.Text = string.Format("{0}%", slider.Value);
            lblPcnt.Parent = panel;

            return new Tuple<ColorSlider, Label, Button>(slider, lblPcnt, btn);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            int index = _dictButtons[(Button)sender];
            Color color = _sliders[index].ElapsedOuterColor;
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = true;
            MyDialog.ShowHelp = true;
            MyDialog.Color = color;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                _sliders[index].ElapsedOuterColor = MyDialog.Color;
                _sliders[index].ElapsedInnerColor = GetHiliteColor(MyDialog.Color);
            }
        }

        static Color GetHiliteColor(Color color)
        {
            int r = ((int)color.R + 255) / 2;
            int g = ((int)color.G + 255) / 2;
            int b = ((int)color.B + 255) / 2;
            return Color.FromArgb(r, g, b);
        }

        private void UpdateSlider(int index, int value)
        {
            _sliders[index].Value = value;
            UpdateSliderLabel(index);
        }

        private void UpdateSliderLabel(int index)
        {
            Label label = _labels[index];
            label.Text = string.Format("{0}%", _sliders[index].Value);
        }

        private void extraSlider_ValueChanged(object sender, EventArgs e)
        {
            _extraSliderLabel.Text = string.Format("{0}%", _extraSlider.Value);
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            ColorSlider slider = (ColorSlider)sender;
            if (slider.Focused)
            {
                int index = _dictSliders[slider];
                UpdateSliderLabel(index);

                int total = CurrentValueTotal;
                if (total != 100)
                    BalanceSliders(index, 100 - total);
            }
        }
        
        private void BalanceSliders(int index, int diff)
        {
         //   while (diff != 0)
                for (int j = 0; j < _sliders.Count(); j++)
                {
                    int i = j;
                    if (i != index && diff != 0)
                    {
                        int amount = (diff < 0) ?
                                -Math.Min(_sliders[i].Value, -diff) :
                                Math.Min(100 - _sliders[i].Value, diff);
                        UpdateSlider(i, _sliders[i].Value + amount);
                        diff -= amount;
                    }
               }
        }

        private int CurrentValueTotal
        {
            get
            {
                return _sliders.Sum(slider => slider.Value);
            }
        }
        
        public class DistributionInfo
        {
            public List<double> Values;
            public List<Color> Colors;
            public double ExtraValue;

            public DistributionInfo(List<double> values, List<Color> colors, double extraValue = double.NaN)
            {
                Values = values;
                Colors = colors;
                ExtraValue = extraValue;
            }
        }

        public List<double> Values 
        { 
            get
            {
                return _sliders.Select( slider => (double) slider.Value / 100.0 ).ToList();
            }
        }

        public List<Color> Colors 
        { 
            get
            {
                return _sliders.Select( slider => slider.ElapsedOuterColor ).ToList();
            }
        }

        public double ExtraValue 
        { 
            get
            {
                return (_extraSlider != null) ? 
                    ((double)_extraSlider.Value) / 100.0 : 
                    double.NaN;
            }
        }
    }
}
