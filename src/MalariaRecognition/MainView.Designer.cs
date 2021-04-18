
namespace MalariaRecognition
{
    partial class MainView
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
            this.btnAnnotations = new System.Windows.Forms.Button();
            this.btnClassification = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAnnotations
            // 
            this.btnAnnotations.Location = new System.Drawing.Point(12, 57);
            this.btnAnnotations.Name = "btnAnnotations";
            this.btnAnnotations.Size = new System.Drawing.Size(200, 23);
            this.btnAnnotations.TabIndex = 0;
            this.btnAnnotations.Text = "Annotálás";
            this.btnAnnotations.UseVisualStyleBackColor = true;
            this.btnAnnotations.Click += new System.EventHandler(this.btnAnnotations_Click);
            // 
            // btnClassification
            // 
            this.btnClassification.Location = new System.Drawing.Point(12, 86);
            this.btnClassification.Name = "btnClassification";
            this.btnClassification.Size = new System.Drawing.Size(200, 23);
            this.btnClassification.TabIndex = 1;
            this.btnClassification.Text = "Osztályzás";
            this.btnClassification.UseVisualStyleBackColor = true;
            this.btnClassification.Click += new System.EventHandler(this.btnClassification_Click);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 121);
            this.Controls.Add(this.btnClassification);
            this.Controls.Add(this.btnAnnotations);
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menü";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAnnotations;
        private System.Windows.Forms.Button btnClassification;
    }
}