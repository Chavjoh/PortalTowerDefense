﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PortalRush.Map
{
    /// <summary>
    /// Location on which a tower can be placed on the map
    /// </summary>
    public class TowerLocation
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
            get
            {
                return baseFolder;
            }
            set
            {
                baseFolder = value;
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
        /// Layer index to position tower
        /// </summary>
        private int layerIndex;

        /// <summary>
        /// Linked tower, occupying the location
        /// </summary>
        private Entity.Tower tower;

        /// <summary>
        /// Layer index to position tower
        /// </summary>
        public int LayerIndex
        {
            get
            {
                return this.layerIndex;
            }
        }

        /// <summary>
        /// Linked tower, occupying the location
        /// </summary>
        public Entity.Tower Tower
        {
            get
            {
                return this.tower;
            }
        }
        
        /// <summary>
        /// Visual control, drawing the tower location and sub-menu when user acting with location
        /// </summary>
        private View.Control.TowerLocationControl control;

        #region Properties

        public int X
        {
            get { return this.x; }
        }

        public int Y 
        {
            get { return this.y; }
        }

        #endregion

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

            // Set layer index
            this.layerIndex = zIndex;

            // No tower on it at creation
            this.tower = null;

            // Prepare visual control
            String image = TowerLocation.baseFolder + "location.png";

            // Add visual control to UI
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control = new View.Control.TowerLocationControl(this, image, 35, 17);
                this.control.move(x, y);
                this.control.changeZIndex(zIndex);
            });
        }

        /// <summary>
        /// Triggered when the user click on the location
        /// </summary>
        public void click()
        {
            if (this.tower == null)
            {
                this.clickCreate();
            }
            else
            {
                this.clickTower();
            }
        }

        /// <summary>
        /// Triggered when the user click on the location to create a tower (location is empty)
        /// </summary>
        private void clickCreate()
        {
            View.Menu.NoTowerMenu menu = new View.Menu.NoTowerMenu(this);
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.mainGrid.Children.Add(menu);
            Grid.SetRow(menu, 1);
            Grid.SetColumn(menu, 0);
            menu.Margin = new Thickness(this.x - menu.Width / 2.0, this.y + menu.Height / 2.0, 0, 0);
        }

        /// <summary>
        /// Triggered when the user click on the location to upgrade or sell a tower (location is filled)
        /// </summary>
        private void clickTower()
        {
            View.Menu.TowerMenu menu = new View.Menu.TowerMenu(this);
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.mainGrid.Children.Add(menu);
            Grid.SetRow(menu, 1);
            Grid.SetColumn(menu, 0);
            menu.Margin = new Thickness(this.x - menu.Width / 2.0, this.y + menu.Height / 2.0, 0, 0);
        }

        /// <summary>
        /// Build the given tower on this location
        /// </summary>
        /// <param name="tower">Tower to place on this location</param>
        public void createTower(Entity.Tower tower)
        {
            this.setTower(tower);
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.clearAllMenus();
        }

        /// <summary>
        /// Sell the tower currently on the location
        /// </summary>
        public void sellTower()
        {
            this.tower.sell();
            this.setTower(null);
        }

        /// <summary>
        /// Modify the tower occupying the location
        /// </summary>
        /// <param name="tower">Tower to place, or null</param>
        private void setTower(Entity.Tower tower)
        {
            if (this.tower != null)
            {
                this.tower.dispose();
            }
            this.tower = tower;
            if (this.tower != null)
            {
                this.tower.place(this);
            }
        }
    }
}
