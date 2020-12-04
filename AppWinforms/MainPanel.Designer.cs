using System;

namespace App
{
    partial class MainPanel
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
            this.addScriptBtn = new System.Windows.Forms.Button();
            this.scriptsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.scriptsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // addScriptBtn
            // 
            this.addScriptBtn.Location = new System.Drawing.Point(44, 3);
            this.addScriptBtn.Name = "addScriptBtn";
            this.addScriptBtn.Size = new System.Drawing.Size(96, 28);
            this.addScriptBtn.TabIndex = 0;
            this.addScriptBtn.Text = "Add Script";
            this.addScriptBtn.UseVisualStyleBackColor = true;
            this.addScriptBtn.Click += new System.EventHandler(this.addScriptBtn_Click);
            // 
            // scriptsPanel
            // 
            this.scriptsPanel.AutoSize = true;
            this.scriptsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scriptsPanel.Controls.Add(this.label1);
            this.scriptsPanel.Controls.Add(this.addScriptBtn);
            this.scriptsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.scriptsPanel.Location = new System.Drawing.Point(0, 0);
            this.scriptsPanel.Name = "scriptsPanel";
            this.scriptsPanel.Size = new System.Drawing.Size(143, 34);
            this.scriptsPanel.TabIndex = 1;
            this.scriptsPanel.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // MainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.scriptsPanel);
            this.MinimumSize = new System.Drawing.Size(100, 30);
            this.Name = "MainPanel";
            this.Size = new System.Drawing.Size(143, 34);
            this.scriptsPanel.ResumeLayout(false);
            this.scriptsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addScriptBtn;
        private System.Windows.Forms.FlowLayoutPanel scriptsPanel;
        private System.Windows.Forms.Label label1;
    }
}
