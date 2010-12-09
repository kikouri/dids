namespace PKI
{
    partial class GUI
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
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.textBoxIAK = new System.Windows.Forms.TextBox();
            this.textBoxRefValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxRevocationList = new System.Windows.Forms.ListBox();
            this.buttonRevocate = new System.Windows.Forms.Button();
            this.textBoxSerialNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSubjectName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(12, 169);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(118, 35);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Register (Off Band)";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // textBoxIAK
            // 
            this.textBoxIAK.Location = new System.Drawing.Point(12, 78);
            this.textBoxIAK.Name = "textBoxIAK";
            this.textBoxIAK.ReadOnly = true;
            this.textBoxIAK.Size = new System.Drawing.Size(120, 20);
            this.textBoxIAK.TabIndex = 1;
            // 
            // textBoxRefValue
            // 
            this.textBoxRefValue.Location = new System.Drawing.Point(12, 26);
            this.textBoxRefValue.Name = "textBoxRefValue";
            this.textBoxRefValue.ReadOnly = true;
            this.textBoxRefValue.Size = new System.Drawing.Size(120, 20);
            this.textBoxRefValue.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Initial Authentication Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Reference Value";
            // 
            // listBoxRevocationList
            // 
            this.listBoxRevocationList.FormattingEnabled = true;
            this.listBoxRevocationList.Location = new System.Drawing.Point(217, 26);
            this.listBoxRevocationList.Name = "listBoxRevocationList";
            this.listBoxRevocationList.Size = new System.Drawing.Size(120, 95);
            this.listBoxRevocationList.TabIndex = 5;
            // 
            // buttonRevocate
            // 
            this.buttonRevocate.Location = new System.Drawing.Point(355, 68);
            this.buttonRevocate.Name = "buttonRevocate";
            this.buttonRevocate.Size = new System.Drawing.Size(75, 23);
            this.buttonRevocate.TabIndex = 6;
            this.buttonRevocate.Text = "Revocate!";
            this.buttonRevocate.UseVisualStyleBackColor = true;
            this.buttonRevocate.Click += new System.EventHandler(this.buttonRevocate_Click);
            // 
            // textBoxSerialNumber
            // 
            this.textBoxSerialNumber.Location = new System.Drawing.Point(355, 42);
            this.textBoxSerialNumber.Name = "textBoxSerialNumber";
            this.textBoxSerialNumber.Size = new System.Drawing.Size(100, 20);
            this.textBoxSerialNumber.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Serial Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(215, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Revocation List";
            // 
            // textBoxSubjectName
            // 
            this.textBoxSubjectName.Location = new System.Drawing.Point(12, 132);
            this.textBoxSubjectName.Name = "textBoxSubjectName";
            this.textBoxSubjectName.Size = new System.Drawing.Size(120, 20);
            this.textBoxSubjectName.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Subject Name";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 216);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxSubjectName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSerialNumber);
            this.Controls.Add(this.buttonRevocate);
            this.Controls.Add(this.listBoxRevocationList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRefValue);
            this.Controls.Add(this.textBoxIAK);
            this.Controls.Add(this.buttonGenerate);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Public Key Infrastructure GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.TextBox textBoxIAK;
        private System.Windows.Forms.TextBox textBoxRefValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxRevocationList;
        private System.Windows.Forms.Button buttonRevocate;
        private System.Windows.Forms.TextBox textBoxSerialNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSubjectName;
        private System.Windows.Forms.Label label5;
    }
}

