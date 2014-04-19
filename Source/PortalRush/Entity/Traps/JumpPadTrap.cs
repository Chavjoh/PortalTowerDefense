using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PortalRush.Entity.Traps
{
    /// <summary>
    /// JumpPad trap, making the monster fly out of the map
    /// </summary>
    public class JumpPadTrap : Trap
    {
        /// <summary>
        /// Destination point after hitting the jumpPad
        /// </summary>
        private Map.Points.DeathPoint destinationPoint;

        /// <summary>
        /// Random number generator
        /// </summary>
        private Random randomGenerator;

        /// <summary>
        /// Create a new JumpPad trap
        /// </summary>
        /// <param name="destinationPoint">Destination point where the monster is sent</param>
        public JumpPadTrap(Map.Points.TrapPoint trapPoint, Map.Points.DeathPoint destinationPoint, int zIndex)
            : base(trapPoint)
        {
            this.destinationPoint = destinationPoint;
            this.randomGenerator = new Random();

            // Prepare visual control
            String image = Trap.BaseFolder + "jumppad.png";

            // Add visual control to UI
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.Control = new View.Control.TrapControl(image);
                this.Control.changeZIndex(zIndex);
                this.Control.move(trapPoint.X, trapPoint.Y);
                this.Control.changeDelta(31, 18);
            });
        }

        /// <summary>
        /// Inherited from Trap
        /// Triggered when a monster arrives in the trap
        /// </summary>
        /// <param name="monster">Monster who arrived</param>
        public override void monsterArrived(Monster monster)
        {
            int randomNumber;

            lock ("jumpPadTrap")
            {
                randomNumber = randomGenerator.Next(0, 10);
                //Console.WriteLine(randomNumber);
            }

            if (randomNumber == 0)
            {
                this.destinationPoint.MoveManager = new GameEngine.MoveManagers.BallisticMoveManager(this.Point, this.destinationPoint);
                monster.ForceNextPoint = (Map.Point) this.destinationPoint;
            }
        }
    }
}
