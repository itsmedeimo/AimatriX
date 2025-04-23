using System;
using System.IO;
using System.Windows.Forms;

namespace AimatriX
{
    public class TrayIconManager : IDisposable
    {
        private readonly NotifyIcon trayIcon;
        private readonly CrosshairForm crosshairForm;
        private readonly Settings settings;

        public TrayIconManager(CrosshairForm form, Settings appSettings)
        {
            crosshairForm = form;
            settings = appSettings;

            trayIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon("Resources/aimatrix.ico"),
                Visible = true,
                Text = "AimatriX"
            };

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Center Crosshair", null, (s, e) => crosshairForm.CenterCrosshair());
            contextMenu.Items.Add("Select Crosshair", null, (s, e) => SelectCrosshairFromFile());
//            contextMenu.Items.Add("Choose From Library", null, (s, e) => ShowGallery());
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Exit", null, (s, e) => Application.Exit());

            trayIcon.ContextMenuStrip = contextMenu;
        }

        private void SelectCrosshairFromFile()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "PNG Files (*.png)|*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    settings.SelectedCrosshair = ofd.FileName;
                    settings.Save();
                    crosshairForm.UpdateCrosshairImage(ofd.FileName);
                }
            }
        }


        public void Dispose()
        {
            trayIcon?.Dispose();
        }
    }
}
