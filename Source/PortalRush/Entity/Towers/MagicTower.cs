﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity.Towers
{
    /// <summary>
    /// Magic tower
    /// </summary>
    public class MagicTower : Tower
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MagicTower() : base()
        {
            this.attackRange = 300;
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
                    return 160;
                case 2:
                    return 260;
                case 3:
                    return 340;
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
                this.attackDamage += (int)(this.attackDamage * 0.5);
                this.attackRange += (int)(this.attackRange * 0.2);
                this.attackSpeed += (int)(this.attackSpeed * 0.3);
                GameEngine.GameManager.Instance.gainMoney(-1 * MagicTower.getPrice(this.level));
            }
        }

        /// <summary>
        /// Sell the tower
        /// </summary>
        public override void sell()
        {
            foreach (Bullet bullet in this.bullets)
            {
                this.bulletsToDelete.Add(bullet);
                bullet.dispose();
            }
            int sellGain = (int) (MagicTower.getPrice(this.level) / 2.0);
            GameEngine.GameManager.Instance.gainMoney(sellGain);
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
            damageStrengh = 0;
            damageMagic = this.attackDamage;
            damageRange = 0;
            image = "magicBullet.png"; 
        }
    }
}
