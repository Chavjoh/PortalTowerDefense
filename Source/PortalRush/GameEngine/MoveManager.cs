using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.GameEngine
{
    /// <summary>
    /// Move management for monsters, depending on targeted point
    /// </summary>
    public abstract class MoveManager
    {
        /// <summary>
        /// Linked monster, which to move when called (1/30 sec)
        /// </summary>
        private Entity.Monster monster;

        /// <summary>
        /// Linked monster, which to move when called (1/30 sec)
        /// </summary>
        public Entity.Monster Monster
        {
            get
            {
                return this.monster;
            }
            set
            {
                this.monster = value;
            }
        }

        /// <summary>
        /// Called by associated monster when he has to move
        /// </summary>
        public abstract void move();
    }
}
