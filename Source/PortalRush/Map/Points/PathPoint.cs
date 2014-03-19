using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map.Points
{
    /// <summary>
    /// Standard path point on which monster has to pass by walking
    /// </summary>
    class PathPoint : Point
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        /// <param name="orientation">Orientation given to monsters passing this point</param>
        /// <param name="zIndex">ZIndex given to monsters passing this point</param>
        public PathPoint(int x, int y, Entity.MonsterOrientation orientation = Entity.MonsterOrientation.NULL, int zIndex = -1)
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
    }
}
