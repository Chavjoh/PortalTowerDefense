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

namespace PortalRush.View.Menu
{
    /// <summary>
    /// Logique d'interaction pour NoTowerMenu.xaml
    /// </summary>
    public partial class NoTowerMenu : UserControl
    {
        /// <summary>
        /// Tower location on which build a tower
        /// </summary>
        private Map.TowerLocation towerLocation;

        /// <summary>
        /// Index of tower type on which the mouse is
        /// </summary>
        private int onTower;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="towerLocation">tower location on which build a tower</param>
        public NoTowerMenu(Map.TowerLocation towerLocation)
        {
            // GUI init
            InitializeComponent();

            // Associated requester
            this.towerLocation = towerLocation;

            // Set default tower
            this.onTower = 0;

            // Update max costs
            this.updateMaxCost();
        }

        /// <summary>
        /// Update available items, based on player's money
        /// </summary>
        public void updateMaxCost()
        {
            if (GameEngine.GameManager.Instance.canBuy(Entity.Towers.ArcherTower.getPrice(1)))
            {
                this.imageArcher.Opacity = 1;
            }
            else
            {
                this.imageArcher.Opacity = 0.3;
            }
            if (GameEngine.GameManager.Instance.canBuy(Entity.Towers.ArtilleryTower.getPrice(1)))
            {
                this.imageArtillery.Opacity = 1;
            }
            else
            {
                this.imageArtillery.Opacity = 0.3;
            }
            if (GameEngine.GameManager.Instance.canBuy(Entity.Towers.MagicTower.getPrice(1)))
            {
                this.imageMagic.Opacity = 1;
            }
            else
            {
                this.imageMagic.Opacity = 0.3;
            }
            this.updateLabel();
        }

        /// <summary>
        /// Update label content
        /// </summary>
        private void updateLabel()
        {
            switch (this.onTower)
            {
                case 1:
                    if (this.imageArcher.Opacity == 1)
                    {
                        this.labelPrice.Content = "-" + Entity.Towers.ArcherTower.getPrice(1).ToString() + "$";
                    }
                    else
                    {
                        this.labelPrice.Content = "Not enough $";
                    }
                    break;
                case 2:
                    if (this.imageArtillery.Opacity == 1)
                    {
                        this.labelPrice.Content = "-" + Entity.Towers.ArtilleryTower.getPrice(1).ToString() + "$";
                    }
                    else
                    {
                        this.labelPrice.Content = "Not enough $";
                    }
                    break;
                case 3:
                    if (this.imageMagic.Opacity == 1)
                    {
                        this.labelPrice.Content = "-" + Entity.Towers.MagicTower.getPrice(1).ToString() + "$";
                    }
                    else
                    {
                        this.labelPrice.Content = "Not enough $";
                    }
                    break;
                default:
                    this.labelPrice.Content = null;
                    break;
            }
        }

        /// <summary>
        /// Build an archer tower
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageArcher_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.imageArcher.Opacity == 1)
            {
                this.towerLocation.createTower(new Entity.Towers.ArcherTower());
            }
        }

        /// <summary>
        /// Mouse enter an archer tower button, update displayed price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageArcher_MouseEnter(object sender, MouseEventArgs e)
        {
            this.onTower = 1;
            this.updateLabel();
        }

        /// <summary>
        /// Build an artillery tower
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageArtillery_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.imageArtillery.Opacity == 1)
            {
                this.towerLocation.createTower(new Entity.Towers.ArtilleryTower());
            }
        }

        /// <summary>
        /// Mouse enter an artillery tower button, update displayed price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageArtillery_MouseEnter(object sender, MouseEventArgs e)
        {
            this.onTower = 2;
            this.updateLabel();
        }

        /// <summary>
        /// Build a magic tower
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageMagic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.imageMagic.Opacity == 1)
            {
                this.towerLocation.createTower(new Entity.Towers.MagicTower());
            }
        }

        /// <summary>
        /// Mouse enter a magic tower button, update displayed price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageMagic_MouseEnter(object sender, MouseEventArgs e)
        {
            this.onTower = 3;
            this.updateLabel();
        }

        /// <summary>
        /// Mouse leave a tower button, clear displayed price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            this.onTower = 0;
            this.updateLabel();
        }
    }
}
