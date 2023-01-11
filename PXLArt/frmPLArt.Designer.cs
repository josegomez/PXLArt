
namespace PXLArt
{
    partial class XLPixelArtGenerator
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
            this.btnCreatePXLArt = new System.Windows.Forms.Button();
            this.picPic = new System.Windows.Forms.PictureBox();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.picPic)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreatePXLArt
            // 
            this.btnCreatePXLArt.Location = new System.Drawing.Point(12, 304);
            this.btnCreatePXLArt.Name = "btnCreatePXLArt";
            this.btnCreatePXLArt.Size = new System.Drawing.Size(114, 23);
            this.btnCreatePXLArt.TabIndex = 0;
            this.btnCreatePXLArt.Text = "Convert Image";
            this.btnCreatePXLArt.UseVisualStyleBackColor = true;
            this.btnCreatePXLArt.Click += new System.EventHandler(this.button1_Click);
            // 
            // picPic
            // 
            this.picPic.Location = new System.Drawing.Point(12, 12);
            this.picPic.Name = "picPic";
            this.picPic.Size = new System.Drawing.Size(284, 284);
            this.picPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPic.TabIndex = 1;
            this.picPic.TabStop = false;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(12, 333);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(283, 23);
            this.pbProgress.TabIndex = 2;
            // 
            // XLPixelArtGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 368);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.picPic);
            this.Controls.Add(this.btnCreatePXLArt);
            this.MinimizeBox = false;
            this.Name = "XLPixelArtGenerator";
            this.Text = "XL Pixel Art Generator";
            ((System.ComponentModel.ISupportInitialize)(this.picPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreatePXLArt;
        private System.Windows.Forms.PictureBox picPic;
        private System.Windows.Forms.ProgressBar pbProgress;
    }
}

