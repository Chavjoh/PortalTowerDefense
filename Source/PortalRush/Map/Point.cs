using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map
{
    abstract class Point
    {
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
    }
}
