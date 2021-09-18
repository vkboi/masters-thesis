using MalariaRecognition.BackendLogic;
using MalariaRecognition.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MalariaRecognition.View
{
    public partial class ClassificationView : Form
    {
        private string imagePath;
        private ClassificationManager manager;
        private Image currentImage;
        private Annotation annotation;

        public ClassificationView()
        {
            InitializeComponent();

            manager = new ClassificationManager();
        }

        private void btnClassify_Click(object sender, EventArgs e)
        {
            if (pbCurrentImage.Image == null)
            {
                if (MessageBox.Show("Még nem választottál képet! Megteszed most?", "Hiányzó kép...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    SelectImage();
                }
                
                if (pbCurrentImage == null)
                {
                    return;
                }
            }

            Application.UseWaitCursor = true;
            btnClassify.Enabled = false;
            try
            {
                List<Prediction> predictions = manager.GetPredictions(imagePath, annotation ?? FileCommon.LoadAnnotation(out string _));

                Bitmap bitmap = new Bitmap(currentImage);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    foreach (Prediction pred in predictions)
                    {
                        using (Pen pen = new Pen(pred.Category?.GetColor() ?? Color.Black, 5))
                        {
                            graphics.DrawRectangle(pen, new Rectangle(pred.X, pred.Y, pred.Width, pred.Height));
                        }
                    }
                }

                pbCurrentImage.Image = bitmap;
                Application.UseWaitCursor = false;
            }
            catch (Exception ex)
            {
                Application.UseWaitCursor = false;
                MessageBox.Show(ex.Message ,"Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnClassify.Enabled = true;
        }

        private void tsmiSelectImage_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            btnClassify.Enabled = false;
            SelectImage();
            Application.UseWaitCursor = false;
            btnClassify.Enabled = true;
        }

        private void tsmiSelectAnnotation_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            btnClassify.Enabled = false;

            try
            {
                annotation = FileCommon.LoadAnnotation(out string annotationPath);
                if (!string.IsNullOrEmpty(annotationPath))
                {
                    lblCurrentAnnotation.Text = $"Jelenlegi annotáció: {annotationPath}";
                }

                Application.UseWaitCursor = false;
            }
            catch (Exception ex)
            {
                Application.UseWaitCursor = false;
                MessageBox.Show(ex.Message, "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnClassify.Enabled = true;
        }

        private void SelectImage()
        {
            Image plainImg = FileCommon.LoadImage();
            if (plainImg != null)
            {
                currentImage = plainImg;
                pbCurrentImage.Image = currentImage;
                imagePath = FileCommon.LastOpenedFilePath;
                lblCurrentImage.Text = $"Jelenlegi kép: {imagePath}";
            }
        }
    }
}
