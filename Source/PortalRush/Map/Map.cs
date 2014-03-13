using System;
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
        /// Default constructor
        /// </summary>
        /// <param name="filepath">XML file path for map loading</param>
        public Map(String filepath)
        {

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
    }
}
