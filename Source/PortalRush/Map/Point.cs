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
        /// ZIndex given to monsters passing this point
        /// </summary>
        private int zIndex;

        /// <summary>
        /// Orientation given to monsters passing this point
        /// </summary>
        private Entity.MonsterOrientation orientation;

        /// <summary>
        /// Distance from this point to the path TargetPoint
        /// </summary>
        private int distanceToTarget = -1;

        /// <summary>
        /// Point to reach after current
        /// </summary>
        private Point next = null;

        /// <summary>
        /// X position on screen
        /// </summary>
        public int X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Y position on screen
        /// </summary>
        public int Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Point to reach after current
        /// </summary>
        public Point Next
        {
            get
            {
                return this.next;
            }
            set
            {
                this.next = value;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        /// <param name="orientation">Orientation given to monsters passing this point</param>
        /// <param name="zIndex">ZIndex given to monsters passing this point</param>
        public Point(int x, int y, Entity.MonsterOrientation orientation = Entity.MonsterOrientation.NULL, int zIndex = -1)
        {
            // Position on screen
            this.x = x;
            this.y = y;

            // Orientation given to monsters passing this point
            this.orientation = orientation;

            // ZIndex given to monsters passing this point
            this.zIndex = zIndex;
        }

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
            monster.Location = this.next.getRangedLocation();
            monster.MoveManager = this.next.getMoveManager();
            if (this.orientation != Entity.MonsterOrientation.NULL)
            {
                monster.Orientation = this.orientation;
            }
            if (this.zIndex >= 0)
            {
                monster.ZIndex = this.zIndex;
            }
        }

        /// <summary>
        /// Get a real location, with random range from the logic path point
        /// </summary>
        /// <returns>Real location usable by a monster as a target for movement</returns>
        public Location getRangedLocation()
        {
            Random random = new Random();
            double rangedX = this.x + (random.NextDouble() - 0.5) * 30;
            double rangedY = this.y + (random.NextDouble() - 0.5) * 30;
            return new Location(this, rangedX, rangedY);
        }
    }
}
