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
    public partial class GeneticAlgorithmSettingsControl : UserControl
    {
        private GeneticAlgorithmSettings _settings = null;

        public GeneticAlgorithmSettingsControl()
        {
            InitializeComponent();
        }

        private void btnReproFuncs_Click(object sender, EventArgs e)
        {
            List<double> probabilities = _settings.ReproductionFuncProbabilities;
            List<string> names = ReproductionFunction.FunctionNames;

            DistributionBox.DistributionInfo info = DistributionBox.ShowDistribution(
                this.Parent,
                "Reproduction",
                "Reproduction function distribution",
                names.Select((string name, int i) => new Tuple<string, double>(name, probabilities[i])).ToList(),
                150
            );
            if (info != null)
                Settings.ReproductionFuncProbabilities = info.Values;
        }

        private void btnMutationFuncs_Click(object sender, System.EventArgs e)
        {
            List<double> probabilities = _settings.MutationFunctionProbabilities;
            List<string> names = MutationFunction.FunctionNames;

            DistributionBox.DistributionInfo info = DistributionBox.ShowDistribution(
                this.Parent,
                "Mutation",
                "Mutation function distribution",
                names.Select( (string name, int i) => new Tuple<string, double>(name, probabilities[i])).ToList(),
                150,
                "Mutation temperature",
                _settings.MutationTemperature
            );
            if (info != null)
            {
                Settings.MutationFunctionProbabilities = info.Values;
                Settings.MutationTemperature = info.ExtraValue;
            }
        }

        private DistributionBox.DistributionInfo DoStatesDistributionDialog(string titleWindow, string titleHistogram, 
                                List<double> probabilities)
        {
            List<Color> colors = ((LifelikeWindow) Parent).Colors;
            int states = probabilities.Count();

            var data = Enumerable.Range(0, states).Select(
                (int i) => {
                    return new Tuple<string, double, Color>(
                        string.Format("State {0}", i),
                        probabilities[i],
                        colors[i]
                    );
                }).ToList();

            return DistributionBox.ShowDistribution(
                this.Parent,
                titleWindow,
                titleHistogram,
                data,
                true
            );
        }

        private void btnInitialStateDistribution_Click(object sender, EventArgs e)
        {
            List<double> probabilities = Settings.InitialStateProbabilities;
            var info = DoStatesDistributionDialog(
                "State Distribution",
                "Distribution of states in initial CA configurations",
                probabilities
            );
            if (info != null)
            {
                Settings.InitialStateProbabilities = info.Values; 
                ((LifelikeWindow) Parent).Colors = info.Colors;
            }
        }

        private void btnStateTableDistirbution_Click(object sender, EventArgs e)
        {
            List<double> probabilities = Settings.StateTableProbabilities;
            var info = DoStatesDistributionDialog(
                "State Distribution",
                "Distribution of states in randomly generated state tables",
                probabilities
            );
            if (info != null)
            {
                Settings.StateTableProbabilities = info.Values;
                ((LifelikeWindow)Parent).Colors = info.Colors;
            }
        }

        public GeneticAlgorithmSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        internal void SetNumStates(int states)
        {
            if (_settings != null)
                _settings.SetNumStates(states);
        }
    }
}
