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
    public abstract class Tower : GameEngine.Tickable
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
        /// Linked location, on which the tower is placed
        /// </summary>
        private Map.TowerLocation location;

        /// <summary>
        /// Linked bullets, thrown by tower and having not reached monster yet
        /// </summary>
        private List<Bullet> bullets;

        /// <summary>
        /// Visual control, drawing the tower at its location
        /// </summary>
        private View.Control.TowerControl control;

        /// <summary>
        /// Get image path of the current tower
        /// </summary>
        /// <returns>Image path</returns>
        public abstract string image();

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
