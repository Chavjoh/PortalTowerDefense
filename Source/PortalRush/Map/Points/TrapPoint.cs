﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map.Points
{
    /// <summary>
    /// Special point corresponding to a trap in which monster will fall when passing
    /// </summary>
    class TrapPoint : Point
    {
        /// <summary>
        /// Linked trap, triggered when a monster passed on the point
        /// </summary>
        private Entity.Trap trap;

        /// <summary>
        /// Inherited from Point
        /// Get the move manager to use to go the current point
        /// </summary>
        /// <returns>MoveManager to assign to monster</returns>
        public override GameEngine.MoveManager getMoveManager()
        {
            return null;
        }

        /// <summary>
        /// Inherited from Point
        /// Triggered when a monster arrives to the point
        /// </summary>
        /// <param name="monster">Monster who arrived</param>
        public override void monsterArrived(Entity.Monster monster)
        {
            base.monsterArrived(monster);
        }
    }
}