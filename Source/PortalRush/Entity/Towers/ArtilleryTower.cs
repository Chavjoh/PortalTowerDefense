using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity.Towers
{
    /// <summary>
    /// Artillery tower
    /// </summary>
    public class ArtilleryTower : Tower
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ArtilleryTower() : base()
        {
            this.attackRange = 240;
            this.attackSpeed = 20;
            this.attackDamage = 50;
            GameEngine.GameManager.Instance.gainMoney(-1 * ArtilleryTower.getPrice(1));
        }

        /// <summary>
        /// Get image path of the current tower
        /// </summary>
        /// <returns>Image path</returns>
        public override string image()
        {
            return "artillery.png";
        }

        /// <summary>
        /// Get the price of artillery tower at given level
        /// </summary>
        /// <param name="level">Level of wanted tower</param>
        /// <returns>Price of an artillery tower</returns>
        public static int getPrice(int level)
        {
            switch (level)
            {
                case 1:
                    return 100;
                case 2:
                    return 150;
                case 3:
                    return 150;
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
                GameEngine.GameManager.Instance.gainMoney(-1 * ArtilleryTower.getPrice(this.level));
            }
        }

        /// <summary>
        /// Get params for building bullet for this tower
        /// </summary>
        /// <param name="damageStrengh">Damage done by pure strengh</param>
        /// <param name="damageMagic">Damage done by magic power</param>
        /// <param name="damageRange">Range of grouped attack</param>
        /// <param name="image">Image to display for bullet</param>
        protected override void getBulletParams(ref int damageStrengh, ref int damageMagic, ref int damageRange, ref String image)
        {
            damageStrengh = this.attackDamage + (int)(this.attackDamage * 0.2 * (level - 1));
            damageMagic = 0;
            damageRange = level * 40;
            image = "artilleryBullet.png";
        }
    }
}
