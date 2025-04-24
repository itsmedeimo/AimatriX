using System;
using System.Windows.Forms;

namespace AimatriX
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var splash = new SplashForm())
            {
                splash.Show();
                Application.DoEvents();
                System.Threading.Thread.Sleep(2500); // Keep for 2.5s
            }

            Application.Run(new CrosshairForm());
        }
    }
}