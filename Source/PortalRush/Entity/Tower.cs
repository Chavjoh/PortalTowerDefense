using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity
{
    /// <summary>
    /// Generic tower entity, attacking spotted monsters
    /// </summary>
    abstract class Tower : GameEngine.Tickable
    {
        /// <summary>
        /// Attack range, in screen units
        /// </summary>
        protected int attackRange;

        /// <summary>
        /// Attack speed, elapsed time between 2 bullets being thrown
        /// </summary>
        protected int attackSpeed;

        /// <summary>
        /// Damages made to monster targetted monster
        /// Subkind of tower decides wether this value concerns magic or non-magic damages
        /// </summary>
        protected int attackDamage;

        /// <summary>
        /// Current tower level
        /// </summary>
        protected int level;

        /// <summary>
        /// List of monster currently in attack range (spotted)
        /// </summary>
        private List<Monster> attackedMonsters;

        /// <summary>
        /// Upgrade the tower to its next level
        /// </summary>
        public abstract void upgrade();

        /// <summary>
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        public void tick()
        {

        }

        /// <summary>
        /// Throw a bullet to the nearest-from-target monster currently spotted
        /// </summary>
        private void attack()
        {

        }
    }
}
