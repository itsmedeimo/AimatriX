using System;
using System.Drawing;
using System.Windows.Forms;

namespace AimatriX
{
    public class TrayIconManager : IDisposable
    {
        private NotifyIcon trayIcon;

        public TrayIconManager()
        {
            trayIcon = new NotifyIcon
            {
                Icon = new Icon("Resources/aimatrix.ico"),
                Visible = true,
                Text = "AimatriX"
            };

            var contextMenu = new ContextMenuStrip();
            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) => Application.Exit();
            contextMenu.Items.Add(exitItem);
            trayIcon.ContextMenuStrip = contextMenu;
        }

        public void Dispose()
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
        }
    }
}