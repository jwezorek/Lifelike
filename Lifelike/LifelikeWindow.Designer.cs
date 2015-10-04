using System;
namespace Lifelike
{
    partial class LifelikeWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LifelikeWindow));
            this.btnBad = new System.Windows.Forms.Button();
            this.btnOkay = new System.Windows.Forms.Button();
            this.btnGood = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnRerun = new System.Windows.Forms.Button();
            this.ctrlGeneticAlgorithmSettings = new Lifelike.GeneticAlgorithmSettingsControl();
            this.ctrlCellularAutomataSettings = new Lifelike.CellularAutomataSettingsControl();
            this.ctrlCellularAutomata = new Lifelike.CellularAutomataControl();
            this.ctrlInProgressPanel = new Lifelike.CustomControls.GeneticAlgInProgressPanel();
            this.SuspendLayout();
            // 
            // btnBad
            // 
            this.btnBad.Enabled = false;
            this.btnBad.Location = new System.Drawing.Point(195, 649);
            this.btnBad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBad.Name = "btnBad";
            this.btnBad.Size = new System.Drawing.Size(84, 30);
            this.btnBad.TabIndex = 2;
            this.btnBad.Text = "Bad";
            this.btnBad.UseVisualStyleBackColor = true;
            this.btnBad.Click += new System.EventHandler(this.btnBad_Click);
            // 
            // btnOkay
            // 
            this.btnOkay.Enabled = false;
            this.btnOkay.Location = new System.Drawing.Point(285, 649);
            this.btnOkay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(84, 30);
            this.btnOkay.TabIndex = 3;
            this.btnOkay.Text = "Okay";
            this.btnOkay.UseVisualStyleBackColor = true;
            this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
            // 
            // btnGood
            // 
            this.btnGood.Enabled = false;
            this.btnGood.Location = new System.Drawing.Point(375, 649);
            this.btnGood.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGood.Name = "btnGood";
            this.btnGood.Size = new System.Drawing.Size(84, 30);
            this.btnGood.TabIndex = 4;
            this.btnGood.Text = "Good";
            this.btnGood.UseVisualStyleBackColor = true;
            this.btnGood.Click += new System.EventHandler(this.btnGood_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(690, 547);
            this.btnRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(183, 39);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "Run GA CA";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.Enabled = false;
            this.btnSkip.Location = new System.Drawing.Point(1, 651);
            this.btnSkip.Margin = new System.Windows.Forms.Padding(4);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(47, 28);
            this.btnSkip.TabIndex = 10;
            this.btnSkip.Text = "Skip";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(690, 592);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(183, 39);
            this.btnLoad.TabIndex = 11;
            this.btnLoad.Text = "Load rules";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnRerun
            // 
            this.btnRerun.Location = new System.Drawing.Point(594, 652);
            this.btnRerun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRerun.Name = "btnRerun";
            this.btnRerun.Size = new System.Drawing.Size(51, 25);
            this.btnRerun.TabIndex = 12;
            this.btnRerun.Text = "Rerun";
            this.btnRerun.UseVisualStyleBackColor = true;
            this.btnRerun.Click += new System.EventHandler(this.btnRerun_Click);
            // 
            // ctrlGeneticAlgorithmSettings
            // 
            this.ctrlGeneticAlgorithmSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctrlGeneticAlgorithmSettings.Location = new System.Drawing.Point(651, 329);
            this.ctrlGeneticAlgorithmSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlGeneticAlgorithmSettings.Name = "ctrlGeneticAlgorithmSettings";
            this.ctrlGeneticAlgorithmSettings.Settings = null;
            this.ctrlGeneticAlgorithmSettings.Size = new System.Drawing.Size(260, 203);
            this.ctrlGeneticAlgorithmSettings.TabIndex = 7;
            this.ctrlGeneticAlgorithmSettings.Load += new System.EventHandler(this.ctrlGeneticAlgorithmSettings_Load);
            // 
            // ctrlCellularAutomataSettings
            // 
            this.ctrlCellularAutomataSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctrlCellularAutomataSettings.Location = new System.Drawing.Point(651, 9);
            this.ctrlCellularAutomataSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlCellularAutomataSettings.Name = "ctrlCellularAutomataSettings";
            this.ctrlCellularAutomataSettings.Size = new System.Drawing.Size(260, 260);
            this.ctrlCellularAutomataSettings.TabIndex = 6;
            // 
            // ctrlCellularAutomata
            // 
            this.ctrlCellularAutomata.CellularAutomataSettings = null;
            this.ctrlCellularAutomata.Colors = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("ctrlCellularAutomata.Colors")));
            this.ctrlCellularAutomata.Location = new System.Drawing.Point(1, 1);
            this.ctrlCellularAutomata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlCellularAutomata.Name = "ctrlCellularAutomata";
            this.ctrlCellularAutomata.Size = new System.Drawing.Size(644, 644);
            this.ctrlCellularAutomata.TabIndex = 0;
            this.ctrlCellularAutomata.Text = "cellularAutomataControl1";
            this.ctrlCellularAutomata.Click += new System.EventHandler(this.ctrlCellularAutomata_Click);
            // 
            // ctrlInProgressPanel
            // 
            this.ctrlInProgressPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctrlInProgressPanel.Location = new System.Drawing.Point(651, 11);
            this.ctrlInProgressPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlInProgressPanel.Name = "ctrlInProgressPanel";
            this.ctrlInProgressPanel.Size = new System.Drawing.Size(260, 260);
            this.ctrlInProgressPanel.TabIndex = 9;
            this.ctrlInProgressPanel.Visible = false;
            // 
            // LifelikeWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(916, 683);
            this.Controls.Add(this.btnRerun);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.ctrlGeneticAlgorithmSettings);
            this.Controls.Add(this.ctrlCellularAutomataSettings);
            this.Controls.Add(this.btnGood);
            this.Controls.Add(this.btnOkay);
            this.Controls.Add(this.btnBad);
            this.Controls.Add(this.ctrlCellularAutomata);
            this.Controls.Add(this.ctrlInProgressPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LifelikeWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Lifelike Cellular Automata Breeder";
            this.ResumeLayout(false);

        }

        #endregion

        private CellularAutomataControl ctrlCellularAutomata;
        private System.Windows.Forms.Button btnBad;
        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.Button btnGood;
        private CellularAutomataSettingsControl ctrlCellularAutomataSettings;
        private GeneticAlgorithmSettingsControl ctrlGeneticAlgorithmSettings;
        private System.Windows.Forms.Button btnRun;
        private CustomControls.GeneticAlgInProgressPanel ctrlInProgressPanel;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnRerun;
    }
}

