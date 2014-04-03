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
    /// Logique d'interaction pour TowerLocationControl.xaml
    /// Visual control for a tower location
    /// </summary>
    public partial class TowerLocationControl : UserControl, GameEngine.Dynamic
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
        /// Logic owner for passing back click events
        /// </summary>
        private Map.TowerLocation owner;

        #region Properties

        public double DeltaX
        {
            set { this.deltaX = value; }
        }

        public double DeltaY
        {
            set { this.deltaY = value; }
        }

        #endregion

        /// <summary>
        ///  Default constructor
        /// </summary>
        public TowerLocationControl(Map.TowerLocation owner, String image, double deltaX, double deltaY)
        {
            // Visual element initialization
            InitializeComponent();

            // Owner registration
            this.owner = owner;

            // Differentials
            this.deltaX = deltaX;
            this.deltaY = deltaY;

            // Image loading
            this.image.Source = new BitmapImage(new Uri(image));

            // Add control to drawing canvas
            GameEngine.GameManager.Instance.Canvas.Children.Add(this);
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
        /// Change image for another (used for tower construction or destruction)
        /// </summary>
        /// <param name="image">Image path</param>
        public void changeImage(String image)
        {
            this.image.Source = new BitmapImage(new Uri(image));

            this.deltaX = (int)(this.image.Source.Width / 2);
            this.deltaY = (int)(this.image.Source.Height / 1.35);
            move(owner.X, owner.Y);
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
        /// Click on the tower location, called by canvas who receive event
        /// </summary>
        public void click()
        {
            this.owner.click();
        }
    }
}
