using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            try
            {
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

                if (data.CustomColors != null)
                    Colors = data.CustomColors.Select(
                        ary => Color.FromArgb(ary[0], ary[1], ary[2])
                    ).ToList();
                
                Cells cells = _genAlg.CaSettings.GetInitialCells(_genAlg.GaSettings.InitialStateDistribution);
                ctrlCellularAutomata.Run(cells, rules);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Invalid CA rules file.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadRules();
        }

        public void DoSaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Lifelike JSON|*.json|Animated GIF|*.gif";
            saveFileDialog.Title = "Save current cellular automata";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".json")
                    SaveRules(saveFileDialog.FileName);
                else
                    ExportAsAnimatedGif(saveFileDialog.FileName);
            }
        }

        public static Image ConvertBitmapToImage(Bitmap oldbmp)
        {
            //I believe this will convert to indexed color..
            using (var ms = new MemoryStream())
            {
                oldbmp.Save(ms, ImageFormat.Gif);
                ms.Position = 0;
                return Image.FromStream(ms);
            }
        }

        private void ExportAsAnimatedGifWorker(string outputFilePath, ProgressBox box, 
            int numFramesPreamble, int numFrames)
        {
            Cells cells = _genAlg.CaSettings.GetInitialCells(_genAlg.GaSettings.InitialStateDistribution);
            CellularAutomataRules rules = ctrlCellularAutomata.Rules;
            NeighborhoodFunction function = _genAlg.CaSettings.NeighborhoodFunction;

            for (int i = 0; i < numFramesPreamble; i++)
            {
                cells = cells.ApplyRules(rules, function);
                box.Invoke(
                    new Action( 
                        ()=>box.Increment()
                    ) 
                );
            }

            using (FileStream fs = new FileStream(outputFilePath, FileMode.Create))
            using (GifEncoder encoder = new GifEncoder(fs, cells.Columns * 2, cells.Rows * 2))
            {
                Bitmap bmp = new Bitmap(cells.Columns * 2, cells.Rows * 2);
                Point offset = new Point(0, 0);
                for (int i = 0; i < numFrames; i++)
                {
                    CellularAutomataControl.PaintBitmap(bmp, cells, offset, ctrlCellularAutomata.Colors);
                    encoder.AddFrame(Image.FromHbitmap(bmp.GetHbitmap()), 0, 0, new TimeSpan(0));
                    cells = cells.ApplyRules(rules, function);

                    box.Increment();
                }
            }

            box.Finish();
        }

        private void ExportAsAnimatedGif(string outputFilePath)
        {
            ctrlCellularAutomata.Stop();

            int numFramesPreamble = 100;
            int numFrames = 800;

            var dlg = new ProgressBox();
            dlg.LabelText = "Generating animated GIF...";
            dlg.ProgressRange = numFramesPreamble + numFrames;
            dlg.StartPosition = FormStartPosition.CenterParent;
            Thread thread = new Thread(
                new ThreadStart(  
                    () => {
                        ExportAsAnimatedGifWorker(outputFilePath, dlg, 
                            numFramesPreamble, numFrames);
                    }
                )
            );
            thread.Start();
            DialogResult dr = dlg.ShowDialog(this);

            ctrlCellularAutomata.Resume();
        }

        private void SaveRules(string fileName)
        {
            CellularAutomataRules rules = _genAlg.CurrentRules;
            var settingsCa = _genAlg.CaSettings;
            var saveData = new RulesSaveData(settingsCa, rules.StateTable);
            saveData.Save(fileName);
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
