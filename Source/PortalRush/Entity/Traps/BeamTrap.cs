using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity.Traps
{
    /// <summary>
    /// Beam trap, catching monster and making him fly over a hole for a given time before releasing him...
    /// </summary>
    public class BeamTrap : Trap
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
