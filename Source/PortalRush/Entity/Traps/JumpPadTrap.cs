using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity.Traps
{
    /// <summary>
    /// JumpPad trap, making the monster fly out of the map
    /// </summary>
    public class JumpPadTrap : Trap
    {
        private Map.Point destinationPoint;

        private Random randomGenerator;

        /// <summary>
        /// Create a new JumpPad trap
        /// </summary>
        /// <param name="destinationPoint">Destination point where the monster is sent</param>
        public JumpPadTrap(Map.Points.TrapPoint trapPoint, Map.Point destinationPoint)
            : base(trapPoint)
        {
            this.destinationPoint = destinationPoint;
            this.randomGenerator = new Random();
        }

        /// <summary>
        /// Inherited from Trap
        /// Triggered when a monster arrives in the trap
        /// </summary>
        /// <param name="monster">Monster who arrived</param>
        public override void monsterArrived(Monster monster)
        {
            int randomNumber;

            lock ("randomJumpPadTrap") {
                randomNumber = randomGenerator.Next(0, 10);
            }

            if (randomNumber == 0)
            {
                this.Point.Next = this.destinationPoint;
                monster.MoveManager = new GameEngine.MoveManagers.WalkMoveManager();
            }
            
        }
    }
}
