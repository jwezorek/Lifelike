using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lifelike
{
    public partial class CellularAutomataSettingsControl : UserControl
    {
        private CellularAutomataSettings _settings = null;

        public CellularAutomataSettingsControl()
        {
            InitializeComponent();

            comboMappingFunction.Items.AddRange(NeighborhoodFunction.FunctionNames.ToArray());
            comboMappingFunction.SelectedItem = NeighborhoodFunction.FunctionNames[0];
            comboMappingFunction.SelectedIndexChanged += comboMappingFunction_SelectedIndexChanged;

            comboCellStructure.Items.AddRange(CellStructure.Names.ToArray());
            comboCellStructure.SelectedItem = CellStructure.Names[0];
            comboCellStructure.SelectedIndexChanged += comboCellStructure_SelectedIndexChanged;

            numboxStates.ValueChanged += numboxStates_ValueChanged;
        }

        private void numboxStates_ValueChanged(object sender, EventArgs e)
        {
            ((LifelikeWindow)Parent).SetNumStates((int) numboxStates.Value);
        }

        private void comboCellStructure_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.CellStructure = CellStructure.Get(comboCellStructure.SelectedIndex);
        }

        private void comboMappingFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.NeighborhoodFunction = NeighborhoodFunction.Get(comboMappingFunction.SelectedIndex);
        }

        public CellularAutomataSettings Settings
        {
            set
            {
                _settings = value;
                UpdateUi();
            }
        }

        private void _settings_Changed(object sender, EventArgs e)
        {
            UpdateUi();
        }

        private void UpdateUi()
        {
            if (_settings != null)
            {
                comboCellStructure.SelectedItem = _settings.CellStructure.Name;
                numboxStates.Value = _settings.NumStates;
                comboMappingFunction.SelectedItem = _settings.NeighborhoodFunction.Name;
            }
        }

        public void Set(string nameCellStructure, int numStates, string nameNeighborhoodFunc)
        {
            _settings.NumStates = numStates;
            _settings.CellStructure = CellStructure.Get(nameCellStructure);
            _settings.NeighborhoodFunction = NeighborhoodFunction.Get(nameNeighborhoodFunc);

            UpdateUi();
        }

        public void SetNumStates(int states)
        {
            _settings.NumStates = states;
            UpdateUi();
        }
    }
}
