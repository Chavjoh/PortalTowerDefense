using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.GameEngine.MoveManagers
{
    /// <summary>
    /// Move manager for a standard walk movement
    /// Should be used when monster walks along the path
    /// </summary>
    public class WalkMoveManager : MoveManager
    {
        /// <summary>
        /// Inherited from MoveManager
        /// Called by associated monster when he has to move
        /// </summary>
        public override void move()
        {
            double vectorX = this.Monster.Location.X - this.Monster.X;
            double vectorY = this.Monster.Location.Y - this.Monster.Y;
            double norm = Math.Sqrt(vectorX * vectorX + vectorY * vectorY);
            norm = 2 / norm;
            vectorX *= norm;
            vectorY *= norm;
            this.Monster.move(this.Monster.X + vectorX, this.Monster.Y + vectorY);
        }
    }
}
