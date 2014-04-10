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
            
            // TODO : set this.attackRange, this.attackSpeed, this.attackDamage
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
            return 0;
        }

        /// <summary>
        /// Inherited from Tower
        /// Upgrade the tower to its next level
        /// </summary>
        public override void upgrade()
        {

        }
    }
}
