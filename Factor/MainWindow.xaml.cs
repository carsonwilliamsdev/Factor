using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Factor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IntPtr _trackedWindow;
        private const string _windowName = "Untitled - Notepad";

        public MainWindow()
        {
            InitializeComponent();
            _trackedWindow = User32.FindWindow(null, _windowName);
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            if (User32.GetForegroundWindow() == _trackedWindow)
            {
                var rect = User32.GetWindowRect(_trackedWindow, true);
                Top = rect.Top;
                Left = rect.Left;
                Width = rect.Width;
                Height = rect.Height;
            }
            else
            {
                Top = -32000;
                Left = -32000;
                Width = 0;
                Height = 0;
            }
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            User32.SetWindowExStyle(hwnd, User32.WsExToolWindow); // removes window from toolbar and alt+tab
            //User32.SetWindowExStyle(hwnd, User32.WsExTransparent | User32.WsExToolWindow);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
