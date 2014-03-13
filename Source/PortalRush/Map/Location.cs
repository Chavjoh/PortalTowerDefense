using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map
{
    /// <summary>
    /// Real screen location, used by monster as a target when moving
    /// </summary>
    class Location
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
        /// Linked point, having generated this real location on map
        /// </summary>
        private Point point;
    }
}
