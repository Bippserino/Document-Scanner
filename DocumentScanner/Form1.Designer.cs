
namespace DocumentScanner
{
    partial class Form1
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
            this.imageBoxView = new System.Windows.Forms.PictureBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.imageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.startCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.stopCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaptiveThresholdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cropImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToOriginalImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotatePointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultBoxView = new System.Windows.Forms.PictureBox();
            this.captureImage = new System.Windows.Forms.Button();
            this.printMenu = new System.Windows.Forms.PageSetupDialog();
            this.printImage = new System.Windows.Forms.Button();
            this.saveImage = new System.Windows.Forms.Button();
            this.cameraSelection = new System.Windows.Forms.ComboBox();
            this.perspectiveButton = new System.Windows.Forms.Button();
            this.filterBox = new System.Windows.Forms.GroupBox();
            this.currentSliderValue = new System.Windows.Forms.Label();
            this.maxFliterValue = new System.Windows.Forms.Label();
            this.minFilterValue = new System.Windows.Forms.Label();
            this.filterSlider = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxView)).BeginInit();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultBoxView)).BeginInit();
            this.filterBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxView
            // 
            this.imageBoxView.Location = new System.Drawing.Point(0, 28);
            this.imageBoxView.Name = "imageBoxView";
            this.imageBoxView.Size = new System.Drawing.Size(466, 262);
            this.imageBoxView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBoxView.TabIndex = 0;
            this.imageBoxView.TabStop = false;
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageMenu,
            this.cameraMenu,
            this.filtersToolStripMenuItem,
            this.cropImageToolStripMenuItem,
            this.resetToOriginalImageToolStripMenuItem,
            this.rotatePointsToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1116, 28);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // imageMenu
            // 
            this.imageMenu.Name = "imageMenu";
            this.imageMenu.Size = new System.Drawing.Size(65, 24);
            this.imageMenu.Text = "Image";
            this.imageMenu.Click += new System.EventHandler(this.imageMenu_Click);
            // 
            // cameraMenu
            // 
            this.cameraMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startCapture,
            this.stopCapture,
            this.pauseCapture});
            this.cameraMenu.Name = "cameraMenu";
            this.cameraMenu.Size = new System.Drawing.Size(74, 24);
            this.cameraMenu.Text = "Camera";
            // 
            // startCapture
            // 
            this.startCapture.Name = "startCapture";
            this.startCapture.Size = new System.Drawing.Size(130, 26);
            this.startCapture.Text = "Start";
            this.startCapture.Click += new System.EventHandler(this.startCapture_Click);
            // 
            // stopCapture
            // 
            this.stopCapture.Enabled = false;
            this.stopCapture.Name = "stopCapture";
            this.stopCapture.Size = new System.Drawing.Size(130, 26);
            this.stopCapture.Text = "Stop";
            this.stopCapture.Click += new System.EventHandler(this.stopCapture_Click);
            // 
            // pauseCapture
            // 
            this.pauseCapture.Enabled = false;
            this.pauseCapture.Name = "pauseCapture";
            this.pauseCapture.Size = new System.Drawing.Size(130, 26);
            this.pauseCapture.Text = "Pause";
            this.pauseCapture.Click += new System.EventHandler(this.pauseCapture_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brightnessToolStripMenuItem,
            this.contrastToolStripMenuItem,
            this.adaptiveThresholdToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.filtersToolStripMenuItem.Enabled = false;
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // brightnessToolStripMenuItem
            // 
            this.brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            this.brightnessToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.brightnessToolStripMenuItem.Text = "Brightness";
            this.brightnessToolStripMenuItem.Click += new System.EventHandler(this.brightnessToolStripMenuItem_Click);
            // 
            // contrastToolStripMenuItem
            // 
            this.contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            this.contrastToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.contrastToolStripMenuItem.Text = "Contrast";
            this.contrastToolStripMenuItem.Click += new System.EventHandler(this.contrastToolStripMenuItem_Click);
            // 
            // adaptiveThresholdToolStripMenuItem
            // 
            this.adaptiveThresholdToolStripMenuItem.Name = "adaptiveThresholdToolStripMenuItem";
            this.adaptiveThresholdToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.adaptiveThresholdToolStripMenuItem.Text = "Adaptive Threshold";
            this.adaptiveThresholdToolStripMenuItem.Click += new System.EventHandler(this.adaptiveThresholdToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.resetToolStripMenuItem.Text = "Reset Filters";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // cropImageToolStripMenuItem
            // 
            this.cropImageToolStripMenuItem.Enabled = false;
            this.cropImageToolStripMenuItem.Name = "cropImageToolStripMenuItem";
            this.cropImageToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.cropImageToolStripMenuItem.Text = "Crop Image";
            this.cropImageToolStripMenuItem.Click += new System.EventHandler(this.cropImageToolStripMenuItem_Click);
            // 
            // resetToOriginalImageToolStripMenuItem
            // 
            this.resetToOriginalImageToolStripMenuItem.Enabled = false;
            this.resetToOriginalImageToolStripMenuItem.Name = "resetToOriginalImageToolStripMenuItem";
            this.resetToOriginalImageToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.resetToOriginalImageToolStripMenuItem.Text = "Reset To Original Image";
            this.resetToOriginalImageToolStripMenuItem.Click += new System.EventHandler(this.resetToOriginalImageToolStripMenuItem_Click);
            // 
            // rotatePointsToolStripMenuItem
            // 
            this.rotatePointsToolStripMenuItem.Enabled = false;
            this.rotatePointsToolStripMenuItem.Name = "rotatePointsToolStripMenuItem";
            this.rotatePointsToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.rotatePointsToolStripMenuItem.Text = "Rotate Points";
            this.rotatePointsToolStripMenuItem.Click += new System.EventHandler(this.rotatePointsToolStripMenuItem_Click);
            // 
            // resultBoxView
            // 
            this.resultBoxView.Location = new System.Drawing.Point(650, 28);
            this.resultBoxView.Name = "resultBoxView";
            this.resultBoxView.Size = new System.Drawing.Size(465, 262);
            this.resultBoxView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.resultBoxView.TabIndex = 2;
            this.resultBoxView.TabStop = false;
            this.resultBoxView.Paint += new System.Windows.Forms.PaintEventHandler(this.resultBoxView_Paint);
            this.resultBoxView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resultBoxView_MouseDown);
            this.resultBoxView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.resultBoxView_MouseMove);
            this.resultBoxView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.resultBoxView_MouseUp);
            // 
            // captureImage
            // 
            this.captureImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.captureImage.Location = new System.Drawing.Point(472, 99);
            this.captureImage.Name = "captureImage";
            this.captureImage.Size = new System.Drawing.Size(172, 67);
            this.captureImage.TabIndex = 3;
            this.captureImage.Text = "Capture Image";
            this.captureImage.UseVisualStyleBackColor = true;
            this.captureImage.Visible = false;
            this.captureImage.Click += new System.EventHandler(this.captureImage_Click);
            // 
            // printImage
            // 
            this.printImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.printImage.Location = new System.Drawing.Point(472, 172);
            this.printImage.Name = "printImage";
            this.printImage.Size = new System.Drawing.Size(172, 67);
            this.printImage.TabIndex = 4;
            this.printImage.Text = "Print Image";
            this.printImage.UseVisualStyleBackColor = true;
            this.printImage.Visible = false;
            this.printImage.Click += new System.EventHandler(this.printImage_Click);
            // 
            // saveImage
            // 
            this.saveImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveImage.Location = new System.Drawing.Point(472, 245);
            this.saveImage.Name = "saveImage";
            this.saveImage.Size = new System.Drawing.Size(172, 67);
            this.saveImage.TabIndex = 5;
            this.saveImage.Text = "Save Image";
            this.saveImage.UseVisualStyleBackColor = true;
            this.saveImage.Visible = false;
            this.saveImage.Click += new System.EventHandler(this.saveImage_Click);
            // 
            // cameraSelection
            // 
            this.cameraSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cameraSelection.FormattingEnabled = true;
            this.cameraSelection.Location = new System.Drawing.Point(472, 55);
            this.cameraSelection.Name = "cameraSelection";
            this.cameraSelection.Size = new System.Drawing.Size(172, 24);
            this.cameraSelection.TabIndex = 8;
            this.cameraSelection.SelectedIndexChanged += new System.EventHandler(this.cameraSelection_SelectedIndexChanged);
            // 
            // perspectiveButton
            // 
            this.perspectiveButton.Location = new System.Drawing.Point(870, 309);
            this.perspectiveButton.Name = "perspectiveButton";
            this.perspectiveButton.Size = new System.Drawing.Size(75, 23);
            this.perspectiveButton.TabIndex = 12;
            this.perspectiveButton.Text = "Next";
            this.perspectiveButton.UseVisualStyleBackColor = true;
            this.perspectiveButton.Visible = false;
            this.perspectiveButton.Click += new System.EventHandler(this.perspectiveButton_Click);
            // 
            // filterBox
            // 
            this.filterBox.Controls.Add(this.currentSliderValue);
            this.filterBox.Controls.Add(this.maxFliterValue);
            this.filterBox.Controls.Add(this.minFilterValue);
            this.filterBox.Controls.Add(this.filterSlider);
            this.filterBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.filterBox.Location = new System.Drawing.Point(0, 435);
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(1116, 88);
            this.filterBox.TabIndex = 13;
            this.filterBox.TabStop = false;
            this.filterBox.Text = "Filter";
            this.filterBox.Visible = false;
            // 
            // currentSliderValue
            // 
            this.currentSliderValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.currentSliderValue.AutoSize = true;
            this.currentSliderValue.Location = new System.Drawing.Point(552, 57);
            this.currentSliderValue.Name = "currentSliderValue";
            this.currentSliderValue.Size = new System.Drawing.Size(16, 17);
            this.currentSliderValue.TabIndex = 3;
            this.currentSliderValue.Text = "1";
            // 
            // maxFliterValue
            // 
            this.maxFliterValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.maxFliterValue.AutoSize = true;
            this.maxFliterValue.Location = new System.Drawing.Point(1064, 59);
            this.maxFliterValue.Name = "maxFliterValue";
            this.maxFliterValue.Size = new System.Drawing.Size(16, 17);
            this.maxFliterValue.TabIndex = 2;
            this.maxFliterValue.Text = "5";
            // 
            // minFilterValue
            // 
            this.minFilterValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.minFilterValue.AutoSize = true;
            this.minFilterValue.Location = new System.Drawing.Point(13, 59);
            this.minFilterValue.Name = "minFilterValue";
            this.minFilterValue.Size = new System.Drawing.Size(16, 17);
            this.minFilterValue.TabIndex = 1;
            this.minFilterValue.Text = "0";
            // 
            // filterSlider
            // 
            this.filterSlider.AutoSize = false;
            this.filterSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterSlider.Location = new System.Drawing.Point(3, 18);
            this.filterSlider.Maximum = 500;
            this.filterSlider.Name = "filterSlider";
            this.filterSlider.Size = new System.Drawing.Size(1110, 56);
            this.filterSlider.TabIndex = 0;
            this.filterSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.filterSlider.Scroll += new System.EventHandler(this.filterSlider_Scroll);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1116, 523);
            this.Controls.Add(this.filterBox);
            this.Controls.Add(this.perspectiveButton);
            this.Controls.Add(this.cameraSelection);
            this.Controls.Add(this.saveImage);
            this.Controls.Add(this.printImage);
            this.Controls.Add(this.captureImage);
            this.Controls.Add(this.resultBoxView);
            this.Controls.Add(this.imageBoxView);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "Form1";
            this.Text = "Document Scanner";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxView)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultBoxView)).EndInit();
            this.filterBox.ResumeLayout(false);
            this.filterBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem imageMenu;
        private System.Windows.Forms.ToolStripMenuItem cameraMenu;
        private System.Windows.Forms.ToolStripMenuItem startCapture;
        private System.Windows.Forms.ToolStripMenuItem pauseCapture;
        private System.Windows.Forms.Button captureImage;
        private System.Windows.Forms.PageSetupDialog printMenu;
        private System.Windows.Forms.Button printImage;
        private System.Windows.Forms.Button saveImage;
        private System.Windows.Forms.ToolStripMenuItem stopCapture;
        private System.Windows.Forms.ComboBox cameraSelection;
        private System.Windows.Forms.Button perspectiveButton;
        private System.Windows.Forms.GroupBox filterBox;
        private System.Windows.Forms.Label minFilterValue;
        private System.Windows.Forms.TrackBar filterSlider;
        private System.Windows.Forms.Label maxFliterValue;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrastToolStripMenuItem;
        private System.Windows.Forms.Label currentSliderValue;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adaptiveThresholdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cropImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToOriginalImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotatePointsToolStripMenuItem;
        public System.Windows.Forms.PictureBox imageBoxView;
        public System.Windows.Forms.PictureBox resultBoxView;
    }
}

