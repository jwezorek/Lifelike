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
    public partial class LifelikeWindow : Form
    {
        private Button[] _fitnessButtons;
        private GeneticAlgorithmState _genAlg;

        public LifelikeWindow()
        {
            InitializeComponent();
            _fitnessButtons = new Button[] {
                btnSkip,
                //btnSuperBad,
                btnBad,
                btnOkay,
                btnGood,
               // btnSuperGood
            };
            _genAlg = new GeneticAlgorithmState();
            ctrlCellularAutomata.CellularAutomataSettings = _genAlg.CaSettings;
            ctrlCellularAutomataSettings.Settings = _genAlg.CaSettings;
            ctrlGeneticAlgorithmSettings.Settings = _genAlg.GaSettings;

            ctrlCellularAutomata.CaRulesChanged += ctrlCellularAutomata_CaRulesChanged;
            ctrlCellularAutomataSettings.SettingsChanged += ctrlCellularAutomataSettings_SettingsChanged;
        }

        void ctrlCellularAutomataSettings_SettingsChanged()
        {
            ctrlCellularAutomata.Clear();
        }

        private void ctrlCellularAutomata_CaRulesChanged()
        {
            btnRerun.Enabled = (ctrlCellularAutomata.Rules != null);
        }

        private void ctrlCellularAutomata_Click(object sender, EventArgs e)
        {
            
        }

        private void HandleFitnessButtonClick(FitnessJudgement judgement)
        {
            if (_genAlg.IsInProgess)
            {
                _genAlg.ApplyFitnessJudgement(judgement);
                ctrlInProgressPanel.SetGenerationItemCount(_genAlg.NumRulesInGeneration);
                ctrlCellularAutomata.Run(_genAlg.InitialCells, _genAlg.CurrentRules);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!_genAlg.IsInProgess)
                StartGeneticAlgorithm();
            else
                StopGeneticAlgorithm();
        }

        private void StartGeneticAlgorithm()
        {
            ctrlInProgressPanel.SetGenerationItemCount(0);
            ctrlInProgressPanel.SetGenerationNumber(_genAlg.GenerationNumber);
            ctrlInProgressPanel.SetDescription( _genAlg.CaSettings );

            //ctrlGeneticAlgorithmSettings.Visible = false;
            ctrlCellularAutomataSettings.Visible = false;
            ctrlInProgressPanel.Visible = true;
            btnLoad.Visible = false;

            foreach (var btn in _fitnessButtons)
                btn.Enabled = true;

            btnRun.Text = "Stop";

            _genAlg.Start(); 
            ctrlCellularAutomata.Run(_genAlg.InitialCells, _genAlg.CurrentRules);
        }

        private void StopGeneticAlgorithm()
        {
            ctrlGeneticAlgorithmSettings.Visible = true;
            ctrlCellularAutomataSettings.Visible = true;
            ctrlInProgressPanel.Visible = false;
            btnLoad.Visible = true;

            btnRun.Text = "Run genetic algorithm";
            ctrlCellularAutomata.Stop();
            foreach (var btn in _fitnessButtons)
                btn.Enabled = false;
            _genAlg.Stop();
        }

        internal void GoToNextGeneration()
        {
            if (_genAlg.IsInProgess && _genAlg.CanGoToNextGeneration)
            {
                _genAlg.GoToNextGeneration();
                ctrlInProgressPanel.SetGenerationItemCount(0);
                ctrlInProgressPanel.SetGenerationNumber(_genAlg.GenerationNumber);
                ctrlCellularAutomata.Run(_genAlg.InitialCells, _genAlg.CurrentRules);
            }
        }

        internal void RerunCurrentRules()
        {
            ctrlCellularAutomata.Run(_genAlg.InitialCells, _genAlg.CurrentRules);
        }

        private void btnSuperBad_Click(object sender, EventArgs e)
        {
            HandleFitnessButtonClick(FitnessJudgement.SuperBad);
        }

        private void btnBad_Click(object sender, EventArgs e)
        {
            HandleFitnessButtonClick(FitnessJudgement.Bad);
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            HandleFitnessButtonClick(FitnessJudgement.Okay);
        }

        private void btnGood_Click(object sender, EventArgs e)
        {
            HandleFitnessButtonClick(FitnessJudgement.Good);
        }

        private void btnSuperGood_Click(object sender, EventArgs e)
        {
            HandleFitnessButtonClick(FitnessJudgement.SuperGood);
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            HandleFitnessButtonClick(FitnessJudgement.Skip);
        }

        public void LoadRules()
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Lifelike JSON|*.json";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult result = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (result != DialogResult.OK)
                return;

            RulesSaveData data = RulesSaveData.Load(openFileDialog1.FileName);
            if (data == null)
                return;
            
            CellularAutomataRules rules = new CellularAutomataRules(data.StateTable);
            if (!_genAlg.IsInProgess)
            {
                ctrlCellularAutomataSettings.Set(data.CellStructure, rules.NumStates, data.NeighborhoodFunction);
                ctrlGeneticAlgorithmSettings.SetNumStates(rules.NumStates);
            }
            else
            {
                if (_genAlg.CaSettings.NeighborhoodFunction.Name != data.NeighborhoodFunction ||
                        _genAlg.CaSettings.NumStates != rules.NumStates ||
                        _genAlg.CaSettings.CellStructure.Name != data.CellStructure)
                {
                    MessageBox.Show(
                        "Neighbor function, cell structure, and/or number states do not match the currently running genetic algorithm settings",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
                _genAlg.CurrentRules = rules;
            }
            Cells cells = _genAlg.CaSettings.GetInitialCells(_genAlg.GaSettings.InitialStateDistribution);
            ctrlCellularAutomata.Run(cells, rules);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadRules();
        }

        public void SaveCurrentRules()
        {
            CellularAutomataRules rules = _genAlg.CurrentRules;
            var settingsCa = _genAlg.CaSettings;
            var saveData = new RulesSaveData(settingsCa, rules.StateTable);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Lifelike JSON|*.json";
            saveFileDialog1.Title = "Save current cellular automata";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
                saveData.Save(saveFileDialog1.FileName);
        }

        public void GoToPreviousGeneration()
        {
            _genAlg.GoToPrevGeneration();
            ctrlInProgressPanel.SetGenerationItemCount( _genAlg.GenerationItemCount );
            ctrlInProgressPanel.SetGenerationNumber(_genAlg.GenerationNumber);
            ctrlCellularAutomata.Run(_genAlg.InitialCells, _genAlg.CurrentRules);
        }

        private void ctrlGeneticAlgorithmSettings_Load(object sender, EventArgs e)
        {

        }

        public List<Color> Colors
        {
            get
            {
                return ctrlCellularAutomata.Colors;
            }

            set
            {
                ctrlCellularAutomata.Colors = value;
            }
        }

        public void SetNumStates(int states)
        {
            ctrlCellularAutomataSettings.SetNumStates(states);
            ctrlGeneticAlgorithmSettings.SetNumStates(states);
        }

        private void btnRerun_Click(object sender, EventArgs e)
        {
            RerunCurrentRules();
        }
    }
}
