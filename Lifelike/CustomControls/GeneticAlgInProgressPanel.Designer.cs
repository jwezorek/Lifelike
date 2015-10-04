namespace Lifelike.CustomControls
{
    partial class GeneticAlgInProgressPanel
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
            this.lblNextGenItemCount = new System.Windows.Forms.Label();
            this.btnGoToNextGeneration = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReturnToPrevGeneration = new System.Windows.Forms.Button();
            this.lblGenerationNumber = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNextGenItemCount
            // 
            this.lblNextGenItemCount.AutoSize = true;
            this.lblNextGenItemCount.Location = new System.Drawing.Point(10, 41);
            this.lblNextGenItemCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNextGenItemCount.Name = "lblNextGenItemCount";
            this.lblNextGenItemCount.Size = new System.Drawing.Size(35, 13);
            this.lblNextGenItemCount.TabIndex = 0;
            this.lblNextGenItemCount.Text = "label1";
            // 
            // btnGoToNextGeneration
            // 
            this.btnGoToNextGeneration.Location = new System.Drawing.Point(11, 69);
            this.btnGoToNextGeneration.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGoToNextGeneration.Name = "btnGoToNextGeneration";
            this.btnGoToNextGeneration.Size = new System.Drawing.Size(172, 25);
            this.btnGoToNextGeneration.TabIndex = 1;
            this.btnGoToNextGeneration.Text = "Go to next generation >>";
            this.btnGoToNextGeneration.UseVisualStyleBackColor = true;
            this.btnGoToNextGeneration.Click += new System.EventHandler(this.btnGoToNextGeneration_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 127);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "____________________________";
            // 
            // btnReturnToPrevGeneration
            // 
            this.btnReturnToPrevGeneration.Location = new System.Drawing.Point(10, 99);
            this.btnReturnToPrevGeneration.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnReturnToPrevGeneration.Name = "btnReturnToPrevGeneration";
            this.btnReturnToPrevGeneration.Size = new System.Drawing.Size(172, 25);
            this.btnReturnToPrevGeneration.TabIndex = 4;
            this.btnReturnToPrevGeneration.Text = "<< Return to previous generation";
            this.btnReturnToPrevGeneration.UseVisualStyleBackColor = true;
            this.btnReturnToPrevGeneration.Click += new System.EventHandler(this.btnReturnToPrevGeneration_Click);
            // 
            // lblGenerationNumber
            // 
            this.lblGenerationNumber.AutoSize = true;
            this.lblGenerationNumber.Location = new System.Drawing.Point(10, 14);
            this.lblGenerationNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGenerationNumber.Name = "lblGenerationNumber";
            this.lblGenerationNumber.Size = new System.Drawing.Size(35, 13);
            this.lblGenerationNumber.TabIndex = 5;
            this.lblGenerationNumber.Text = "label1";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 183);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(172, 25);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save current rules";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(10, 154);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(172, 25);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load rules";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // GeneticAlgInProgressPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblGenerationNumber);
            this.Controls.Add(this.btnReturnToPrevGeneration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGoToNextGeneration);
            this.Controls.Add(this.lblNextGenItemCount);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GeneticAlgInProgressPanel";
            this.Size = new System.Drawing.Size(192, 236);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNextGenItemCount;
        private System.Windows.Forms.Button btnGoToNextGeneration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReturnToPrevGeneration;
        private System.Windows.Forms.Label lblGenerationNumber;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
    }
}
