using System;
using System.Drawing;
using System.Windows.Forms;

public class SplashForm : Form
{
    private Label versionLabel;

    public SplashForm()
    {
        // Window setup
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ClientSize = new Size(573, 209);
        this.BackColor = Color.Black;

        // Background image
        this.BackgroundImage = Image.FromFile("Resources/aimatrix_start.png");
        this.BackgroundImageLayout = ImageLayout.Zoom;

             // Version
        versionLabel = new Label()
        {
            Text = "Version 0.3",
            ForeColor = Color.White,
            Font = new Font("Roboto", 14),
            AutoSize = true,
            BackColor = Color.Transparent,
            Location = new Point(465, 180)
        };

        this.Controls.Add(versionLabel);
    }
}
