﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map.Points
{
    class PortalPoint : Point
    {
        /// <summary>
        /// Inherited from Point
        /// Get the move manager to use to go the current point
        /// </summary>
        /// <returns>MoveManager to assign to monster</returns>
        public override GameEngine.MoveManager getMoveManager()
        {
            return null;
        }
    }
}
