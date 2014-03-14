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
        private double x;

        /// <summary>
        /// Y position on screen
        /// </summary>
        private double y;

        /// <summary>
        /// Linked point, having generated this real location on map
        /// </summary>
        private Point point;

        /// <summary>
        /// X position on screen
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Y position on screen
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Linked point, having generated this real location on map
        /// </summary>
        public Point Point
        {
            get
            {
                return this.point;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="point">Base point of this ranged location</param>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        public Location(Point point, double x, double y)
        {
            this.point = point;
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Check if a monster has reached the point
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        /// <returns></returns>
        public bool isReached(double x, double y)
        {
            double dist = Math.Sqrt(Math.Pow(this.x - x, 2) + Math.Pow(this.y - y, 2));
            return(dist <= 2.0);
        }
    }
}
