﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PortalRush.Map
{
    /// <summary>
    /// Current map being played
    /// </summary>
    class Map
    {
        /// <summary>
        /// List of layers for drawing the map
        /// </summary>
        private List<View.Control.LayerControl> layers;

        /// <summary>
        /// List of monsters currently on the map
        /// </summary>
        private List<Entity.Monster> monsters;

        /// <summary>
        /// List of points, representing logical path(s) for monsters
        /// </summary>
        private List<Point> points;

        /// <summary>
        /// List of tower locations where towers can be placed
        /// </summary>
        private List<TowerLocation> towerLocations;

        /// <summary>
        /// Monster to add to referenced list
        /// </summary>
        public Entity.Monster NewMonster
        {
            set
            {
                this.monsters.Add(value);
            }
        }

        /// <summary>
        /// Monster to remove from referenced list
        /// </summary>
        public Entity.Monster OldMonster
        {
            set
            {
                if (this.monsters.Contains(value))
                {
                    this.monsters.Remove(value);
                }
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filepath">XML file path for map loading</param>
        public Map(String filepath)
        {
            // TEMP CODE, WAITING FOR XML PARSERS TO BE IN PLACE
            if (filepath == null)
            {
                this.TEMP_MAP_CONSTRUCTOR();
            }

            // Construct local lists
            this.monsters = new List<Entity.Monster>();
        }

        /// <summary>
        /// Get list of monsters in a ranged location
        /// </summary>
        /// <param name="x">X position of range</param>
        /// <param name="y">Y position of range</param>
        /// <param name="range">Size of range</param>
        /// <returns>List of retrieved monsters</returns>
        public List<Entity.Monster> monstersInRange(int x, int y, int range)
        {
            return null;
        }

        /// <summary>
        /// Map loader
        /// </summary>
        /// <param name="filepath">XML file path for map loading</param>
        private void loadMap(String filepath)
        {

        }

        /// <summary>
        /// Load a map visual layer
        /// </summary>
        /// <param name="xmlDocument">XML node representing layer</param>
        private void loadLayer(XmlDocument xmlDocument)
        {

        }

        /// <summary>
        /// Load a tower location on the map
        /// </summary>
        /// <param name="xmlDocument">XML node representing tower location</param>
        private void loadTowerLocation(XmlDocument xmlDocument)
        {

        }

        /// <summary>
        /// Load a point on the map
        /// </summary>
        /// <param name="xmlDocument">XML node representing point</param>
        private void loadPoint(XmlDocument xmlDocument)
        {

        }

        /// <summary>
        /// Link the Point objects like described in XML file
        /// </summary>
        private void linkPoints()
        {

        }

        /// <summary>
        /// TEMP FUNCTION, WAITING FOR XML PARSERS TO BE IN PLACE
        /// </summary>
        private void TEMP_MAP_CONSTRUCTOR()
        {
            // Register monsters images folder
            Entity.Monster.BaseFolder = "Maps\\Static\\Monsters\\";

            // Create layers
            this.layers = new List<View.Control.LayerControl>();
            this.layers.Add(new View.Control.LayerControl("Maps\\Static\\Layers\\map_layer1.png", 1));
            this.layers.Add(new View.Control.LayerControl("Maps\\Static\\Layers\\map_layer2.png", 2));
            this.layers.Add(new View.Control.LayerControl("Maps\\Static\\Layers\\map_layer4.png", 4));
            this.layers.Add(new View.Control.LayerControl("Maps\\Static\\Layers\\map_layer6.png", 6));
            this.layers.Add(new View.Control.LayerControl("Maps\\Static\\Layers\\map_layer7.png", 7));

            // Create points
            this.points = new List<Point>();
            Points.SpawnPoint spawn = new Points.SpawnPoint(344, 562, Entity.MonsterOrientation.REAR_LEFT, 5);
            spawn.spawn(60, Entity.MonsterType.CHELL);
            this.points.Add(spawn);
            this.points.Add(new Points.PathPoint(271, 472, Entity.MonsterOrientation.REAR_RIGHT));
            this.points.Add(new Points.PathPoint(419, 445, Entity.MonsterOrientation.REAR_LEFT));
            this.points.Add(new Points.PathPoint(326, 339));
            this.points.Add(new Points.PortalPoint(732, 386, Entity.MonsterOrientation.FRONT_LEFT));
            this.points.Add(new Points.PathPoint(631, 405, Entity.MonsterOrientation.REAR_LEFT));
            this.points.Add(new Points.PathPoint(525, 304, Entity.MonsterOrientation.REAR_LEFT, 3));
            this.points.Add(new Points.TargetPoint(494, 255));
        }
    }
}
