using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factor
{
    public static class Core
    {
        public static MainWindow MainWindow;

        public static async void Initialize()
        {
            MainWindow = new MainWindow();
            MainWindow.Show();
            await Task.Delay(100);
            UpdateOverlayAsync();
        }

        private static async void UpdateOverlayAsync()
        {
            while (true)
            {
                MainWindow.UpdatePosition();
                await Task.Delay(100);
            }
        }
    }
}
