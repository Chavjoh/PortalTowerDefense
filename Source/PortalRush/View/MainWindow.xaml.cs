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

            // TEMP CODE : WAITING FOR XML PARSERS TO BE IN PLACE
            this.Width = 916;
            this.Height = 798;

            GameEngine.GameManager.Instance.Canvas = this.mainCanvas;
            GameEngine.GameManager.Instance.loadConfig();
            GameEngine.GameManager.Instance.loadMap(-1);
            GameEngine.GameManager.Instance.play();
        }
    }
}
