﻿namespace GraphicsProgramming
{
    partial class GrafPack
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


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GrafPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GrafPack";
            this.Text = "GrafPack";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseClick);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GrafPack_MouseUp);
            this.ResumeLayout(false);

        }
    }
}

