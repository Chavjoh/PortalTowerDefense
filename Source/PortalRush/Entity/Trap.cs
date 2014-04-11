using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity
{
    /// <summary>
    /// Generic trap entity, able to catch monsters and modify their behaviour
    /// </summary>
    public abstract class Trap : GameEngine.Tickable
    {
        /// <summary>
        /// Logic path point to which the trap is linked
        /// </summary>
        private Map.Points.TrapPoint point;

        /// <summary>
        /// Visual control, drawing the trap in its current state
        /// </summary>
        private View.Control.TrapControl control;

        /// <summary>
        /// Triggered when a monster arrives in the trap
        /// </summary>
        /// <param name="monster">Monster who arrived</param>
        public abstract void monsterArrived(Monster monster);

        /// <summary>
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        public void tick()
        {

        }
    }
}
