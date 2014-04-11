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
    /// Logique d'interaction pour BulletControl.xaml
    /// Visual control for a bullet
    /// </summary>
    public partial class BulletControl : UserControl, GameEngine.Dynamic
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BulletControl(String image)
        {
            // Visual element initialization
            InitializeComponent();

            // Image loading
            this.image.Source = new BitmapImage(new Uri(image));

            // Add control to drawing canvas
            GameEngine.GameManager.Instance.Canvas.Children.Add(this);
        }

        /// <summary>
        /// Default destructor
        /// </summary>
        public void dispose()
        {
            // Remove control from drawing canvas
            GameEngine.GameManager.Instance.Canvas.Children.Remove(this);
        }

        /// <summary>
        /// Change the image for the element
        /// </summary>
        /// <param name="index">Index of new image, referencing internal tab</param>
        public void changeImage(int index)
        {
            // Nothing
        }

        /// <summary>
        /// Change the Z Index of the element
        /// </summary>
        /// <param name="index">Z-index of element in canvas</param>
        public void changeZIndex(int index)
        {
            Canvas.SetZIndex(this, index);
        }

        /// <summary>
        /// Move the element to a given location
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        public void move(double x, double y)
        {
            Canvas.SetLeft(this, x - 4);
            Canvas.SetTop(this, y - 4);
        }
    }
}
