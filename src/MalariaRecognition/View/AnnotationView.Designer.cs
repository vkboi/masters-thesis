
namespace MalariaRecognition.View
{
    partial class AnnotationView
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
            this.pbCurrentImage = new System.Windows.Forms.PictureBox();
            this.dgvClassNames = new System.Windows.Forms.DataGridView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadAnnotation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadClasses = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiApproximate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiApproximateWithClasses = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiApproximateBBoxes = new System.Windows.Forms.ToolStripMenuItem();
            this.ssInfo = new System.Windows.Forms.StatusStrip();
            this.tsslCurrentAnnotationLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCurrentAnnotation = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslNewAnnotationData = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvAnnotations = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassNames)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.ssInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnnotations)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCurrentImage
            // 
            this.pbCurrentImage.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pbCurrentImage.Location = new System.Drawing.Point(12, 27);
            this.pbCurrentImage.Name = "pbCurrentImage";
            this.pbCurrentImage.Size = new System.Drawing.Size(1634, 989);
            this.pbCurrentImage.TabIndex = 0;
            this.pbCurrentImage.TabStop = false;
            this.pbCurrentImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCurrentImage_MouseDown);
            this.pbCurrentImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCurrentImage_MouseMove);
            this.pbCurrentImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCurrentImage_MouseUp);
            // 
            // dgvClassNames
            // 
            this.dgvClassNames.AllowUserToAddRows = false;
            this.dgvClassNames.AllowUserToDeleteRows = false;
            this.dgvClassNames.AllowUserToResizeColumns = false;
            this.dgvClassNames.AllowUserToResizeRows = false;
            this.dgvClassNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClassNames.Location = new System.Drawing.Point(1652, 645);
            this.dgvClassNames.MultiSelect = false;
            this.dgvClassNames.Name = "dgvClassNames";
            this.dgvClassNames.ReadOnly = true;
            this.dgvClassNames.RowHeadersVisible = false;
            this.dgvClassNames.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvClassNames.Size = new System.Drawing.Size(240, 371);
            this.dgvClassNames.TabIndex = 2;
            this.dgvClassNames.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvClassNames_CellFormatting);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoad,
            this.tsmiApproximate});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1904, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // tsmiLoad
            // 
            this.tsmiLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadImage,
            this.tsmiLoadAnnotation,
            this.tsmiLoadClasses});
            this.tsmiLoad.Name = "tsmiLoad";
            this.tsmiLoad.Size = new System.Drawing.Size(61, 20);
            this.tsmiLoad.Text = "Betöltés";
            // 
            // tsmiLoadImage
            // 
            this.tsmiLoadImage.Name = "tsmiLoadImage";
            this.tsmiLoadImage.Size = new System.Drawing.Size(129, 22);
            this.tsmiLoadImage.Text = "Kép";
            this.tsmiLoadImage.Click += new System.EventHandler(this.tsmiLoadImage_Click);
            // 
            // tsmiLoadAnnotation
            // 
            this.tsmiLoadAnnotation.Name = "tsmiLoadAnnotation";
            this.tsmiLoadAnnotation.Size = new System.Drawing.Size(129, 22);
            this.tsmiLoadAnnotation.Text = "Annotáció";
            this.tsmiLoadAnnotation.Click += new System.EventHandler(this.tsmiLoadAnnotation_Click);
            // 
            // tsmiLoadClasses
            // 
            this.tsmiLoadClasses.Name = "tsmiLoadClasses";
            this.tsmiLoadClasses.Size = new System.Drawing.Size(129, 22);
            this.tsmiLoadClasses.Text = "Osztályok";
            this.tsmiLoadClasses.Click += new System.EventHandler(this.tsmiLoadClasses_Click);
            // 
            // tsmiApproximate
            // 
            this.tsmiApproximate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiApproximateWithClasses,
            this.tsmiApproximateBBoxes});
            this.tsmiApproximate.Name = "tsmiApproximate";
            this.tsmiApproximate.Size = new System.Drawing.Size(94, 20);
            this.tsmiApproximate.Text = "Approximáció";
            // 
            // tsmiApproximateWithClasses
            // 
            this.tsmiApproximateWithClasses.Name = "tsmiApproximateWithClasses";
            this.tsmiApproximateWithClasses.Size = new System.Drawing.Size(197, 22);
            this.tsmiApproximateWithClasses.Text = "Osztályokkal";
            this.tsmiApproximateWithClasses.Click += new System.EventHandler(this.tsmiApproximateWithClasses_Click);
            // 
            // tsmiApproximateBBoxes
            // 
            this.tsmiApproximateBBoxes.Name = "tsmiApproximateBBoxes";
            this.tsmiApproximateBBoxes.Size = new System.Drawing.Size(197, 22);
            this.tsmiApproximateBBoxes.Text = "Csak befoglaló téglalap";
            this.tsmiApproximateBBoxes.Click += new System.EventHandler(this.tsmiApproximateBBoxes_Click);
            // 
            // ssInfo
            // 
            this.ssInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCurrentAnnotationLabel,
            this.tsslCurrentAnnotation,
            this.tsslNewAnnotationData});
            this.ssInfo.Location = new System.Drawing.Point(0, 1019);
            this.ssInfo.Name = "ssInfo";
            this.ssInfo.Size = new System.Drawing.Size(1904, 22);
            this.ssInfo.TabIndex = 4;
            this.ssInfo.Text = "statusStrip1";
            // 
            // tsslCurrentAnnotationLabel
            // 
            this.tsslCurrentAnnotationLabel.Name = "tsslCurrentAnnotationLabel";
            this.tsslCurrentAnnotationLabel.Size = new System.Drawing.Size(114, 17);
            this.tsslCurrentAnnotationLabel.Text = "Jelenlegi annotáció: ";
            // 
            // tsslCurrentAnnotation
            // 
            this.tsslCurrentAnnotation.Name = "tsslCurrentAnnotation";
            this.tsslCurrentAnnotation.Size = new System.Drawing.Size(0, 17);
            // 
            // tsslNewAnnotationData
            // 
            this.tsslNewAnnotationData.Name = "tsslNewAnnotationData";
            this.tsslNewAnnotationData.Size = new System.Drawing.Size(0, 17);
            // 
            // dgvAnnotations
            // 
            this.dgvAnnotations.AllowUserToAddRows = false;
            this.dgvAnnotations.AllowUserToResizeColumns = false;
            this.dgvAnnotations.AllowUserToResizeRows = false;
            this.dgvAnnotations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvAnnotations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnnotations.Location = new System.Drawing.Point(1652, 27);
            this.dgvAnnotations.MultiSelect = false;
            this.dgvAnnotations.Name = "dgvAnnotations";
            this.dgvAnnotations.Size = new System.Drawing.Size(240, 612);
            this.dgvAnnotations.TabIndex = 5;
            this.dgvAnnotations.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnnotations_CellValueChanged);
            this.dgvAnnotations.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvAnnotations_UserDeletedRow);
            // 
            // AnnotationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.dgvAnnotations);
            this.Controls.Add(this.ssInfo);
            this.Controls.Add(this.dgvClassNames);
            this.Controls.Add(this.pbCurrentImage);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "AnnotationView";
            this.Text = "Annotálás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AnnotationView_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AnnotationView_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassNames)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ssInfo.ResumeLayout(false);
            this.ssInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnnotations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCurrentImage;
        private System.Windows.Forms.DataGridView dgvClassNames;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadAnnotation;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadClasses;
        private System.Windows.Forms.StatusStrip ssInfo;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentAnnotationLabel;
        private System.Windows.Forms.ToolStripMenuItem tsmiApproximate;
        private System.Windows.Forms.ToolStripMenuItem tsmiApproximateWithClasses;
        private System.Windows.Forms.ToolStripMenuItem tsmiApproximateBBoxes;
        private System.Windows.Forms.DataGridView dgvAnnotations;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentAnnotation;
        private System.Windows.Forms.ToolStripStatusLabel tsslNewAnnotationData;
    }
}