using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AimatriX
{
    public partial class CrosshairForm : Form
    {
        private Bitmap currentImage;
        private TrayIconManager trayIconManager;

        public CrosshairForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            StartPosition = FormStartPosition.Manual;

            UpdateCrosshairImage("Resources/crosshair.png");

            trayIconManager = new TrayIconManager(this);
        }

        public void UpdateCrosshairImage(string imagePath)
        {
            if (currentImage != null)
                currentImage.Dispose();

            currentImage = new Bitmap(imagePath);
            ApplyPerPixelTransparency(currentImage);
            CenterCrosshair();
        }

        public void CenterCrosshair()
        {
            var screen = Screen.PrimaryScreen.Bounds;
            var size = currentImage?.Size ?? new Size(100, 100);
            this.Location = new Point((screen.Width - size.Width) / 2, (screen.Height - size.Height) / 2);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_LAYERED = 0x80000;
                const int WS_EX_TRANSPARENT = 0x20;

                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT;
                return cp;
            }
        }

        private void ApplyPerPixelTransparency(Bitmap bmp)
        {
            IntPtr screenDC = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr memDC = NativeMethods.CreateCompatibleDC(screenDC);
            IntPtr hBitmap = bmp.GetHbitmap(Color.FromArgb(0));
            IntPtr oldBitmap = NativeMethods.SelectObject(memDC, hBitmap);

            NativeMethods.SIZE size = new NativeMethods.SIZE { cx = bmp.Width, cy = bmp.Height };
            NativeMethods.POINT pointSource = new NativeMethods.POINT { x = 0, y = 0 };
            NativeMethods.POINT topPos = new NativeMethods.POINT { x = Left, y = Top };
            NativeMethods.BLENDFUNCTION blend = new NativeMethods.BLENDFUNCTION
            {
                BlendOp = 0,
                BlendFlags = 0,
                SourceConstantAlpha = 255,
                AlphaFormat = 1
            };

            NativeMethods.UpdateLayeredWindow(Handle, screenDC, ref topPos, ref size, memDC, ref pointSource, 0, ref blend, 2);

            NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
            if (hBitmap != IntPtr.Zero)
            {
                NativeMethods.SelectObject(memDC, oldBitmap);
                NativeMethods.DeleteObject(hBitmap);
            }
            NativeMethods.DeleteDC(memDC);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            trayIconManager.Dispose();
            base.OnFormClosing(e);
        }
    }

    internal class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT { public int x; public int y; }
        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE { public int cx; public int cy; }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst,
            ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}
