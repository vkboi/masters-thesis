using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MalariaRecognition.Controller;
using MalariaRecognition.Model;

namespace MalariaRecognition.View
{
    public partial class AnnotationView : Form
    {

        private AnnotationController controller = new AnnotationController();

        private string annotationPath;

        private BindingList<BoundingBox> boundingBoxes = new BindingList<BoundingBox>();
        private BindingList<AnnotationClass> classes;

        private Image plainImg;
        private Image bboxImg;
        
        private bool isDrawingBox;

        private Point bboxStart;

        public AnnotationView()
        {
            InitializeComponent();
        }

        private void AnnotationView_Load(object sender, EventArgs e)
        {
            dgvAnnotations.DataSource = boundingBoxes;
            dgvAnnotations.Columns["ToRow"].Visible = false;

            classes = new BindingList<AnnotationClass>(Enum.GetNames(typeof(Category)).Select(x => new AnnotationClass { Name = x }).ToList());

            dgvClassNames.DataSource = classes;
            dgvClassNames.Columns["Name"].HeaderText = "Név";
            dgvClassNames.Columns["Color"].HeaderText = "Szín";
            dgvClassNames.Columns["Color"].Width = 40;
            dgvClassNames.ClearSelection();
            dgvClassNames.Rows[0].Selected = true;
        }

        private void tsmiLoadImage_Click(object sender, EventArgs e)
        {
            plainImg = FileCommon.LoadImage();
            if (plainImg != null)
            {
                pbCurrentImage.Image = plainImg;
                bboxImg = (Image)plainImg.Clone();
                annotationPath = null;
            }
        }

        private void tsmiLoadAnnotation_Click(object sender, EventArgs e)
        {
            Annotation annotation = FileCommon.LoadAnnotation(out annotationPath);

            if (annotation != null && annotation.BoundingBoxes.Count > 0)
            {
                boundingBoxes.Clear();
                foreach (BoundingBox bbox in annotation.BoundingBoxes)
                {
                    boundingBoxes.Add(bbox);
                }
            }

            RenderBoundingBoxes();
        }

        private void tsmiLoadClasses_Click(object sender, EventArgs e)
        {
            BindingList<AnnotationClass> classList = new BindingList<AnnotationClass>(FileCommon.LoadClasses().Select(x => new AnnotationClass{ Name = x }).ToList());

            if (classList != null && classList.Count > 0)
            {
                classes = classList;
            }

        }

        private void tsmiApproximateWithClasses_Click(object sender, EventArgs e)
        {

        }

        private void tsmiApproximateBBoxes_Click(object sender, EventArgs e)
        {

        }

        private void pbCurrentImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (isDrawingBox)
            {
                bboxStart = e.Location;
            }
        }

        private void pbCurrentImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawingBox)
            {
                int width = Math.Abs(bboxStart.X - e.Location.X);
                int height = Math.Abs(bboxStart.Y - e.Location.Y);
                
                if (width > 20 && height > 20)
                {
                    boundingBoxes.Add(new BoundingBox
                    {
                        X = Math.Min(bboxStart.X, e.Location.X),
                        Y = Math.Min(bboxStart.Y, e.Location.Y),
                        Width = width,
                        Height = height,
                        Category = (Category)Enum.Parse(typeof(Category), (string)dgvClassNames.SelectedCells[0].Value)
                    });
                    RenderBoundingBoxes();
                }

                isDrawingBox = false;
            }
        }

        private void AnnotationView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                isDrawingBox = false;
                Cursor.Current = Cursors.Arrow;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (dgvAnnotations.SelectedRows.Count > 0)
                {
                    dgvAnnotations.Rows.Remove(dgvAnnotations.SelectedRows[0]);
                }
            }
            else if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    if (string.IsNullOrEmpty(annotationPath) || e.Shift)
                    {
                        FileCommon.SaveAnnotation(new Annotation
                        {
                            BoundingBoxes = ((BindingList<BoundingBox>)dgvAnnotations.DataSource).Select(x => x).ToList()
                        }, ref annotationPath);
                    }
                    else
                    {
                        FileCommon.SaveAnnotationSilent(new Annotation
                        {
                            BoundingBoxes = ((BindingList<BoundingBox>)dgvAnnotations.DataSource).Select(x => x).ToList()
                        }, annotationPath);
                    }
                }
                else if (e.KeyCode == Keys.N)
                {
                    isDrawingBox = !isDrawingBox;
                }
            }
        }

        private void pbCurrentImage_MouseMove(object sender, MouseEventArgs e)
        {
            RenderImage();
            if (isDrawingBox)
            {
                Cursor.Current = Cursors.Cross;
                tsslNewAnnotationData.Text = $"X: {Math.Min(bboxStart.X, e.Location.X)}, Y: {Math.Min(bboxStart.Y, e.Location.Y)}, W: {Math.Abs(bboxStart.X - e.Location.X)}, H: {Math.Abs(bboxStart.Y - e.Location.Y)}";
            }
            else
            {
                Cursor.Current = Cursors.Arrow;
            }
        }

        private void RenderBoundingBoxes()
        {
            Bitmap bitmap = new Bitmap(plainImg);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                foreach (BoundingBox bbox in boundingBoxes)
                {
                    using (Pen pen = new Pen(bbox.Category.GetColor(), 5))
                    {
                        graphics.DrawRectangle(pen, new Rectangle(bbox.X, bbox.Y, bbox.Width, bbox.Height));
                    }
                }
            }
            bboxImg = bitmap;
            pbCurrentImage.Image = bitmap;
        }
           
        private void RenderImage()
        {
            if (isDrawingBox)
            {
                Bitmap bitmap = new Bitmap(bboxImg);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                using (Pen pen = new Pen(Color.DarkGray, 1))
                {
                    int xOffset = pbCurrentImage.Location.X;
                    int yOffset = pbCurrentImage.Location.Y + menuStrip.Height;

                    Point cursorPos = new Point(Cursor.Position.X - xOffset, Cursor.Position.Y - yOffset);

                    graphics.DrawLine(pen, cursorPos, new Point(0, Cursor.Position.Y - yOffset));
                    graphics.DrawLine(pen, cursorPos, new Point(pbCurrentImage.Width, Cursor.Position.Y - yOffset));
                    graphics.DrawLine(pen, cursorPos, new Point(Cursor.Position.X - xOffset, 0));
                    graphics.DrawLine(pen, cursorPos, new Point(Cursor.Position.X - xOffset, pbCurrentImage.Height));
                }
                pbCurrentImage.Image = bitmap;
            }
            else
            {
                pbCurrentImage.Image = bboxImg;
            }
        }

        private void dgvAnnotations_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            boundingBoxes.RemoveAt(e.RowIndex);
        }

        private void dgvClassNames_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvClassNames.Columns[e.ColumnIndex].Name == "Color")
            {
                e.CellStyle.BackColor = ((Category)Enum.Parse(typeof(Category), (string)dgvClassNames.Rows[e.RowIndex].Cells["Name"].Value)).GetColor();
            }
        }
    }
}
