using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PortalRush.Map
{
    /// <summary>
    /// Current map being played
    /// </summary>
    class Map
    {
        #region Attributes

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
        /// Map width
        /// </summary>
        private int width;

        /// <summary>
        /// Map height
        /// </summary>
        private int height;

        /// <summary>
        /// Absolute path to map description file
        /// </summary>
        private string mapFile;

        #endregion

        #region Properties

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
        /// Map width
        /// </summary>
        public int Width
        {
            get { return this.width; }
        }

        /// <summary>
        /// Map height
        /// </summary>
        public int Height
        {
            get { return this.height; }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filepath">XML file path for map loading</param>
        public Map(String filepath)
        {
            monsters = new List<Entity.Monster>();
            points = new List<Point>();

            this.loadMap(filepath);
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
            mapFile = filepath;

            if (File.Exists(mapFile))
            {
                // Load XML from map file
                XmlDocument mapDoc = new XmlDocument();
                mapDoc.Load(mapFile);

                // Load nodes
                XmlNode mapWidth = mapDoc.SelectSingleNode("/portalRushMap/width");
                XmlNode mapHeight = mapDoc.SelectSingleNode("/portalRushMap/height");
                XmlNode monsterPath = mapDoc.SelectSingleNode("/portalRushMap/monsterPath");
                XmlNode towerPath = mapDoc.SelectSingleNode("/portalRushMap/towerPath");
                XmlNode mapPathPointList = mapDoc.SelectSingleNode("/portalRushMap/pathPointList");

                // Save map size
                this.width = int.Parse(mapWidth.InnerText);
                this.height = int.Parse(mapHeight.InnerText);

                // Save monster path
                Entity.Monster.BaseFolder = Path.GetDirectoryName(mapFile) + "\\" + monsterPath.InnerText;
                TowerLocation.BaseFolder = Path.GetDirectoryName(mapFile) + "\\" + towerPath.InnerText;

                // Store point list to bind them together
                Dictionary<string, Point> pointList = new Dictionary<string, Point>();
                Dictionary<string, string> bindList = new Dictionary<string, string>();

                // Save all path points
                foreach (XmlNode pathPoint in mapPathPointList)
                {
                    loadPoint(pathPoint, pointList, bindList);
                }

                // Bind points
                linkPoints(pointList, bindList);

                // Save all tower positions
                loadTowerLocation(mapDoc);

                // Save all layers
                loadLayer(mapDoc);

                // Save points list in class attribute
                foreach (KeyValuePair<string, Point> pointPair in pointList)
                {
                    this.points.Add(pointPair.Value);
                }
            }
            else
            {
                throw new FileNotFoundException("Map configuration file not found (searching for " + mapFile + ").");
            }
        }

        /// <summary>
        /// Load a map visual layer
        /// </summary>
        /// <param name="mapDocument">XML Map Descriptof</param>
        private void loadLayer(XmlDocument mapDocument)
        {
            this.layers = new List<View.Control.LayerControl>();
            XmlNode mapLayerList = mapDocument.SelectSingleNode("/portalRushMap/layerList");
            string layerLocation = mapLayerList.Attributes.GetNamedItem("location").Value;

            foreach (XmlNode layer in mapLayerList)
            {
                checkAttributeList(layer, new List<string> { "layerIndex", "image" }, "Layer : ");

                int layerIndex = int.Parse(layer.Attributes.GetNamedItem("layerIndex").Value);
                string layerImageName = layer.Attributes.GetNamedItem("image").Value;

                this.layers.Add(new View.Control.LayerControl(Path.GetDirectoryName(mapFile) + "\\" + layerLocation + layerImageName, layerIndex));
            }
        }

        /// <summary>
        /// Load a tower location on the map
        /// </summary>
        /// <param name="mapDocument">XML Map Descriptor</param>
        private void loadTowerLocation(XmlDocument mapDocument)
        {
            towerLocations = new List<TowerLocation>();
            XmlNode mapTowerPositionList = mapDocument.SelectSingleNode("/portalRushMap/towerPositionList");

            foreach (XmlNode towerPosition in mapTowerPositionList)
            {
                checkAttributeList(towerPosition, new List<string> { "x", "y", "layerIndex" }, "TowerLocation : ");

                int x = int.Parse(towerPosition.Attributes.GetNamedItem("x").Value);
                int y = int.Parse(towerPosition.Attributes.GetNamedItem("y").Value);
                int layerIndex = int.Parse(towerPosition.Attributes.GetNamedItem("layerIndex").Value);

                this.towerLocations.Add(new TowerLocation(x, y, layerIndex));
            }
        }

        /// <summary>
        /// Load a point on the map
        /// </summary>
        /// <param name="pathPoint">XML node representing point</param>
        /// <param name="pointList">List of all points</param>
        /// <param name="bindList">List of points to bind between them</param>
        private void loadPoint(XmlNode pathPoint, Dictionary<string, Point> pointList, Dictionary<string, string> bindList)
        {
            Point mapPoint;

            string id = pathPoint.Attributes.GetNamedItem("id").Value;
            string idNext = (pathPoint.Attributes.GetNamedItem("next") != null) ? pathPoint.Attributes.GetNamedItem("next").Value : "";

            int x = int.Parse(pathPoint.Attributes.GetNamedItem("x").Value);
            int y = int.Parse(pathPoint.Attributes.GetNamedItem("y").Value);

            string monsterOrientationName = (pathPoint.Attributes.GetNamedItem("monsterOrientation") != null) ? pathPoint.Attributes.GetNamedItem("monsterOrientation").Value : "";
            Entity.MonsterOrientation monsterOrientation;
            int layerIndex = (pathPoint.Attributes.GetNamedItem("layerIndex") != null) ? int.Parse(pathPoint.Attributes.GetNamedItem("layerIndex").Value) : -1;

            switch (monsterOrientationName)
            {
                case "REAR_LEFT":
                    monsterOrientation = Entity.MonsterOrientation.REAR_LEFT;
                    break;

                case "REAR_RIGHT":
                    monsterOrientation = Entity.MonsterOrientation.REAR_RIGHT;
                    break;

                case "FRONT_LEFT":
                    monsterOrientation = Entity.MonsterOrientation.FRONT_LEFT;
                    break;

                case "FRONT_RIGHT":
                    monsterOrientation = Entity.MonsterOrientation.FRONT_RIGHT;
                    break;

                default:
                    monsterOrientation = Entity.MonsterOrientation.NULL;
                    break;
            }

            switch (pathPoint.Name)
            {
                case "spawnPoint":
                    mapPoint = new Points.SpawnPoint(x, y, monsterOrientation, layerIndex);

                    foreach (XmlNode monsterOnSpawnPoint in pathPoint)
                    {
                        // Common values
                        int interval = int.Parse(monsterOnSpawnPoint.Attributes.GetNamedItem("interval").Value);
                        int startTime = int.Parse(monsterOnSpawnPoint.Attributes.GetNamedItem("startTime").Value);
                        int quantity = int.Parse(monsterOnSpawnPoint.Attributes.GetNamedItem("quantity").Value);
                        Entity.MonsterType monsterType;

                        // Choose monster type
                        switch (monsterOnSpawnPoint.Name)
                        {
                            case "chell":
                                monsterType = Entity.MonsterType.CHELL;
                                break;

                            default:
                                throw new Exception("Invalid monster type (" + monsterOnSpawnPoint.Name + ")");
                        }

                        // Create each monster at the good start time
                        for (int currentTime = startTime; currentTime < (startTime + quantity * interval); currentTime += interval)
                        {
                            ((Points.SpawnPoint)mapPoint).spawn(currentTime, monsterType);
                        }
                    }
                    break;

                case "pathPoint":
                    mapPoint = new Points.PathPoint(x, y, monsterOrientation, layerIndex);
                    break;

                case "portalPoint":
                    mapPoint = new Points.PortalPoint(x, y, monsterOrientation, layerIndex);
                    break;

                case "targetPoint":
                    mapPoint = new Points.TargetPoint(x, y);
                    break;

                default:
                    throw new Exception("Node type not registered (" + pathPoint.Name + ") in map file.");
            }

            pointList.Add(id, mapPoint);

            // Remember to bind it with another point if indicated
            if (!String.IsNullOrEmpty(idNext))
            {
                bindList.Add(id, idNext);
            }
        }

        /// <summary>
        /// Link the Point objects like described in XML file
        /// </summary>
        /// <param name="pointList">List of all points</param>
        /// <param name="bindList">List of points to bind between them</param>
        private void linkPoints(Dictionary<string, Point> pointList, Dictionary<string, string> bindList)
        {
            foreach (KeyValuePair<string, string> bindPair in bindList)
            {
                if (pointList.ContainsKey(bindPair.Key) && pointList.ContainsKey(bindPair.Value))
                {
                    pointList[bindPair.Key].Next = pointList[bindPair.Value];
                }
                else
                {
                    throw new Exception("Invalid map points binding between ID " + bindPair.Key + " and ID " + bindPair.Value);
                }
            }
        }

        /// <summary>
        /// Check if a list of attributes exist in a node
        /// </summary>
        /// <param name="node">Node to check the attribute in list</param>
        /// <param name="attributeList">List of attribute to check in node</param>
        /// <param name="title">Exception title</param>
        private void checkAttributeList(XmlNode node, List<string> attributeList, string title = "")
        {
            foreach (string attributeName in attributeList)
            {
                if (node.Attributes.GetNamedItem(attributeName) == null)
                {
                    throw new Exception(title + "Attribute " + attributeName + " not found.");
                }
            }
        }

        /// <summary>
        /// TEMP FUNCTION, WAITING FOR XML PARSERS TO BE IN PLACE
        /// </summary>
        private void TEMP_MAP_CONSTRUCTOR()
        {
            // Register images folder
            Entity.Monster.BaseFolder = "Maps\\Static\\Monsters\\";
            TowerLocation.BaseFolder = "Maps\\Static\\Towers\\";

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
            spawn.spawn(30, Entity.MonsterType.CHELL);
            spawn.spawn(60, Entity.MonsterType.CHELL);
            spawn.spawn(90, Entity.MonsterType.CHELL);
            spawn.spawn(120, Entity.MonsterType.CHELL);
            spawn.spawn(150, Entity.MonsterType.CHELL);
            spawn.spawn(180, Entity.MonsterType.CHELL);
            Points.PathPoint p1 = new Points.PathPoint(271, 472, Entity.MonsterOrientation.REAR_RIGHT);
            Points.PathPoint p2 = new Points.PathPoint(419, 445, Entity.MonsterOrientation.REAR_LEFT);
            Points.PathPoint p3 = new Points.PathPoint(326, 339);
            Points.PortalPoint p4 = new Points.PortalPoint(732, 386, Entity.MonsterOrientation.FRONT_LEFT);
            Points.PathPoint p5 = new Points.PathPoint(631, 405, Entity.MonsterOrientation.REAR_LEFT);
            Points.PathPoint p6 = new Points.PathPoint(525, 304, Entity.MonsterOrientation.REAR_LEFT, 3);
            Points.TargetPoint p7 = new Points.TargetPoint(494, 255);
            this.points.Add(spawn);
            this.points.Add(p1);
            this.points.Add(p2);
            this.points.Add(p3);
            this.points.Add(p4);
            this.points.Add(p5);
            this.points.Add(p6);
            this.points.Add(p7);
            spawn.Next = p1;
            p1.Next = p2;
            p2.Next = p3;
            p3.Next = p4;
            p4.Next = p5;
            p5.Next = p6;
            p6.Next = p7;
            p7.Next = null;

            // Create tower locations
            this.towerLocations = new List<TowerLocation>();
            this.towerLocations.Add(new TowerLocation(221, 520, 3));
            this.towerLocations.Add(new TowerLocation(245, 439, 3));
            this.towerLocations.Add(new TowerLocation(292, 395, 3));
            this.towerLocations.Add(new TowerLocation(460, 400, 3));
            this.towerLocations.Add(new TowerLocation(500, 358, 5));
            this.towerLocations.Add(new TowerLocation(530, 387, 5));
        }
    }
}
