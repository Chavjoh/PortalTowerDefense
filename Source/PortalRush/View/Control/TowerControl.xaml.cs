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
    /// Logique d'interaction pour TowerControl.xaml
    /// Visual control for a tower
    /// </summary>
    public partial class TowerControl : UserControl, GameEngine.Dynamic
    {
        /// <summary>
        /// Pixels differentials between left of image and base placement
        /// </summary>
        private double deltaX;

        /// <summary>
        /// Pixels differentials between top of image and base placement
        /// </summary>
        private double deltaY;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TowerControl(String image)
        {
            // Visual element initialization
            InitializeComponent();

            // Differentials 
            this.deltaX = 0;
            this.deltaY = 0;

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
            Canvas.SetLeft(this, x - this.deltaX);
            Canvas.SetTop(this, y - this.deltaY);
        }

        /// <summary>
        /// Change deltas of object for displaying in correct basement position
        /// </summary>
        /// <param name="deltaX">new X delta to apply</param>
        /// <param name="deltaY">new Y delta to apply</param>
        public void changeDelta(double deltaX, double deltaY)
        {
            double left = Canvas.GetLeft(this) + this.deltaX;
            double top = Canvas.GetTop(this) + this.deltaY;
            this.deltaX = deltaX;
            this.deltaY = deltaY;
            this.move(left, top);
        }
    }
}
