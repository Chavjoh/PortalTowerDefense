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
    /// Logique d'interaction pour MonsterControl.xaml
    /// Visual control for a monster
    /// </summary>
    public partial class MonsterControl : UserControl, GameEngine.Dynamic
    {
        /// <summary>
        /// List of images
        /// </summary>
        private Dictionary<int, BitmapImage> images;

        /// <summary>
        /// Pixels differentials between left of image and base placement
        /// </summary>
        private double deltaX;

        /// <summary>
        /// Pixels diffrentials between top of image and base placement
        /// </summary>
        private double deltaY;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MonsterControl(Dictionary<int,String> images, double deltaX, double deltaY)
        {
            // Visual element initialization
            InitializeComponent();

            // Differentials
            this.deltaX = deltaX;
            this.deltaY = deltaY + this.mainGrid.RowDefinitions[0].Height.Value + this.mainGrid.RowDefinitions[1].Height.Value;

            // Dictionary of images
            this.images = new Dictionary<int, BitmapImage>();
            foreach(int index in images.Keys)
            {
                this.images.Add(index, new BitmapImage(new Uri(images[index])));
            }

            // Add control to drawing canvas
            GameEngine.GameManager.Instance.Canvas.Children.Add(this);
        }

        /// <summary>
        /// Default constructor
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
            if (this.images.Keys.Contains(index))
            {
                this.image.Source = this.images[index];
            }
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
        /// Define the value of lifebar
        /// </summary>
        /// <param name="life">Value in percent</param>
        public void setLife(int life)
        {
            life /= 2;
            if (life >= 0 && life <= 50)
            {
                this.lifeBar.Width = life;
                if (life > 25)
                {
                    this.lifeBar.Fill = (System.Windows.Media.Brush)Application.Current.FindResource("BrushLifeGreen");
                }
                else if (life > 10)
                {
                    this.lifeBar.Fill = (System.Windows.Media.Brush)Application.Current.FindResource("BrushLifeOrange");
                }
                else
                {
                    this.lifeBar.Fill = (System.Windows.Media.Brush)Application.Current.FindResource("BrushLifeRed");
                }
            }
        }
    }
}
