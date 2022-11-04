using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;
using VisioForge.Libs.DirectShowLib;

namespace DocumentScanner
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        DocumentScanner documentScanner;
        Video_Device[] webCams;
        public Point mPoint1, mPoint2, mPoint3, mPoint4;
        int cameraDevice, mPointMoveInProgress, brightness, contrast;
        bool isImageCaptured, hasMoved, isCropOn;

        public Form1()
        {
            InitializeComponent();
            documentScanner = new DocumentScanner(null, this);
            mPointMoveInProgress = 0;
            cameraDevice = 0;
            hasMoved = false;
            isImageCaptured = false;
            isCropOn = false;
            brightness = Constants.defaultBrightness;
            contrast = Constants.defaultContrast;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DsDevice[] systemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            webCams = new Video_Device[systemCameras.Length];

            for (int cameraIndex = 0; cameraIndex < systemCameras.Length; cameraIndex++)
            {
                webCams[cameraIndex] = new Video_Device(cameraIndex, systemCameras[cameraIndex].Name,
                    systemCameras[cameraIndex].CLSID);

                cameraSelection.Items.Add(systemCameras[cameraIndex].Name);
            }

            if (cameraSelection.Items.Count > 0)
            {
                cameraSelection.SelectedIndex = 0;
            }
        }

        private void ToggleCameraControls()
        {
            stopCapture.Enabled = !stopCapture.Enabled;
            pauseCapture.Enabled = !pauseCapture.Enabled;
            startCapture.Enabled = !startCapture.Enabled;
            cameraSelection.Enabled = !cameraSelection.Enabled;
        }


        private void startCapture_Click(object sender, EventArgs e)
        {
            ToggleCameraControls();
            perspectiveButton.Visible = false;
            saveImage.Visible = false;
            printImage.Visible = false;
            filtersToolStripMenuItem.Enabled = false;
            cropImageToolStripMenuItem.Enabled = false;
            resetToOriginalImageToolStripMenuItem.Enabled = false;

            if (capture == null)
            {
                SetupCapture(cameraSelection.SelectedIndex);
            }
            capture.Set(CapProp.FrameWidth, 10000);
            capture.Set(CapProp.FrameHeight, 10000);

            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();


            if (capture.IsOpened)
            {
                captureImage.Visible = true;
            }

            mPoint1 = new Point(0, 0);
            mPoint2 = new Point(0, resultBoxView.Height);
            mPoint3 = new Point(resultBoxView.Width, resultBoxView.Height);
            mPoint4 = new Point(resultBoxView.Width, 0);
            resultBoxView.Image = null;
        }

        private void pauseCapture_Click(object sender, EventArgs e)
        {
            startCapture.Enabled = true;
            pauseCapture.Enabled = false;
            imageBoxView.Image = null;

            capture.Pause();            
        }

        private void stopCapture_Click(object sender, EventArgs e)
        {
            if (capture != null) {
                capture.Dispose();
                capture = null;
            }
            imageBoxView.Image = null;
            resultBoxView.Image = null;

            captureImage.Visible = false;
            printImage.Visible = false;
            saveImage.Visible = false;

            ToggleCameraControls();
        }

        private void cameraSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupCapture(cameraSelection.SelectedIndex);
        }
        private void SetupCapture(int cameraID)
        {
            try
            {
                cameraDevice = cameraID;

                if (capture != null)
                {
                    capture.Dispose();
                    capture = null;
                }

                capture = new VideoCapture(cameraDevice, VideoCapture.API.DShow);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void imageMenu_Click(object sender, EventArgs e)
        {
            filtersToolStripMenuItem.Enabled = false;
            cropImageToolStripMenuItem.Enabled = false;
            resetToOriginalImageToolStripMenuItem.Enabled = false;
            filterBox.Visible = false;

            mPoint1 = new Point(0, 0);
            mPoint2 = new Point(0, resultBoxView.Height);
            mPoint3 = new Point(resultBoxView.Width, resultBoxView.Height);
            mPoint4 = new Point(resultBoxView.Width, 0);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.png;*.jpg)|*.png;*.jpg;|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!startCapture.Enabled)
                {
                    ToggleCameraControls();
                }
                captureImage.Visible = false;
                printImage.Visible = false;
                saveImage.Visible = false;
                rotatePointsToolStripMenuItem.Enabled = true;

                resultBoxView.Image = null;

                if (capture != null)
                {
                    capture.Dispose();
                    capture = null;
                    resultBoxView.Image = null;
                }

                Mat img = CvInvoke.Imread(ofd.FileName);
                documentScanner = new DocumentScanner(img.ToBitmap(), this);
                documentScanner.ResultImage = img.ToBitmap();

                perspectiveButton.Visible = true;
                imageBoxView.Image = new Bitmap(documentScanner.CapturedImage, resultBoxView.Size);

                documentScanner.detectPaper();
            }
        }

        private void captureImage_Click(object sender, EventArgs e)
        {
            isImageCaptured = true;
            printImage.Visible = true;
            saveImage.Visible = true;
            perspectiveButton.Visible = true;
            captureImage.Visible = false;
            rotatePointsToolStripMenuItem.Enabled = true;
            ToggleCameraControls();

            if (capture != null)
            {
                capture.Dispose();
                capture = null;
            }            
        }

        private void printImage_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += myPrintPage;
            pd.Document = doc;

            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        private void saveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG (*.png)|*.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                documentScanner.ResultImage.Save(sfd.FileName, ImageFormat.Png);
            }
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                using (Mat cameraImg = new Mat())
                {
                    capture.Retrieve(cameraImg);

                    if (imageBoxView.Image != null) imageBoxView.Image.Dispose();
                    imageBoxView.Image = cameraImg.ToBitmap();

                    if (isImageCaptured)
                    {
                        isImageCaptured = !isImageCaptured;
                        documentScanner.CapturedImage = cameraImg.ToBitmap();
                        documentScanner.ResultImage = cameraImg.ToBitmap();
                        documentScanner.OriginalImage = cameraImg.ToBitmap();

                        capture.Dispose();
                        capture = null;

                        documentScanner.detectPaper();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void myPrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(documentScanner.ResultImage, e.MarginBounds);
        }

        private void resultBoxView_Paint(object sender, PaintEventArgs e)
        {
            if (documentScanner != null)
            {
                if (documentScanner.CapturedImage != null && hasMoved)
                {
                    hasMoved = false;
                    documentScanner.drawMovablePoints();
                }
            }
        }

        private void perspectiveButton_Click(object sender, EventArgs e)
        {
            resetToOriginalImageToolStripMenuItem.Enabled = true;
           if (isCropOn)
            {
                documentScanner.CropPhoto();
                isCropOn = false;
                perspectiveButton.Visible = false;
            }
           else
            {
                rotatePointsToolStripMenuItem.Enabled = false;
                perspectiveButton.Visible = false;
                filtersToolStripMenuItem.Enabled = true;
                cropImageToolStripMenuItem.Enabled = true;

                PointF relativePoint1 = new PointF((int)((mPoint1.X / (double)resultBoxView.Width) * documentScanner.CapturedImage.Width), 
                    (int)((mPoint1.Y / (double)resultBoxView.Height) * documentScanner.CapturedImage.Height));

                PointF relativePoint2 = new PointF((int)((mPoint2.X / (double)resultBoxView.Width) * documentScanner.CapturedImage.Width), 
                    (int)((mPoint2.Y / (double)resultBoxView.Height) * documentScanner.CapturedImage.Height));

                PointF relativePoint3 = new PointF((int)((mPoint3.X / (double)resultBoxView.Width) * documentScanner.CapturedImage.Width), 
                    (int)((mPoint3.Y / (double)resultBoxView.Height) * documentScanner.CapturedImage.Height));

                PointF relativePoint4 = new PointF((int)((mPoint4.X / (double)resultBoxView.Width) * documentScanner.CapturedImage.Width), 
                    (int)((mPoint4.Y / (double)resultBoxView.Height) * documentScanner.CapturedImage.Height));

                PointF[] points = new PointF[] { relativePoint1, relativePoint2, relativePoint3, relativePoint4 };

                double width_AD = Math.Sqrt(Math.Pow(relativePoint1.X - relativePoint4.X, 2) 
                    + Math.Pow(relativePoint1.Y - relativePoint4.Y, 2));

                double width_BC = Math.Sqrt(Math.Pow(relativePoint2.X - relativePoint3.X, 2) 
                    + Math.Pow(relativePoint2.Y - relativePoint3.Y, 2));

                int maxWidth = (int)Math.Max(width_AD, width_BC);

                double height_AB = Math.Sqrt(Math.Pow(relativePoint1.X - relativePoint2.X, 2) 
                    + Math.Pow(relativePoint1.Y - relativePoint2.Y, 2));

                double height_CD = Math.Sqrt(Math.Pow(relativePoint3.X - relativePoint4.X, 2) 
                    + Math.Pow(relativePoint3.Y - relativePoint4.Y, 2));

                int maxHeight = (int)Math.Max(height_AB, height_CD);

                PointF[] maxPoints = new PointF[] { new PointF(0, 0), new PointF(0, maxHeight),
                    new PointF(maxWidth, maxHeight), new PointF(maxWidth, 0) };

                var matrix = CvInvoke.GetPerspectiveTransform(points, maxPoints);

                Mat warpedImage = new Mat();
                CvInvoke.WarpPerspective(documentScanner.CapturedImage.ToMat(), warpedImage, matrix, new Size(maxWidth, maxHeight));
                documentScanner.ResultImage = warpedImage.ToBitmap();
                documentScanner.CapturedImage = warpedImage.ToBitmap();
                documentScanner.OriginalImage = warpedImage.ToBitmap();

                resultBoxView.Image = new Bitmap(documentScanner.ResultImage, resultBoxView.Size);

                printImage.Visible = true;
                saveImage.Visible = true;
            }
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterBox.Text = Constants.brightnessLabel;
            filterSlider.Value = brightness;
            filterSlider.Maximum = Constants.maxBrightness;
            currentSliderValue.Text = brightness.ToString();
            maxFliterValue.Text = Constants.maxBrightness.ToString();
            filterBox.Visible = true;
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterBox.Text = Constants.contrastLabel;
            filterSlider.Maximum = Constants.maxContrast;
            filterSlider.Value = contrast;                     
            currentSliderValue.Text = ((float)contrast / 100).ToString();
            maxFliterValue.Text = ((float)filterSlider.Maximum / 100).ToString();
            filterBox.Visible = true;
        }

        private void filterSlider_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (filterBox.Text == Constants.contrastLabel)
                {
                    contrast = filterSlider.Value;
                    currentSliderValue.Text = ((float)contrast / 100).ToString();
                }
                else
                {
                    brightness = filterSlider.Value;
                    currentSliderValue.Text = brightness.ToString();
                }

                documentScanner.ContrastBrightnessAdjust(contrast, brightness);

                if (adaptiveThresholdToolStripMenuItem.Checked)
                {
                    documentScanner.ResultImage = documentScanner.AdaptiveThreshold();
                }

                resultBoxView.Image = new Bitmap(documentScanner.ResultImage, resultBoxView.Size);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void adaptiveThresholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!adaptiveThresholdToolStripMenuItem.Checked)
            {
                adaptiveThresholdToolStripMenuItem.Checked = true;
                documentScanner.ResultImage = documentScanner.AdaptiveThreshold();
            }
            else
            {
                adaptiveThresholdToolStripMenuItem.Checked = false;
                documentScanner.ContrastBrightnessAdjust(contrast, brightness);
            }
            resultBoxView.Image = new Bitmap(documentScanner.ResultImage, resultBoxView.Size);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetFilter();
            documentScanner.ResultImage = new Bitmap(documentScanner.CapturedImage);
            resultBoxView.Image = new Bitmap(documentScanner.CapturedImage, resultBoxView.Size);
        }

        private void ResetFilter()
        {
            brightness = Constants.defaultBrightness;
            contrast = Constants.defaultContrast;
            adaptiveThresholdToolStripMenuItem.Checked = false;

            if (filterBox.Text == Constants.contrastLabel)
            {
                filterSlider.Value = contrast;
                currentSliderValue.Text = ((float)contrast / 100).ToString();
            }
            else
            {
                filterSlider.Value = brightness;
                currentSliderValue.Text = brightness.ToString();
            }
        }

        private void cropImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mPoint1 = new Point(0, 0);
            mPoint2 = new Point(0, resultBoxView.Height);
            mPoint3 = new Point(resultBoxView.Width, resultBoxView.Height);
            mPoint4 = new Point(resultBoxView.Width, 0);
            documentScanner.drawMovablePoints();

            isCropOn = true;
            perspectiveButton.Visible = true;
        }

        private void resetToOriginalImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetFilter();
            documentScanner.CapturedImage = new Bitmap(documentScanner.OriginalImage);
            documentScanner.ResultImage = new Bitmap(documentScanner.OriginalImage);
            resultBoxView.Image = new Bitmap(documentScanner.ResultImage, resultBoxView.Size);
        }

        private void resultBoxView_MouseDown(object sender, MouseEventArgs e)
        {
            if (Math.Abs(e.X - mPoint1.X) < Constants.handleRadius && Math.Abs(e.Y - mPoint1.Y) < Constants.handleRadius) 
                mPointMoveInProgress = 1;

            else if (Math.Abs(e.X - mPoint2.X) < Constants.handleRadius && Math.Abs(e.Y - mPoint2.Y) < Constants.handleRadius) 
                mPointMoveInProgress = 2;

            else if (Math.Abs(e.X - mPoint3.X) < Constants.handleRadius && Math.Abs(e.Y - mPoint3.Y) < Constants.handleRadius) 
                mPointMoveInProgress = 3;

            else if (Math.Abs(e.X - mPoint4.X) < Constants.handleRadius && Math.Abs(e.Y - mPoint4.Y) < Constants.handleRadius) 
                mPointMoveInProgress = 4;

            else mPointMoveInProgress = 0;
        }
        private void resultBoxView_MouseUp(object sender, MouseEventArgs e)
        {
            mPointMoveInProgress = 0;
        }
        private void resultBoxView_MouseMove(object sender, MouseEventArgs e)
        {

            if (mPointMoveInProgress == 1)
            {
                if (isCropOn)
                {
                    hasMoved = true;
                    mPoint1.X = (e.X < 0) ? 0 : (e.X > resultBoxView.Width) ? resultBoxView.Width : e.X;
                    mPoint1.Y = (e.Y < 0) ? 0 : (e.Y > resultBoxView.Height) ? resultBoxView.Height : e.Y;
                    mPoint2.X = mPoint1.X;
                    mPoint4.Y = mPoint1.Y;
                    Refresh();
                }
                else
                {
                    hasMoved = true;
                    mPoint1.X = (e.X < 0) ? 0 : (e.X > resultBoxView.Width) ? resultBoxView.Width : e.X;
                    mPoint1.Y = (e.Y < 0) ? 0 : (e.Y > resultBoxView.Height) ? resultBoxView.Height : e.Y;
                    Refresh();
                }
            }
            else if (mPointMoveInProgress == 2)
            {
                if (isCropOn)
                {
                    hasMoved = true;
                    mPoint2.X = (e.X < 0) ? 0 : (e.X > resultBoxView.Width) ? resultBoxView.Width : e.X;
                    mPoint2.Y = (e.Y < 0) ? 0 : (e.Y > resultBoxView.Height) ? resultBoxView.Height : e.Y;
                    mPoint1.X = mPoint2.X;
                    mPoint3.Y = mPoint2.Y;
                    Refresh();
                }
                else
                {
                    hasMoved = true;
                    mPoint2.X = (e.X < 0) ? 0 : (e.X > resultBoxView.Width) ? resultBoxView.Width : e.X;
                    mPoint2.Y = (e.Y < 0) ? 0 : (e.Y > resultBoxView.Height) ? resultBoxView.Height : e.Y;
                    Refresh();
                }
            }
            else if (mPointMoveInProgress == 3)
            {
                if (isCropOn)
                {
                    hasMoved = true;
                    mPoint3.X = (e.X < 0) ? 0 : (e.X > resultBoxView.Width) ? resultBoxView.Width : e.X;
                    mPoint3.Y = (e.Y < 0) ? 0 : (e.Y > resultBoxView.Height) ? resultBoxView.Height : e.Y;
                    mPoint4.X = mPoint3.X;
                    mPoint2.Y = mPoint3.Y;
                    Refresh();
                }
                else
                {
                    hasMoved = true;
                    mPoint3.X = (e.X < 0) ? 0 : (e.X > resultBoxView.Width) ? resultBoxView.Width : e.X;
                    mPoint3.Y = (e.Y < 0) ? 0 : (e.Y > resultBoxView.Height) ? resultBoxView.Height : e.Y;
                    Refresh();
                }
            }
            else if (mPointMoveInProgress == 4)
            {
                if (isCropOn)
                {
                    hasMoved = true;
                    mPoint4.X = (e.X < 0) ? 0 : (e.X > resultBoxView.Width) ? resultBoxView.Width : e.X;
                    mPoint4.Y = (e.Y < 0) ? 0 : (e.Y > resultBoxView.Height) ? resultBoxView.Height : e.Y;
                    mPoint3.X = mPoint4.X;
                    mPoint1.Y = mPoint4.Y;
                    Refresh();
                }
                else
                {
                    hasMoved = true;
                    mPoint4.X = (e.X < 0) ? 0 : (e.X > resultBoxView.Width) ? resultBoxView.Width : e.X;
                    mPoint4.Y = (e.Y < 0) ? 0 : (e.Y > resultBoxView.Height) ? resultBoxView.Height : e.Y;
                    Refresh();
                }
            }
            else
            {
                if (Math.Abs(e.X - mPoint1.X) < Constants.handleRadius && Math.Abs(e.Y - mPoint1.Y) < Constants.handleRadius)
                    Cursor.Current = Cursors.Hand;

                else if (Math.Abs(e.X - mPoint2.X) < Constants.handleRadius && Math.Abs(e.Y - mPoint2.Y) < Constants.handleRadius)
                    Cursor.Current = Cursors.Hand;

                else if (Math.Abs(e.X - mPoint3.X) < Constants.handleRadius && Math.Abs(e.Y - mPoint3.Y) < Constants.handleRadius)
                    Cursor.Current = Cursors.Hand;

                else if (Math.Abs(e.X - mPoint4.X) < Constants.handleRadius && Math.Abs(e.Y - mPoint4.Y) < Constants.handleRadius)
                    Cursor.Current = Cursors.Hand;

                else Cursor.Current = Cursors.Default;
            }
        }

        private void rotatePointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotatePoints();
            documentScanner.drawMovablePoints();
        }       

        private void RotatePoints()
        {
            Point tempPoint = new Point(mPoint1.X, mPoint1.Y);
            mPoint1 = new Point(mPoint4.X, mPoint4.Y);
            mPoint4 = new Point(mPoint3.X, mPoint3.Y);
            mPoint3 = new Point(mPoint2.X, mPoint2.Y);
            mPoint2 = new Point(tempPoint.X, tempPoint.Y);
        }
    }
}
