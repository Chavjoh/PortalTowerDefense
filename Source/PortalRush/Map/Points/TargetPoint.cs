using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map.Points
{
    /// <summary>
    /// Target point, placed at end of path and making player lose one life per passing monster
    /// </summary>
    class TargetPoint : Point
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        public TargetPoint(int x, int y)
            : base(x,y)
        {
        }

        /// <summary>
        /// Inherited from Point
        /// Get the move manager to use to go the current point
        /// </summary>
        /// <returns>MoveManager to assign to monster</returns>
        public override GameEngine.MoveManager getMoveManager()
        {
            return new GameEngine.MoveManagers.WalkMoveManager();
        }

        /// <summary>
        /// Inherited from Point
        /// Triggered when a monster arrives to the point
        /// </summary>
        /// <param name="monster">Monster who arrived</param>
        public override void monsterArrived(Entity.Monster monster)
        {
            GameEngine.GameManager.Instance.clockUnregister(monster);
        }
    }
}
