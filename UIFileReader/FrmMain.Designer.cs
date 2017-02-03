namespace UIFileReader
{
    partial class FrmMain
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
            this.lblArchivo = new System.Windows.Forms.Label();
            this.txtArchivo = new System.Windows.Forms.TextBox();
            this.cmbImp = new System.Windows.Forms.ComboBox();
            this.lblImp = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtContent = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lblArchivo
            // 
            this.lblArchivo.AutoSize = true;
            this.lblArchivo.Location = new System.Drawing.Point(13, 13);
            this.lblArchivo.Name = "lblArchivo";
            this.lblArchivo.Size = new System.Drawing.Size(46, 13);
            this.lblArchivo.TabIndex = 0;
            this.lblArchivo.Text = "Archivo:";
            // 
            // txtArchivo
            // 
            this.txtArchivo.Location = new System.Drawing.Point(102, 10);
            this.txtArchivo.Name = "txtArchivo";
            this.txtArchivo.Size = new System.Drawing.Size(183, 20);
            this.txtArchivo.TabIndex = 1;
            // 
            // cmbImp
            // 
            this.cmbImp.FormattingEnabled = true;
            this.cmbImp.Location = new System.Drawing.Point(102, 37);
            this.cmbImp.Name = "cmbImp";
            this.cmbImp.Size = new System.Drawing.Size(183, 21);
            this.cmbImp.TabIndex = 2;
            // 
            // lblImp
            // 
            this.lblImp.AutoSize = true;
            this.lblImp.Location = new System.Drawing.Point(13, 40);
            this.lblImp.Name = "lblImp";
            this.lblImp.Size = new System.Drawing.Size(84, 13);
            this.lblImp.TabIndex = 3;
            this.lblImp.Text = "Implementación:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(291, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 40);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(12, 64);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(354, 165);
            this.txtContent.TabIndex = 5;
            this.txtContent.Text = "";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 250);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblImp);
            this.Controls.Add(this.cmbImp);
            this.Controls.Add(this.txtArchivo);
            this.Controls.Add(this.lblArchivo);
            this.Name = "FrmMain";
            this.Text = "File Reader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblArchivo;
        private System.Windows.Forms.TextBox txtArchivo;
        private System.Windows.Forms.ComboBox cmbImp;
        private System.Windows.Forms.Label lblImp;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RichTextBox txtContent;
    }
}

