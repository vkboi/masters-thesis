using System;
using System.Windows.Forms;
using MalariaRecognition.View;

namespace MalariaRecognition
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void btnAnnotations_Click(object sender, EventArgs e)
        {
            Hide();
            AnnotationView annotationView = new AnnotationView();
            annotationView.FormClosing += ShowOnClose;
            annotationView.Show();
        }

        private void btnClassification_Click(object sender, EventArgs e)
        {
            Hide();
            ClassificationView classificationView = new ClassificationView();
            classificationView.FormClosing += ShowOnClose;
            classificationView.Show();
        }

        private void ShowOnClose(object sender, EventArgs e)
        {
            Show();
        }
    }
}
