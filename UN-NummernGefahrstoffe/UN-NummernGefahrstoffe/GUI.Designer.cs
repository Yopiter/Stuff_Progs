namespace UN_NummernGefahrstoffe
{
    partial class GUI
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbBeschr = new System.Windows.Forms.TextBox();
            this.numPad = new System.Windows.Forms.NumericUpDown();
            this.btSearch = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.tbGefZahl = new System.Windows.Forms.TextBox();
            this.tbKlasse = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numPad)).BeginInit();
            this.SuspendLayout();
            // 
            // tbBeschr
            // 
            this.tbBeschr.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbBeschr.Location = new System.Drawing.Point(0, 64);
            this.tbBeschr.Multiline = true;
            this.tbBeschr.Name = "tbBeschr";
            this.tbBeschr.ReadOnly = true;
            this.tbBeschr.Size = new System.Drawing.Size(572, 195);
            this.tbBeschr.TabIndex = 0;
            // 
            // numPad
            // 
            this.numPad.Location = new System.Drawing.Point(12, 12);
            this.numPad.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPad.Name = "numPad";
            this.numPad.Size = new System.Drawing.Size(93, 20);
            this.numPad.TabIndex = 1;
            this.numPad.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(111, 12);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(142, 20);
            this.btSearch.TabIndex = 2;
            this.btSearch.Text = "Bezeichnung suchen";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Location = new System.Drawing.Point(350, 10);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(199, 20);
            this.btUpdate.TabIndex = 3;
            this.btUpdate.Text = "Update";
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // tbGefZahl
            // 
            this.tbGefZahl.Location = new System.Drawing.Point(85, 38);
            this.tbGefZahl.Name = "tbGefZahl";
            this.tbGefZahl.ReadOnly = true;
            this.tbGefZahl.Size = new System.Drawing.Size(100, 20);
            this.tbGefZahl.TabIndex = 4;
            // 
            // tbKlasse
            // 
            this.tbKlasse.Location = new System.Drawing.Point(255, 38);
            this.tbKlasse.Name = "tbKlasse";
            this.tbKlasse.ReadOnly = true;
            this.tbKlasse.Size = new System.Drawing.Size(100, 20);
            this.tbKlasse.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Gefahrenzahl";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Klasse";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 259);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbKlasse);
            this.Controls.Add(this.tbGefZahl);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.numPad);
            this.Controls.Add(this.tbBeschr);
            this.Name = "GUI";
            this.Text = "UN-Nummern";
            ((System.ComponentModel.ISupportInitialize)(this.numPad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBeschr;
        private System.Windows.Forms.NumericUpDown numPad;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.TextBox tbGefZahl;
        private System.Windows.Forms.TextBox tbKlasse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

