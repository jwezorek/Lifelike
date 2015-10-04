namespace Lifelike
{
    partial class GeneticAlgorithmSettingsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnInitialStateDistribution = new System.Windows.Forms.Button();
            this.btnStateTableDistirbution = new System.Windows.Forms.Button();
            this.btnReproFuncs = new System.Windows.Forms.Button();
            this.btnMutationFuncs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Genetic algorithm settings...";
            // 
            // btnInitialStateDistribution
            // 
            this.btnInitialStateDistribution.Location = new System.Drawing.Point(10, 39);
            this.btnInitialStateDistribution.Name = "btnInitialStateDistribution";
            this.btnInitialStateDistribution.Size = new System.Drawing.Size(239, 30);
            this.btnInitialStateDistribution.TabIndex = 1;
            this.btnInitialStateDistribution.Text = "Initial state distribution ";
            this.btnInitialStateDistribution.UseVisualStyleBackColor = true;
            this.btnInitialStateDistribution.Click += new System.EventHandler(this.btnInitialStateDistribution_Click);
            // 
            // btnStateTableDistirbution
            // 
            this.btnStateTableDistirbution.Location = new System.Drawing.Point(10, 75);
            this.btnStateTableDistirbution.Name = "btnStateTableDistirbution";
            this.btnStateTableDistirbution.Size = new System.Drawing.Size(239, 30);
            this.btnStateTableDistirbution.TabIndex = 2;
            this.btnStateTableDistirbution.Text = "State table distribution";
            this.btnStateTableDistirbution.UseVisualStyleBackColor = true;
            this.btnStateTableDistirbution.Click += new System.EventHandler(this.btnStateTableDistirbution_Click);
            // 
            // btnReproFuncs
            // 
            this.btnReproFuncs.Location = new System.Drawing.Point(10, 111);
            this.btnReproFuncs.Name = "btnReproFuncs";
            this.btnReproFuncs.Size = new System.Drawing.Size(239, 30);
            this.btnReproFuncs.TabIndex = 3;
            this.btnReproFuncs.Text = "Reproduction function distribution";
            this.btnReproFuncs.UseVisualStyleBackColor = true;
            this.btnReproFuncs.Click += new System.EventHandler(this.btnReproFuncs_Click);
            // 
            // btnMutationFuncs
            // 
            this.btnMutationFuncs.Location = new System.Drawing.Point(10, 147);
            this.btnMutationFuncs.Name = "btnMutationFuncs";
            this.btnMutationFuncs.Size = new System.Drawing.Size(239, 30);
            this.btnMutationFuncs.TabIndex = 4;
            this.btnMutationFuncs.Text = "Mutation function distribution";
            this.btnMutationFuncs.UseVisualStyleBackColor = true;
            this.btnMutationFuncs.Click += new System.EventHandler(this.btnMutationFuncs_Click);
            // 
            // GeneticAlgorithmSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnMutationFuncs);
            this.Controls.Add(this.btnReproFuncs);
            this.Controls.Add(this.btnStateTableDistirbution);
            this.Controls.Add(this.btnInitialStateDistribution);
            this.Controls.Add(this.label1);
            this.Name = "GeneticAlgorithmSettingsControl";
            this.Size = new System.Drawing.Size(260, 260);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInitialStateDistribution;
        private System.Windows.Forms.Button btnStateTableDistirbution;
        private System.Windows.Forms.Button btnReproFuncs;
        private System.Windows.Forms.Button btnMutationFuncs;
    }
}
