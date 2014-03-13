using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map
{
    /// <summary>
    /// Location on which a tower can be placed on the map
    /// </summary>
    class TowerLocation
    {
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
