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
    public class Map
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
            List<Entity.Monster> monsterInRange = new List<Entity.Monster>();

            foreach (Entity.Monster monster in monsters)
            {
                int distance = (int) Math.Sqrt(Math.Pow(monster.X - x, 2) + Math.Pow(monster.Y - y, 2));
                
                if (distance < range)
                {
                    monsterInRange.Add(monster);
                }
            }

            return monsterInRange;
        }

        /// <summary>
        /// Cancel bullets targetting a specific monster, because he passed through a portal
        /// </summary>
        /// <param name="monster">Monster targeted by bullets</param>
        public void cancelBulletsTargeting(Entity.Monster monster)
        {
            foreach (TowerLocation location in this.towerLocations)
            {
                if (location.Tower != null)
                {
                    location.Tower.cancelBulletsTargeting(monster);
                }
            }
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

                case "trapPoint":
                    mapPoint = new Points.TrapPoint(x, y, monsterOrientation, layerIndex);

                    Entity.Trap trap;
                    switch (pathPoint.Attributes.GetNamedItem("type").Value)
                    {
                        case "jumpPad":
                            // Get the destination point after the monster jump with the jumpPad
                            int destX = int.Parse(pathPoint.Attributes.GetNamedItem("destX").Value);
                            int destY = int.Parse(pathPoint.Attributes.GetNamedItem("destY").Value);
                            Point destinationPoint = new Points.DeathPoint(destX, destY);

                            // Create the jumpPad trap
                            trap = new Entity.Traps.JumpPadTrap((Points.TrapPoint)mapPoint, destinationPoint);
                            break;

                        default:
                            throw new Exception("[TrapPoint] Unknown trap point type.");
                    }

                    // Save trap inside the point
                    ((Points.TrapPoint)mapPoint).Trap = trap;
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
    }
}
