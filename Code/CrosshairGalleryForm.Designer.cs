using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AimatriX.Forms
{
    partial class CrosshairGalleryForm
    {
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox zoomedInPreview;
        private Button btnSave;

        /// <summary>
        /// Required method for Designer support — do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Instantiate
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.zoomedInPreview = new PictureBox();
            this.btnSave = new Button();

            // flowLayoutPanel1
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = DockStyle.Left;
            this.flowLayoutPanel1.Width = 200;
            this.flowLayoutPanel1.BackColor = Color.Black;

            // zoomedInPreview
            this.zoomedInPreview.Dock = DockStyle.Fill;
            this.zoomedInPreview.SizeMode = PictureBoxSizeMode.Zoom;
            this.zoomedInPreview.BackColor = Color.Black;

            // btnSave
            this.btnSave.Text = "Save";
            this.btnSave.Dock = DockStyle.Bottom;
            this.btnSave.Height = 40;
            this.btnSave.BackColor = Color.DimGray;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // CrosshairGalleryForm
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(600, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Crosshair Gallery";
            this.BackColor = Color.Black;
            this.ForeColor = Color.White;

            // Icon
            var icoPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources", "aimatrix.ico");
            if (File.Exists(icoPath))
                this.Icon = new Icon(icoPath);

            // Add controls in correct z-order
            this.Controls.Add(this.zoomedInPreview);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnSave);
        }
    }
}
