using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity.Traps
{
    /// <summary>
    /// Laser trap, making the monster eatable for conventional people
    /// </summary>
    public class LaserTrap : Trap
    {
        /// <summary>
        /// Create a new laser trap
        /// </summary>
        public LaserTrap(Map.Points.TrapPoint trapPoint)
            : base(trapPoint)
        {

        }

        /// <summary>
        /// Inherited from Trap
        /// Triggered when a monster arrives in the trap
        /// </summary>
        /// <param name="monster">Monster who arrived</param>
        public override void monsterArrived(Monster monster)
        {

        }
    }
}
