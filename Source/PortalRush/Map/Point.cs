using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map
{
    /// <summary>
    /// Generic path point, describing a part of a path for monsters
    /// </summary>
    abstract class Point
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
        /// Distance from this point to the path TargetPoint
        /// </summary>
        private int distanceToTarget;

        /// <summary>
        /// Point to reach after current
        /// </summary>
        private Point next;

        /// <summary>
        /// Get the move manager to use to go the current point
        /// </summary>
        /// <returns>MoveManager to assign to monster</returns>
        public abstract GameEngine.MoveManager getMoveManager();

        /// <summary>
        /// Triggered when a monster arrives to the point
        /// </summary>
        /// <param name="monster">Monster who arrived</param>
        public virtual void monsterArrived(Entity.Monster monster)
        {

        }

        /// <summary>
        /// Get a real location, with random range from the logic path point
        /// </summary>
        /// <returns>Real location usable by a monster as a target for movement</returns>
        public Location getRangedLocation()
        {
            return null;
        }
    }
}
