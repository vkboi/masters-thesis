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
    public partial class AnnotationEditView : Form
    {

        public BoundingBox OriginalBoundingBox { get; set; }

        public AnnotationEditView()
        {
            InitializeComponent();
        }

        #region Input sanitization
        private void DiscardNonNumeric(KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCoordinateX_KeyPress(object sender, KeyPressEventArgs e)
        {
            DiscardNonNumeric(e);
        }

        private void txtCoordinateY_KeyPress(object sender, KeyPressEventArgs e)
        {
            DiscardNonNumeric(e);
        }

        private void txtWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            DiscardNonNumeric(e);
        }

        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            DiscardNonNumeric(e);
        }
        #endregion

        private void AnnotationEditView_Load(object sender, EventArgs e)
        {
            if (OriginalBoundingBox == null)
            {
                Close();
            }

            txtCoordinateX.Text = OriginalBoundingBox.X.ToString();
            txtCoordinateY.Text = OriginalBoundingBox.Y.ToString();
            txtWidth.Text = OriginalBoundingBox.Width.ToString();
            txtHeight.Text = OriginalBoundingBox.Height.ToString();

            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategory.DataSource = Enum.GetValues(typeof(Category));//.Cast<List<Category>>().Select(x => new { Name = x.ToString(), Value = x }).ToList();
            //cbCategory.DisplayMember = "Name";
            //cbCategory.ValueMember = "Value";
            if (OriginalBoundingBox.Category.HasValue)
            {
                cbCategory.SelectedIndex = (int)OriginalBoundingBox.Category;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OriginalBoundingBox.X = Convert.ToInt32(txtCoordinateX.Text);
            OriginalBoundingBox.Y = Convert.ToInt32(txtCoordinateY.Text);
            OriginalBoundingBox.Width = Convert.ToInt32(txtWidth.Text);
            OriginalBoundingBox.Height = Convert.ToInt32(txtHeight.Text);
            OriginalBoundingBox.Category = (Category)cbCategory.SelectedIndex;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
