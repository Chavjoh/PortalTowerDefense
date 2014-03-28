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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PortalRush
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// Main view window containing application
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Register canvas in GameManager
            GameEngine.GameManager.Instance.Canvas = this.mainCanvas;

            // Launch default configuration
            GameEngine.GameManager.Instance.loadConfig();

            try
            {
                // Load default map
                GameEngine.GameManager.Instance.loadMap("Static");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Map loading error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Set window size using map size
            this.Width = GameEngine.GameManager.Instance.Map.Width;
            this.Height = GameEngine.GameManager.Instance.Map.Height;

            // Launch game
            GameEngine.GameManager.Instance.play();
        }

        /// <summary>
        /// Closing procedure, must stop subthreads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            GameEngine.GameManager.Instance.quit();
        }

        /// <summary>
        /// Click event on canvas, managing for child tower locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (UIElement uiElement in this.mainCanvas.Children)
            {
                View.Control.TowerLocationControl control = uiElement as View.Control.TowerLocationControl;
                if (control != null)
                {
                    Point position = e.GetPosition(control);
                    if (position.X >= 0 && position.Y >= 0 && position.X < control.ActualWidth && position.Y < control.ActualHeight)
                    {
                        control.click();
                        return;
                    }
                }
            }
        }
    }
}
