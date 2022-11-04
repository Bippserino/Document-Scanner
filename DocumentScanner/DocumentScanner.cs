using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentScanner
{
    class DocumentScanner
    {
        public Bitmap CapturedImage { get; set; }
        public Bitmap ResultImage { get; set; }
        public Bitmap OriginalImage { get; set; }

        public Form1 MyForm { get; private set; }
        public DocumentScanner(){}

        public DocumentScanner(Bitmap capturedImage, Form1 form)
        {
            CapturedImage = capturedImage;
            MyForm = form;
        }
        public void detectPaper()
        {
            Mat sourceImage = CapturedImage.ToMat();
            Mat hierarchy = new Mat();
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            
            double maxArea = 0.0;

            DetectEdges(sourceImage, ref contours, ref hierarchy);
            VectorOfPoint paperVector = FindBiggestContour(contours, ref maxArea);

            if (maxArea != 0.0)
            {
                MyForm.mPoint1.X = (int)((paperVector[0].X / (double)CapturedImage.Width)
                    * MyForm.resultBoxView.Width);
                MyForm.mPoint1.Y = (int)((paperVector[0].Y / (double)CapturedImage.Height)
                    * MyForm.resultBoxView.Height);

                MyForm.mPoint2.X = (int)((paperVector[1].X / (double)CapturedImage.Width) 
                    * MyForm.resultBoxView.Width);
                MyForm.mPoint2.Y = (int)((paperVector[1].Y / (double)CapturedImage.Height) 
                    * MyForm.resultBoxView.Height);

                MyForm.mPoint3.X = (int)((paperVector[2].X / (double)CapturedImage.Width) 
                    * MyForm.resultBoxView.Width);
                MyForm.mPoint3.Y = (int)((paperVector[2].Y / (double)CapturedImage.Height) 
                    * MyForm.resultBoxView.Height);

                MyForm.mPoint4.X = (int)((paperVector[3].X / (double)CapturedImage.Width)
                    * MyForm.resultBoxView.Width);
                MyForm.mPoint4.Y = (int)((paperVector[3].Y / (double)CapturedImage.Height) 
                    * MyForm.resultBoxView.Height);
            }
            drawMovablePoints();
        }

        private void DetectEdges(Mat img, ref VectorOfVectorOfPoint contours, ref Mat hierarchy)
        {
            Mat gaussianBlurredImage = new Mat();
            Mat cannyImage = new Mat();
            Mat claheImage = new Mat();

            Image<Gray, Byte> grayscaleImage = img.ToImage<Gray, Byte>()
                .ThresholdBinary(new Gray(100), new Gray(255));

            CvInvoke.CLAHE(grayscaleImage, 5, new Size(16, 16), claheImage);

            CvInvoke.GaussianBlur(claheImage, gaussianBlurredImage, new Size(5, 5), 1);    
            
            CvInvoke.Canny(gaussianBlurredImage, cannyImage,
                Constants.lowerThreshold, Constants.upperThreshold);

            CvInvoke.FindContours(cannyImage, contours, hierarchy, RetrType.External,
                ChainApproxMethod.ChainApproxSimple);
            CvInvoke.BitwiseNot(cannyImage, cannyImage);
            
            MyForm.imageBoxView.Image = cannyImage.ToBitmap();
        }

        private VectorOfPoint FindBiggestContour(VectorOfVectorOfPoint contours, ref double maxArea)
        {
            VectorOfPoint biggest = new VectorOfPoint();
            
            for (int contourIndex = 0; contourIndex < contours.Size; contourIndex++)
            {
                double area = CvInvoke.ContourArea(contours[contourIndex]);
                if (area > 5000)
                {
                    double perimiter = CvInvoke.ArcLength(contours[contourIndex], true);
                    VectorOfPoint approximate = new VectorOfPoint();
                    CvInvoke.ApproxPolyDP(contours[contourIndex], approximate, 0.02 * perimiter, true);

                    if (area > maxArea && approximate.Size == 4)
                    {
                        if (Utility.IsRectangle(approximate))
                        {
                            biggest = approximate;
                            maxArea = area;
                        }
                    }
                }
            }
            return biggest;
        }

        public void drawMovablePoints()
        {
            Pen blackPen = new Pen(Color.Black, 1);
            Bitmap resizedImage = new Bitmap(ResultImage, new Size(MyForm.resultBoxView.Width, MyForm.resultBoxView.Height));
 
            using (var graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawLine(blackPen, MyForm.mPoint4, MyForm.mPoint1);
                graphics.DrawLine(blackPen, MyForm.mPoint1, MyForm.mPoint2);
                graphics.DrawLine(blackPen, MyForm.mPoint2, MyForm.mPoint3);
                graphics.DrawLine(blackPen, MyForm.mPoint3, MyForm.mPoint4);

                Rectangle rectangle;
                rectangle = new Rectangle(MyForm.mPoint1.X - Constants.handleRadius, MyForm.mPoint1.Y - Constants.handleRadius,
                    Constants.handleRadius * 2, Constants.handleRadius * 2);

                graphics.FillRectangle(Brushes.Red, rectangle);
                graphics.DrawRectangle(Pens.Black, rectangle);

                rectangle = new Rectangle(MyForm.mPoint2.X - Constants.handleRadius, MyForm.mPoint2.Y - Constants.handleRadius,
                    Constants.handleRadius * 2, Constants.handleRadius * 2);

                graphics.FillRectangle(Brushes.White, rectangle);
                graphics.DrawRectangle(Pens.Black, rectangle);

                rectangle = new Rectangle(MyForm.mPoint3.X - Constants.handleRadius, MyForm.mPoint3.Y - Constants.handleRadius,
                    Constants.handleRadius * 2, Constants.handleRadius * 2);

                graphics.FillRectangle(Brushes.White, rectangle);
                graphics.DrawRectangle(Pens.Yellow, rectangle);

                rectangle = new Rectangle(MyForm.mPoint4.X - Constants.handleRadius, MyForm.mPoint4.Y - Constants.handleRadius,
                    Constants.handleRadius * 2, Constants.handleRadius * 2);

                graphics.FillRectangle(Brushes.White, rectangle);
                graphics.DrawRectangle(Pens.Green, rectangle);
            }
            MyForm.resultBoxView.Image = resizedImage;
        }

        public Bitmap AdaptiveThreshold()
        {
            Mat img = new Mat();
            CvInvoke.CvtColor(ResultImage.ToImage<Bgr, byte>(), img, ColorConversion.Bgr2Gray);
            CvInvoke.AdaptiveThreshold(img, img, 255, AdaptiveThresholdType.GaussianC, ThresholdType.BinaryInv, 7, 2);
            CvInvoke.BitwiseNot(img, img);
            CvInvoke.MedianBlur(img, img, 5);

            return img.ToBitmap();
        }

        public void ContrastBrightnessAdjust(int contrast, int brightness)
        {
            try
            {
                using (Image<Bgr, byte> adjustedImage = (CapturedImage.ToImage<Bgr,
                    byte>().Mul((float)contrast / 100) + brightness))

                {
                    ResultImage = new Bitmap(adjustedImage.ToBitmap());
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CropPhoto()
        {
            Point relativePoint1 = new Point((int)((MyForm.mPoint1.X / (double)MyForm.resultBoxView.Width) * CapturedImage.Width), 
                (int)((MyForm.mPoint1.Y / (double)MyForm.resultBoxView.Height) * CapturedImage.Height));

            Point relativePoint3 = new Point((int)((MyForm.mPoint3.X / (double)MyForm.resultBoxView.Width) * CapturedImage.Width), 
                (int)((MyForm.mPoint3.Y / (double)MyForm.resultBoxView.Height) * CapturedImage.Height));

            Rectangle rectangle = new Rectangle();
            rectangle.X = relativePoint1.X;
            rectangle.Y = relativePoint1.Y;
            rectangle.Width = relativePoint3.X - relativePoint1.X;
            rectangle.Height = relativePoint3.Y - relativePoint1.Y;

            Image<Bgr, byte> tempImg = CapturedImage.ToImage<Bgr, byte>();
            tempImg.ROI = rectangle;
            CapturedImage = tempImg.ToBitmap();
            
            tempImg = ResultImage.ToImage<Bgr, byte>();
            tempImg.ROI = rectangle;
            ResultImage = tempImg.ToBitmap();

            MyForm.resultBoxView.Image = new Bitmap(ResultImage, new Size(MyForm.resultBoxView.Width, MyForm.resultBoxView.Height));
        }
    }
}
