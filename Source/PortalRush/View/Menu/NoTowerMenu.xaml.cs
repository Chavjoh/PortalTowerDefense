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
        /// Default constructor
        /// </summary>
        /// <param name="towerLocation">tower location on which build a tower</param>
        public NoTowerMenu(Map.TowerLocation towerLocation)
        {
            // GUI init
            InitializeComponent();

            // Associated requester
            this.towerLocation = towerLocation;
        }

        /// <summary>
        /// Build an archer tower
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageArcher_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.towerLocation.createTower(new Entity.Towers.ArcherTower());
        }

        /// <summary>
        /// Build an artillery tower
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageArtillery_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.towerLocation.createTower(new Entity.Towers.ArtilleryTower());
        }

        /// <summary>
        /// Build a magic tower
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageMagic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.towerLocation.createTower(new Entity.Towers.MagicTower());
        }
    }
}
