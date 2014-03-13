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
    abstract class MoveManager
    {
        /// <summary>
        /// Linked monster, which to move when called (1/30 sec)
        /// </summary>
        private Entity.Monster monster;

        /// <summary>
        /// Called by associated monster when he has to move
        /// </summary>
        public abstract void move();
    }
}
