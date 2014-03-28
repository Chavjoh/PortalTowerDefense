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

namespace PortalRush.View.Control
{
    /// <summary>
    /// Logique d'interaction pour LayerControl.xaml
    /// Visual control for a layer of a map
    /// </summary>
    public partial class LayerControl : UserControl
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="imagePath">Absolute path to plain-field image</param>
        /// <param name="zIndex">Z-index to use (can't be modified in the future)</param>
        public LayerControl(String imagePath, int zIndex)
        {
            InitializeComponent();

            // Set image
            this.image.Source = new BitmapImage(new Uri(imagePath));

            // Set Zindex
            Canvas.SetZIndex(this, zIndex);

            // Add control to drawing canvas
            GameEngine.GameManager.Instance.Canvas.Children.Add(this);
        }
    }
}
