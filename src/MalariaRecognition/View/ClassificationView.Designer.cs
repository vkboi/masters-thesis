
namespace MalariaRecognition.View
{
    partial class ClassificationView
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMain = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelectImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelectAnnotation = new System.Windows.Forms.ToolStripMenuItem();
            this.pbCurrentImage = new System.Windows.Forms.PictureBox();
            this.btnClassify = new System.Windows.Forms.Button();
            this.lblCurrentAnnotation = new System.Windows.Forms.Label();
            this.lblCurrentImage = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentImage)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMain});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1350, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiMain
            // 
            this.tsmiMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelectImage,
            this.tsmiSelectAnnotation});
            this.tsmiMain.Name = "tsmiMain";
            this.tsmiMain.Size = new System.Drawing.Size(50, 20);
            this.tsmiMain.Text = "Menü";
            // 
            // tsmiSelectImage
            // 
            this.tsmiSelectImage.Name = "tsmiSelectImage";
            this.tsmiSelectImage.Size = new System.Drawing.Size(180, 22);
            this.tsmiSelectImage.Text = "Kép választása";
            this.tsmiSelectImage.Click += new System.EventHandler(this.tsmiSelectImage_Click);
            // 
            // tsmiSelectAnnotation
            // 
            this.tsmiSelectAnnotation.Name = "tsmiSelectAnnotation";
            this.tsmiSelectAnnotation.Size = new System.Drawing.Size(180, 22);
            this.tsmiSelectAnnotation.Text = "Annotáció betöltése";
            this.tsmiSelectAnnotation.Click += new System.EventHandler(this.tsmiSelectAnnotation_Click);
            // 
            // pbCurrentImage
            // 
            this.pbCurrentImage.Location = new System.Drawing.Point(12, 27);
            this.pbCurrentImage.Name = "pbCurrentImage";
            this.pbCurrentImage.Size = new System.Drawing.Size(1326, 661);
            this.pbCurrentImage.TabIndex = 1;
            this.pbCurrentImage.TabStop = false;
            // 
            // btnClassify
            // 
            this.btnClassify.Location = new System.Drawing.Point(643, 694);
            this.btnClassify.Name = "btnClassify";
            this.btnClassify.Size = new System.Drawing.Size(80, 23);
            this.btnClassify.TabIndex = 2;
            this.btnClassify.Text = "Osztályzás";
            this.btnClassify.UseVisualStyleBackColor = true;
            this.btnClassify.Click += new System.EventHandler(this.btnClassify_Click);
            // 
            // lblCurrentAnnotation
            // 
            this.lblCurrentAnnotation.AutoSize = true;
            this.lblCurrentAnnotation.Location = new System.Drawing.Point(745, 699);
            this.lblCurrentAnnotation.Name = "lblCurrentAnnotation";
            this.lblCurrentAnnotation.Size = new System.Drawing.Size(104, 13);
            this.lblCurrentAnnotation.TabIndex = 3;
            this.lblCurrentAnnotation.Text = "Jelenlegi annotáció: ";
            // 
            // lblCurrentImage
            // 
            this.lblCurrentImage.AutoSize = true;
            this.lblCurrentImage.Location = new System.Drawing.Point(24, 699);
            this.lblCurrentImage.Name = "lblCurrentImage";
            this.lblCurrentImage.Size = new System.Drawing.Size(75, 13);
            this.lblCurrentImage.TabIndex = 4;
            this.lblCurrentImage.Text = "Jelenlegi kép: ";
            // 
            // ClassificationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.lblCurrentImage);
            this.Controls.Add(this.lblCurrentAnnotation);
            this.Controls.Add(this.btnClassify);
            this.Controls.Add(this.pbCurrentImage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ClassificationView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Osztályzás";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectImage;
        private System.Windows.Forms.PictureBox pbCurrentImage;
        private System.Windows.Forms.Button btnClassify;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAnnotation;
        private System.Windows.Forms.Label lblCurrentAnnotation;
        private System.Windows.Forms.Label lblCurrentImage;
    }
}