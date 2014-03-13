using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity
{
    /// <summary>
    /// Bullet thrown by a tower to attack a monster
    /// </summary>
    class Bullet
    {
        /// <summary>
        /// Damage level for non-magic attack
        /// </summary>
        private int damageStrengh;

        /// <summary>
        /// Damage level for a magic attack
        /// </summary>
        private int damageMagic;

        /// <summary>
        /// Range for group damages
        /// Should be set to 0 for non-ranged attack
        /// </summary>
        private int damageRange;

        /// <summary>
        /// X position on screen
        /// </summary>
        private int x;

        /// <summary>
        /// Y position on screen
        /// </summary>
        private int y;

        /// <summary>
        /// Main target monster
        /// </summary>
        private Monster target;

        /// <summary>
        /// Visual control, drawing the bullet at its current location
        /// </summary>
        private View.Control.BulletControl control;

        /// <summary>
        /// Move the bullet to its next position (1/30 sec)
        /// </summary>
        public void move()
        {

        }

        /// <summary>
        /// Bullet has reached its target and should damage concerned monsters
        /// </summary>
        private void targetReached()
        {

        }
    }
}
