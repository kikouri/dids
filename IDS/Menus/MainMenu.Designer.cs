﻿namespace IDS.Menus
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sendPluginButton = new System.Windows.Forms.Button();
            this.listAttacksButton = new System.Windows.Forms.Button();
            this.shareNewAttackButton = new System.Windows.Forms.Button();
            this.listSolutionsButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Share New Attack Discovered";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Send Attack Solution Plugin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 386);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "List Detected Attacks";
            // 
            // sendPluginButton
            // 
            this.sendPluginButton.BackgroundImage = global::IDS.Properties.Resources.cyberMID;
            this.sendPluginButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sendPluginButton.Location = new System.Drawing.Point(368, 117);
            this.sendPluginButton.Name = "sendPluginButton";
            this.sendPluginButton.Size = new System.Drawing.Size(106, 104);
            this.sendPluginButton.TabIndex = 2;
            this.sendPluginButton.UseVisualStyleBackColor = true;
            this.sendPluginButton.Click += new System.EventHandler(this.sendPluginButton_Click);
            // 
            // listAttacksButton
            // 
            this.listAttacksButton.BackgroundImage = global::IDS.Properties.Resources.List_Of_Medications_For_Panic_Attacks;
            this.listAttacksButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.listAttacksButton.Location = new System.Drawing.Point(108, 280);
            this.listAttacksButton.Name = "listAttacksButton";
            this.listAttacksButton.Size = new System.Drawing.Size(106, 104);
            this.listAttacksButton.TabIndex = 1;
            this.listAttacksButton.UseVisualStyleBackColor = true;
            this.listAttacksButton.Click += new System.EventHandler(this.listAttacksButton_Click);
            // 
            // shareNewAttackButton
            // 
            this.shareNewAttackButton.BackgroundImage = global::IDS.Properties.Resources.pc_trouble;
            this.shareNewAttackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shareNewAttackButton.Location = new System.Drawing.Point(108, 117);
            this.shareNewAttackButton.Name = "shareNewAttackButton";
            this.shareNewAttackButton.Size = new System.Drawing.Size(106, 104);
            this.shareNewAttackButton.TabIndex = 0;
            this.shareNewAttackButton.UseVisualStyleBackColor = true;
            this.shareNewAttackButton.Click += new System.EventHandler(this.shareNewAttackButton_Click);
            // 
            // listSolutionsButton
            // 
            this.listSolutionsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("listSolutionsButton.BackgroundImage")));
            this.listSolutionsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.listSolutionsButton.Location = new System.Drawing.Point(368, 280);
            this.listSolutionsButton.Name = "listSolutionsButton";
            this.listSolutionsButton.Size = new System.Drawing.Size(106, 104);
            this.listSolutionsButton.TabIndex = 6;
            this.listSolutionsButton.UseVisualStyleBackColor = true;
            this.listSolutionsButton.Click += new System.EventHandler(this.listSolutionsButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 386);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "List Received Attack Solutions";
            // 
            // buttonQuit
            // 
            this.buttonQuit.Location = new System.Drawing.Point(231, 496);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(103, 54);
            this.buttonQuit.TabIndex = 8;
            this.buttonQuit.Text = "Save and Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.butttonQuit_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listSolutionsButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendPluginButton);
            this.Controls.Add(this.listAttacksButton);
            this.Controls.Add(this.shareNewAttackButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button shareNewAttackButton;
        private System.Windows.Forms.Button listAttacksButton;
        private System.Windows.Forms.Button sendPluginButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button listSolutionsButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonQuit;
    }
}