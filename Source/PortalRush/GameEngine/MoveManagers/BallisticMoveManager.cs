﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.GameEngine.MoveManagers
{
    /// <summary>
    /// Move manager for a ballistic movement
    /// Should be used when monster is ejected by a JumpPadTrap
    /// </summary>
    public class BallisticMoveManager : MoveManager
    {
        /// <summary>
        /// Count each tick
        /// </summary>
        int tick;

        /// <summary>
        /// Start point for the ballistic movement
        /// </summary>
        Map.Point startPoint;

        /// <summary>
        /// Destionation point for the ballistic movement
        /// </summary>
        Map.Point destinationPoint;

        /// <summary>
        /// Create a new ballistic movement
        /// </summary>
        /// <param name="startPoint">Start point for the ballistic movement</param>
        /// <param name="destinationPoint">Destionation point for the ballistic movement</param>
        public BallisticMoveManager(Map.Point startPoint, Map.Point destinationPoint)
        {
            this.tick = 0;
            this.startPoint = startPoint;
            this.destinationPoint = destinationPoint;
        }

        /// <summary>
        /// Inherited from MoveManager
        /// Called by associated monster when he has to move
        /// </summary>
        public override void move()
        {
            tick++;

            //double tickNormalized = tick/30.0;

            // We want to arrive to the final position in ticks
            double timeToDestination = 150;

            // Be careful, to go down, we increase our pixel position

            // MRUA
            double vectorY = tick * (destinationPoint.Y - startPoint.Y) / timeToDestination; //(0.5 * -9.81 * tickNormalized * tickNormalized) + (10 * tickNormalized);

            // MRU
            double vectorX = tick * (destinationPoint.X - startPoint.X) / timeToDestination;

            // Move monster with calculated position
            this.Monster.move(startPoint.X + vectorX, startPoint.Y + vectorY);
        }
    }
}
