using System;
using System.IO;
using System.Windows.Forms;
using AimatriX.Forms;    // for CrosshairGalleryForm

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
                Icon = new System.Drawing.Icon(Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Resources", "aimatrix.ico")),
                Visible = true,
                Text = "AimatriX"
            };

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Center Crosshair", null, (s, e) => crosshairForm.CenterCrosshair());
            contextMenu.Items.Add("Select Crosshair", null, (s, e) => SelectCrosshairFromFile());

            // ← Here we launch the gallery, passing in our Settings instance
            contextMenu.Items.Add("Crosshair Gallery", null, (s, e) => ShowGallery());

            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Exit", null, (s, e) => Application.Exit());

            trayIcon.ContextMenuStrip = contextMenu;
        }

        private void SelectCrosshairFromFile()
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "PNG Files (*.png)|*.png"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Store full path for backward compatibility
                settings.SelectedCrosshair = ofd.FileName;
                settings.Save();

                crosshairForm.UpdateCrosshairImage(ofd.FileName);
            }
        }

        private void ShowGallery()
        {
            // Pass the same settings instance you loaded at startup
            using var gallery = new CrosshairGalleryForm(settings);
            gallery.ShowDialog();

            // After the user picks & saves, reapply the selection
            string sel = settings.SelectedCrosshair;
            string path = ResolveCrosshairPath(sel);
            if (File.Exists(path))
                crosshairForm.UpdateCrosshairImage(path);
        }

        private string ResolveCrosshairPath(string sel)
        {
            // If they stored a full path, just return it
            if (File.Exists(sel))
                return sel;

            // Otherwise assume it's a gallery name
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources", "CrosshairGallery",
                sel + ".png");
        }

        public void Dispose()
        {
            trayIcon?.Dispose();
        }
    }
}
