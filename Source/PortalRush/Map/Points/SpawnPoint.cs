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
    public class SpawnPoint : Point, GameEngine.Tickable
    {
        /// <summary>
        /// List of times (in ticks) > MonsterType for spawning monsters during game
        /// </summary>
        private Dictionary<int, Entity.MonsterType> spawners;

        /// <summary>
        /// Number of ticks elapsed since the beginning of the game
        /// </summary>
        private int elapsedTicks;

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
            // Local variables
            this.spawners = new Dictionary<int, Entity.MonsterType>();
            this.elapsedTicks = 0;

            // Register as a clock
            GameEngine.GameManager.Instance.clockRegister(this);
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
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        public void tick()
        {
            if (this.spawners.Keys.Contains(this.elapsedTicks))
            {
                Entity.MonsterType monsterType = this.spawners[this.elapsedTicks];
                this.spawners.Remove(this.elapsedTicks);
                Entity.Monster monster = new Entity.Monster(monsterType, this);
                GameEngine.GameManager.Instance.Map.NewMonster = monster;
            }
            this.elapsedTicks++;
        }

        /// <summary>
        /// Register a monster type to be spawned at a particular time
        /// </summary>
        /// <param name="time">Number of ticks from game beginning at which spawn monster</param>
        /// <param name="type">Type of monster to spawn at given type</param>
        public void spawn(int time, Entity.MonsterType type)
        {
            this.spawners.Add(time, type);
        }

        /// <summary>
        /// Get the location corresponding to the exact center of this point
        /// </summary>
        /// <returns></returns>
        public Location getExactLocation()
        {
            return new Location(this, X, Y);
        }
    }
}
