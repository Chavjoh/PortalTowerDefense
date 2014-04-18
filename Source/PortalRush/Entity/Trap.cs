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
        #region Attributes

        /// <summary>
        /// Logic path point to which the trap is linked
        /// </summary>
        private Map.Points.TrapPoint point;

        /// <summary>
        /// Visual control, drawing the trap in its current state
        /// </summary>
        private View.Control.TrapControl control;

        #endregion 

        #region Properties
        
        protected Map.Points.TrapPoint Point
        {
            get { return this.point; }
        }

        protected View.Control.TrapControl Control
        {
            get { return this.control; }
        }

        #endregion

        /// <summary>
        /// Constructor of a new Trap
        /// </summary>
        /// <param name="trapPoint">Corresponding point with the trap is linked</param>
        public Trap(Map.Points.TrapPoint trapPoint)
        {
            this.point = trapPoint;
        }

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
