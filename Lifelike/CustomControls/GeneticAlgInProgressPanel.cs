using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lifelike.CustomControls
{
    public partial class GeneticAlgInProgressPanel : UserControl
    {
        public GeneticAlgInProgressPanel()
        {
            InitializeComponent();
        }

        LifelikeWindow MainWindow
        {
            get
            {
                return (LifelikeWindow)Parent;
            }
        }

        private void btnGoToNextGeneration_Click(object sender, EventArgs e)
        {
            MainWindow.GoToNextGeneration();
        }

        internal void SetGenerationItemCount(int count)
        {
            lblNextGenItemCount.Text = string.Format("{0} CAs added to next generation", count);
        }

        internal void SetGenerationNumber(int gen)
        {
            lblGenerationNumber.Text = string.Format("Generation: {0}", gen);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MainWindow.SaveCurrentRules();
        }

        private void btnReturnToPrevGeneration_Click(object sender, EventArgs e)
        {
            MainWindow.GoToPreviousGeneration();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            MainWindow.LoadRules();
        }
    }
}
