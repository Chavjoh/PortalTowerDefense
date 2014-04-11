using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity.Towers
{
    /// <summary>
    /// Magic tower
    /// </summary>
    class MagicTower : Tower
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MagicTower() : base()
        {
            this.attackRange = 200;
            this.attackSpeed = 30;
            this.attackDamage = 20;
            GameEngine.GameManager.Instance.gainMoney(-1 * MagicTower.getPrice(1));
        }

        /// <summary>
        /// Get image path of the current tower
        /// </summary>
        /// <returns>Image path</returns>
        public override string image()
        {
            return "magic.png";
        }

        /// <summary>
        /// Get the price of magic tower at given level
        /// </summary>
        /// <param name="level">Level of wanted tower</param>
        /// <returns>Price of a magic tower</returns>
        public static int getPrice(int level)
        {
            switch (level)
            {
                case 1:
                    return 80;
                case 2:
                    return 180;
                case 3:
                    return 240;
                default:
                    return 1000000;
            }
        }

        /// <summary>
        /// Inherited from Tower
        /// Upgrade the tower to its next level
        /// </summary>
        public override void upgrade()
        {
            if (this.level < 3)
            {
                this.level++;
                GameEngine.GameManager.Instance.gainMoney(-1 * MagicTower.getPrice(this.level));
            }
        }
    }
}
