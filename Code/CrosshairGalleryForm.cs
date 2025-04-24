using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AimatriX;

namespace AimatriX.Forms
{
    public partial class CrosshairGalleryForm : Form
    {
        private readonly string crosshairFolder = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Resources", "CrosshairGallery");
        private string selectedCrosshair = null;
        private readonly Settings appSettings;

        public CrosshairGalleryForm(Settings settings)
        {
            appSettings = settings;
            InitializeComponent();   // this now calls the one in Designer.cs
            LoadCrosshairs();
            SetFormIcon();
        }

        private void SetFormIcon()
        {
            var iconPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources", "aimatrix.ico");
            if (File.Exists(iconPath))
                this.Icon = new Icon(iconPath);
        }

        private void LoadCrosshairs()
        {
            flowLayoutPanel1.Controls.Clear();
            if (!Directory.Exists(crosshairFolder))
                Directory.CreateDirectory(crosshairFolder);

            foreach (var file in Directory.GetFiles(crosshairFolder, "*.png"))
            {
                var name = Path.GetFileNameWithoutExtension(file);
                var pic = new PictureBox
                {
                    Image = Image.FromFile(file),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 64,
                    Height = 64,
                    Margin = new Padding(10),
                    Tag = name,
                    BackColor = Color.Black
                };
                pic.Click += (s, e) =>
                {
                    selectedCrosshair = name;
                    UpdateZoomedInPreview();
                };

                var label = new Label
                {
                    Text = name,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Width = 64,
                    Height = 20,
                    Top = pic.Bottom,
                    ForeColor = Color.White
                };

                var panel = new Panel { Width = 70, Height = 90 };
                panel.Controls.Add(pic);
                panel.Controls.Add(label);

                flowLayoutPanel1.Controls.Add(panel);
            }

            UpdateZoomedInPreview();
        }

        private void UpdateZoomedInPreview()
        {
            if (selectedCrosshair != null)
            {
                var path = Path.Combine(crosshairFolder, selectedCrosshair + ".png");
                if (File.Exists(path))
                    zoomedInPreview.Image = Image.FromFile(path);
            }
            else
            {
                zoomedInPreview.Image = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedCrosshair == null) return;

            appSettings.SelectedCrosshair = selectedCrosshair;
            appSettings.Save();
            MessageBox.Show(
                $"Saved '{selectedCrosshair}' as your crosshair.",
                "Crosshair Saved",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            this.Close();
        }
    }
}
