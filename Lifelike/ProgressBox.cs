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
    public partial class ProgressBox : Form
    {
        public ProgressBox()
        {
            InitializeComponent();
        }

        public string LabelText 
        { 
            set
            {
                lblTitle.Text = value;
            }
        }

        public int ProgressRange 
        { 
            set
            {
                ctrlProgBar.Value = 0;
                ctrlProgBar.Minimum = 0;
                ctrlProgBar.Maximum = value;
            }
        }

        public void Increment()
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => ctrlProgBar.Value = ctrlProgBar.Value + 1));
            else
                ctrlProgBar.Value = ctrlProgBar.Value + 1;
        }

        public void Finish()
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => Close()));
            else
                Close();
        }
    }
}
