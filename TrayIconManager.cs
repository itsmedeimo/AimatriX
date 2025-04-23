using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AimatriX
{
    public class TrayIconManager : IDisposable
    {
        private NotifyIcon trayIcon;
        private CrosshairForm crosshairForm;

        public TrayIconManager(CrosshairForm form)
        {
            crosshairForm = form;

            trayIcon = new NotifyIcon();
            trayIcon.Icon = new Icon("Resources/aimatrix.ico");
            trayIcon.Visible = true;

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Select Crosshair", null, SelectCrosshair);
            contextMenu.Items.Add("Center Crosshair", null, CenterCrosshair);
            contextMenu.Items.Add("Exit", null, Exit);

            trayIcon.ContextMenuStrip = contextMenu;
        }

        private void SelectCrosshair(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PNG files (*.png)|*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;
                    if (File.Exists(selectedFile))
                    {
                        crosshairForm.UpdateCrosshairImage(selectedFile);
                    }
                }
            }
        }

        private void CenterCrosshair(object sender, EventArgs e)
        {
            crosshairForm.CenterCrosshair();
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Dispose()
        {
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
                trayIcon = null;
            }
        }
    }
}
