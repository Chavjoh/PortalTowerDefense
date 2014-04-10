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
        /// Default constructor
        /// </summary>
        /// <param name="towerLocation">tower location on which resides tower to modify</param>
        public TowerMenu(Map.TowerLocation towerLocation)
        {
            // GUI init
            InitializeComponent();

            // Associated requester
            this.towerLocation = towerLocation;
        }

        /// <summary>
        /// Upgrade the tower to next level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageUpgrade_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("upgrade");
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
    }
}
