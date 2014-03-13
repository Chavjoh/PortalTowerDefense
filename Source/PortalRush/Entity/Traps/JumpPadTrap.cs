using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity.Traps
{
    /// <summary>
    /// JumpPad trap, making the monster fly out of the map
    /// </summary>
    class JumpPadTrap : Trap
    {
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
