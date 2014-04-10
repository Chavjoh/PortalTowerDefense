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
    /// Logique d'interaction pour TowerMenu.xaml
    /// </summary>
    public partial class TowerMenu : UserControl
    {
        /// <summary>
        /// Tower location on which resides the tower to modify
        /// </summary>
        private Map.TowerLocation towerLocation;

        /// <summary>
        /// Index of element on which is the mouse
        /// </summary>
        private int onItem;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="towerLocation">tower location on which resides tower to modify</param>
        public TowerMenu(Map.TowerLocation towerLocation)
        {
            // GUI init
            InitializeComponent();

            // Associated requester
            this.towerLocation = towerLocation;

            // Set default item
            this.onItem = 0;

            // Update max costs
            this.updateMaxCost();
        }

        /// <summary>
        /// Update available items, based on player's money
        /// </summary>
        public void updateMaxCost()
        {
            this.imageUpgrade.Opacity = 0.3;
            if (this.towerLocation.Tower.Level < 3)
            {
                if (this.towerLocation.Tower is Entity.Towers.ArcherTower)
                {
                    if (GameEngine.GameManager.Instance.canBuy(Entity.Towers.ArcherTower.getPrice(this.towerLocation.Tower.Level)))
                    {
                        this.imageUpgrade.Opacity = 1;
                    }
                }
                else if (this.towerLocation.Tower is Entity.Towers.ArtilleryTower)
                {
                    if (GameEngine.GameManager.Instance.canBuy(Entity.Towers.ArtilleryTower.getPrice(this.towerLocation.Tower.Level)))
                    {
                        this.imageUpgrade.Opacity = 1;
                    }
                }
                else if (this.towerLocation.Tower is Entity.Towers.MagicTower)
                {
                    if (GameEngine.GameManager.Instance.canBuy(Entity.Towers.MagicTower.getPrice(this.towerLocation.Tower.Level)))
                    {
                        this.imageUpgrade.Opacity = 1;
                    }
                }
            }
            this.updateLabel();
        }

        /// <summary>
        /// Update label content
        /// </summary>
        private void updateLabel()
        {
            switch (this.onItem)
            {
                case 1:
                    this.labelPrice.Foreground = (System.Windows.Media.Brush)Application.Current.FindResource("BrushLabelPriceNegative");
                    if (this.imageUpgrade.Opacity == 1)
                    {
                        if (this.towerLocation.Tower is Entity.Towers.ArcherTower)
                        {
                            this.labelPrice.Content = "-" + Entity.Towers.ArcherTower.getPrice(this.towerLocation.Tower.Level + 1).ToString() + "$";
                        }
                        else if (this.towerLocation.Tower is Entity.Towers.ArtilleryTower)
                        {
                            this.labelPrice.Content = "-" + Entity.Towers.ArtilleryTower.getPrice(this.towerLocation.Tower.Level + 1).ToString() + "$";
                        }
                        else if (this.towerLocation.Tower is Entity.Towers.MagicTower)
                        {
                            this.labelPrice.Content = "-" + Entity.Towers.MagicTower.getPrice(this.towerLocation.Tower.Level + 1).ToString() + "$";
                        }
                    }
                    else
                    {
                        if (this.towerLocation.Tower.Level >= 3)
                        {
                            this.labelPrice.Content = "Top level";
                        }
                        else
                        {
                            this.labelPrice.Content = "Not enough $";
                        }
                    }
                    break;
                case 2:
                    this.labelPrice.Foreground = (System.Windows.Media.Brush)Application.Current.FindResource("BrushLabelPricePositive");
                    if (this.towerLocation.Tower is Entity.Towers.ArcherTower)
                    {
                        this.labelPrice.Content = "+" + (Entity.Towers.ArcherTower.getPrice(this.towerLocation.Tower.Level) / 2.0).ToString() + "$";
                    }
                    else if (this.towerLocation.Tower is Entity.Towers.ArtilleryTower)
                    {
                        this.labelPrice.Content = "+" + (Entity.Towers.ArtilleryTower.getPrice(this.towerLocation.Tower.Level) / 2.0).ToString() + "$";
                    }
                    else if (this.towerLocation.Tower is Entity.Towers.MagicTower)
                    {
                        this.labelPrice.Content = "+" + (Entity.Towers.MagicTower.getPrice(this.towerLocation.Tower.Level) / 2.0).ToString() + "$";
                    }
                    break;
                default:
                    this.labelPrice.Content = null;
                    break;
            }
        }

        /// <summary>
        /// Upgrade the tower to next level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.imageUpgrade.Opacity == 1)
            {
                Console.WriteLine("upgrade");
            }
        }

        /// <summary>
        /// Mouse enter the upgrade button, update displayed price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageUpgrade_MouseEnter(object sender, MouseEventArgs e)
        {
            this.onItem = 1;
            this.updateLabel();
        }

        /// <summary>
        /// Sell the tower on location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageSell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("sell");
        }

        /// <summary>
        /// Mouse enter the sell button, update displayed price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageSell_MouseEnter(object sender, MouseEventArgs e)
        {
            this.onItem = 2;
            this.updateLabel();
        }

        /// <summary>
        /// Mouse leave a tower button, clear displayed price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            this.onItem = 0;
            this.updateLabel();
        }


    }
}
