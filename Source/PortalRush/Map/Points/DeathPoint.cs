using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map.Points
{
    public class DeathPoint : Point
    {
        #region Attributes 

        /// <summary>
        /// Move manager to the DeathPoint
        /// </summary>
        GameEngine.MoveManager moveManager;

        #endregion

        #region Properties

        public GameEngine.MoveManager MoveManager
        {
            get { return this.moveManager; }
            set { this.moveManager = value; }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        public DeathPoint(int x, int y)
            : base(x,y)
        {
            this.moveManager = new GameEngine.MoveManagers.WalkMoveManager();
        }

        /// <summary>
        /// Inherited from Point
        /// Get the move manager to use to go the current point
        /// </summary>
        /// <returns>MoveManager to assign to monster</returns>
        public override GameEngine.MoveManager getMoveManager()
        {
            return this.moveManager;
        }

        /// <summary>
        /// Inherited from Point
        /// Triggered when a monster arrives to the point
        /// </summary>
        /// <param name="monster">Monster who arrived</param>
        public override void monsterArrived(Entity.Monster monster)
        {
            monster.dispose();
        }
    }
}
