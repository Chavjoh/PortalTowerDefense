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
        /// Called by associated monster when he has to move
        /// </summary>
        public abstract void move();
    }
}
