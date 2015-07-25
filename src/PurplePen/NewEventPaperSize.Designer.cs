﻿namespace PurplePen
{
    partial class NewEventPaperSize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewEventPaperSize));
            this.paperSizeControl1 = new PurplePen.PaperSizeControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // paperSizeControl1
            // 
            this.paperSizeControl1.AutoSize = true;
            this.paperSizeControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.paperSizeControl1.Landscape = false;
            this.paperSizeControl1.Location = new System.Drawing.Point(25, 51);
            this.paperSizeControl1.MarginSize = 0;
            this.paperSizeControl1.Name = "paperSizeControl1";
            this.paperSizeControl1.PaperSize = ((System.Drawing.Printing.PaperSize)(resources.GetObject("paperSizeControl1.PaperSize")));
            this.paperSizeControl1.Size = new System.Drawing.Size(271, 163);
            this.paperSizeControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(479, 41);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter the size and orientation of the paper that you will use to print your map.\r" +
    "\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "You can change this later with File/Set Print Area.";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(466, 232);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(60, 15);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "Paper Size";
            this.labelTitle.Visible = false;
            // 
            // NewEventPaperSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.paperSizeControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NewEventPaperSize";
            this.Size = new System.Drawing.Size(529, 247);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PaperSizeControl paperSizeControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTitle;
    }
}
