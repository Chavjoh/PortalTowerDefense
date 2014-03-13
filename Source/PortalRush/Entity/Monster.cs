using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity
{
    /// <summary>
    /// Generic monster, going along the path
    /// </summary>
    abstract class Monster : GameEngine.Tickable
    {
        /// <summary>
        /// Non-magic armor level
        /// </summary>
        protected int strenghArmor;

        /// <summary>
        /// Magic armor level
        /// </summary>
        protected int magicArmor;

        /// <summary>
        /// X position on screen
        /// </summary>
        private int x;

        /// <summary>
        /// Y position on screen
        /// </summary>
        private int y;

        /// <summary>
        /// Current health of monster
        /// </summary>
        private int health;

        /// <summary>
        /// Money got when monster dies
        /// </summary>
        private int money;

        /// <summary>
        /// Linked MoveManager, which to call when having to move (1/30 sec)
        /// </summary>
        private GameEngine.MoveManager moveManager;

        /// <summary>
        /// Linked Location, which to have as a target when moving
        /// </summary>
        private Map.Location location;

        /// <summary>
        /// Visual control, displaying the monster on the map
        /// </summary>
        private View.Control.MonsterControl control;

        /// <summary>
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        public void tick()
        {

        }

        /// <summary>
        /// Get distance to final target
        /// </summary>
        /// <returns>Distance to final TargetPoint</returns>
        public int distanceToTarget()
        {
            return 0;
        }
    }
}
