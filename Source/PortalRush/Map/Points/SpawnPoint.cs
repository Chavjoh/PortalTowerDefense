using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Map.Points
{
    /// <summary>
    /// Spawn point, creating new monsters at configured intervals
    /// </summary>
    class SpawnPoint : Point, GameEngine.Tickable
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        /// <param name="orientation">Orientation given to monsters passing this point</param>
        /// <param name="zIndex">ZIndex given to monsters passing this point</param>
        public SpawnPoint(int x, int y, Entity.MonsterOrientation orientation = Entity.MonsterOrientation.NULL, int zIndex = -1)
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
            return null;
        }

        /// <summary>
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        public void tick()
        {

        }
    }
}
