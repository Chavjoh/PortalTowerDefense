using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.GameEngine.MoveManagers
{
    /// <summary>
    /// Move manager for an immediate "teleportation" movement
    /// Should be used when monster goes through a portal
    /// </summary>
    class ImmediateMoveManager : MoveManager
    {
        /// <summary>
        /// Inherited from MoveManager
        /// Called by associated monster when he has to move
        /// </summary>
        public override void move()
        {

        }
    }
}
