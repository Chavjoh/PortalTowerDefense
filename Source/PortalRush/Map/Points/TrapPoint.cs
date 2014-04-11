using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map.Points
{
    /// <summary>
    /// Special point corresponding to a trap in which monster will fall when passing
    /// </summary>
    public class TrapPoint : Point
    {
        /// <summary>
        /// Linked trap, triggered when a monster passed on the point
        /// </summary>
        private Entity.Trap trap;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        /// <param name="orientation">Orientation given to monsters passing this point</param>
        /// <param name="zIndex">ZIndex given to monsters passing this point</param>
        public TrapPoint(int x, int y, Entity.MonsterOrientation orientation = Entity.MonsterOrientation.NULL, int zIndex = -1)
            : base(x,y,orientation,zIndex)
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
            base.monsterArrived(monster);
        }
    }
}
