using System;
using System.Drawing;
using System.Windows.Forms;

namespace AimatriX
{
    public class CrosshairForm : Form
    {
        private TrayIconManager trayIconManager;

        public CrosshairForm()
        {
            InitializeOverlay();
            trayIconManager = new TrayIconManager();
        }

        private void InitializeOverlay()
        {
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            ShowInTaskbar = false;
            BackColor = Color.Magenta;
            TransparencyKey = Color.Magenta;
            Bounds = Screen.PrimaryScreen.Bounds;

            var crosshairPath = "Resources/crosshair.png";
            if (System.IO.File.Exists(crosshairPath))
            {
                BackgroundImage = new Bitmap(crosshairPath);
                BackgroundImageLayout = ImageLayout.Center;
            }

            Load += (s, e) => { this.TopMost = true; };
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            trayIconManager.Dispose();
            base.OnFormClosing(e);
        }
    }
}