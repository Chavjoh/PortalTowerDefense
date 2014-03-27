using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PortalRush.Map
{
    /// <summary>
    /// Location on which a tower can be placed on the map
    /// </summary>
    class TowerLocation
    {
        /// <summary>
        /// Base folder for tower location image
        /// </summary>
        private static String baseFolder;

        /// <summary>
        /// Base folder for tower location image
        /// </summary>
        public static String BaseFolder
        {
            set
            {
                baseFolder = AppDomain.CurrentDomain.BaseDirectory + value;
            }
        }

        /// <summary>
        /// X position on screen
        /// </summary>
        private int x;

        /// <summary>
        /// Y position on screen
        /// </summary>
        private int y;

        /// <summary>
        /// Linked tower, occupying the location
        /// </summary>
        private Entity.Tower tower;

        /// <summary>
        /// Visual control, drawing the tower location and sub-menu when user acting with location
        /// </summary>
        private View.Control.TowerLocationControl control;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        /// <param name="zIndex">Z index of tower location and tower on it</param>
        public TowerLocation(int x, int y, int zIndex)
        {
            // Assign coordinates
            this.x = x;
            this.y = y;

            // No tower on it at creation
            this.tower = null;

            // Prepare visual control
            String image = TowerLocation.baseFolder + "location.png";

            // Add visual control to UI
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control = new View.Control.TowerLocationControl(image, 35, 17);
                this.control.move(x, y);
                this.control.changeZIndex(zIndex);
            });
        }

        /// <summary>
        /// Triggered when the user click on the location to create a tower (location is empty)
        /// </summary>
        public void clickCreate()
        {

        }

        /// <summary>
        /// Triggered when the user click on the location to upgrade or sell a tower (location is filled)
        /// </summary>
        public void clickTower()
        {

        }

        /// <summary>
        /// Build the given tower on this location
        /// </summary>
        /// <param name="tower">Tower to place on this location</param>
        public void createTower(Entity.Tower tower)
        {

        }

        /// <summary>
        /// Sell the tower currently on the location
        /// </summary>
        public void sellTower()
        {

        }

        /// <summary>
        /// Modify the tower occupying the location
        /// </summary>
        /// <param name="tower">Tower to place, or null</param>
        private void setTower(Entity.Tower tower)
        {

        }
    }
}
