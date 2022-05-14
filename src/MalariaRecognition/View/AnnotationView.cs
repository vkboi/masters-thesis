using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MalariaRecognition.BackendLogic;
using MalariaRecognition.Model;

namespace MalariaRecognition.View
{
    public partial class AnnotationView : Form
    {
        private Color CrosshairColor => Color.DarkGray;
        private Color HighlightColor => Color.White;


        private RegionProposerManager regionProposerManager = new RegionProposerManager();
        private ClassificationManager classificationManager = new ClassificationManager();

        private string annotationPath;
        private string imagePath;

        private BindingList<BoundingBox> boundingBoxes = new BindingList<BoundingBox>();
        private BindingList<AnnotationClass> classes;

        private Annotation annotation;
        private Image plainImg;
        private Image bboxImg;
        
        private bool isDrawingCrosshair;
        private bool isDrawingHighlight;

        private Point bboxStart;

        public AnnotationView()
        {
            InitializeComponent();
        }

        private void AnnotationView_Load(object sender, EventArgs e)
        {
            dgvAnnotations.DataSource = boundingBoxes;
            dgvAnnotations.Columns["Row"].Visible = false;
            dgvAnnotations.Columns["ModelInput"].Visible = false;

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
                imagePath = FileCommon.LastOpenedFilePath;
                boundingBoxes.Clear();
                annotationPath = null;
            }
        }

        private void tsmiLoadAnnotation_Click(object sender, EventArgs e)
        {
            annotation = FileCommon.LoadAnnotation(out annotationPath);

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
            boundingBoxes.Clear();

            foreach (Prediction prediction in classificationManager.GetPredictions(imagePath, annotation, true))
            {
                boundingBoxes.Add(prediction);
            }

            RenderBoundingBoxes();
        }

        private void tsmiApproximateBBoxes_Click(object sender, EventArgs e)
        {
            boundingBoxes.Clear();

            foreach (BoundingBox bbox in regionProposerManager.GetBoundingBoxProposals(imagePath))
            {
                boundingBoxes.Add(bbox);
            }

            RenderBoundingBoxes();
        }

        private void pbCurrentImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (isDrawingCrosshair)
            {
                bboxStart = e.Location;
                isDrawingHighlight = true;
            }
        }

        private void pbCurrentImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawingCrosshair)
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

                isDrawingCrosshair = false;
                isDrawingHighlight = false;
            }
        }

        private void AnnotationView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                isDrawingCrosshair = false;
                Cursor.Current = Cursors.Arrow;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (dgvAnnotations.SelectedRows.Count > 0)
                {
                    dgvAnnotations.Rows.Remove(dgvAnnotations.SelectedRows[0]);
                    RenderBoundingBoxes();
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
                    isDrawingCrosshair = !isDrawingCrosshair;
                }
            }
        }

        private void pbCurrentImage_MouseMove(object sender, MouseEventArgs e)
        {
            RenderImage();
            if (isDrawingCrosshair)
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
                    using (Pen pen = new Pen(bbox.Category?.GetColor(bbox.IsSelected) ?? Color.Black, 5))
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
            if (isDrawingCrosshair)
            {
                Bitmap bitmap = new Bitmap(bboxImg);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                using (Pen pen = new Pen(CrosshairColor, 1))
                using (Pen highlightPen = new Pen(HighlightColor, 3))
                {
                    int xOffset = pbCurrentImage.Location.X;
                    int yOffset = pbCurrentImage.Location.Y + menuStrip.Height;

                    Point cursorPos = new Point(Cursor.Position.X - xOffset, Cursor.Position.Y - yOffset);

                    int width = Math.Abs(bboxStart.X - cursorPos.X);
                    int height = Math.Abs(bboxStart.Y - cursorPos.Y);

                    if (isDrawingHighlight)
                    {
                        graphics.DrawRectangle(highlightPen, Math.Min(bboxStart.X, cursorPos.X), Math.Min(bboxStart.Y, cursorPos.Y), width, height);
                    }

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

        private void dgvClassNames_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvClassNames.Columns[e.ColumnIndex].Name == "Color")
            {
                e.CellStyle.BackColor = ((Category)Enum.Parse(typeof(Category), (string)dgvClassNames.Rows[e.RowIndex].Cells["Name"].Value)).GetColor();
            }
        }

        private void dgvAnnotations_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            BoundingBox bbox = boundingBoxes[e.RowIndex];
            DataGridViewRow modifiedRow = dgvAnnotations.Rows[e.RowIndex];
            bbox.X = (int)modifiedRow.Cells["X"].Value;
            bbox.Y = (int)modifiedRow.Cells["Y"].Value;
            bbox.Width = (int)modifiedRow.Cells["Width"].Value;
            bbox.Height = (int)modifiedRow.Cells["Height"].Value;
            bbox.Category = (Category)modifiedRow.Cells["Category"].Value;

            RenderBoundingBoxes();
        }

        private void EditBoundingBox(BoundingBox box)
        {
            if (box != null)
            {
                box.IsSelected = true;
                RenderBoundingBoxes();

                AnnotationEditView aev = new AnnotationEditView
                {
                    OriginalBoundingBox = box
                };

                aev.ShowDialog();

                box.IsSelected = false;
                RenderBoundingBoxes();
            }
        }

        private void pbCurrentImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BoundingBox selected = boundingBoxes.Where(x => x.IsPointOnFrame(e.Location)).FirstOrDefault();

            EditBoundingBox(selected);
        }

        private void dgvAnnotations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                EditBoundingBox(boundingBoxes[e.RowIndex]);
            }
        }
    }
}
