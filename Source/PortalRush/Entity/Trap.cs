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
        /// Base folder for trap location image
        /// </summary>
        private static String baseFolder;

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

        /// <summary>
        /// Base folder for trap location image
        /// </summary>
        public static String BaseFolder
        {
            get
            {
                return baseFolder;
            }
            set
            {
                baseFolder = value;
            }
        }

        /// <summary>
        /// Logic path point to which the trap is linked
        /// </summary>
        protected Map.Points.TrapPoint Point
        {
            get { return this.point; }
            set { this.point = value; }
        }

        /// <summary>
        /// Visual control, drawing the trap in its current state
        /// </summary>
        protected View.Control.TrapControl Control
        {
            get { return this.control; }
            set { this.control = value; }
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

        /// <summary>
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.) to know if game is finished
        /// </summary>
        /// <returns>true if game is finished</returns>
        public bool gameFinished()
        {
            return true;
        }
    }
}
