namespace Lifelike
{
    partial class CellularAutomataSettingsControl
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
            this.comboCellStructure = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numboxStates = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboMappingFunction = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numboxStates)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cellular Automata Settings...";
            // 
            // comboCellStructure
            // 
            this.comboCellStructure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCellStructure.FormattingEnabled = true;
            this.comboCellStructure.Location = new System.Drawing.Point(8, 50);
            this.comboCellStructure.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboCellStructure.Name = "comboCellStructure";
            this.comboCellStructure.Size = new System.Drawing.Size(180, 21);
            this.comboCellStructure.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Grid/Neighborhood";
            // 
            // numboxStates
            // 
            this.numboxStates.Location = new System.Drawing.Point(10, 161);
            this.numboxStates.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numboxStates.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numboxStates.Name = "numboxStates";
            this.numboxStates.Size = new System.Drawing.Size(55, 20);
            this.numboxStates.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 145);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Number of states";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Neighborhood mapping function";
            // 
            // comboMappingFunction
            // 
            this.comboMappingFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMappingFunction.FormattingEnabled = true;
            this.comboMappingFunction.Location = new System.Drawing.Point(8, 104);
            this.comboMappingFunction.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboMappingFunction.Name = "comboMappingFunction";
            this.comboMappingFunction.Size = new System.Drawing.Size(180, 21);
            this.comboMappingFunction.TabIndex = 5;
            // 
            // CellularAutomataSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboMappingFunction);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numboxStates);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboCellStructure);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "CellularAutomataSettingsControl";
            this.Size = new System.Drawing.Size(195, 211);
            ((System.ComponentModel.ISupportInitialize)(this.numboxStates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboCellStructure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numboxStates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboMappingFunction;


    }
}
