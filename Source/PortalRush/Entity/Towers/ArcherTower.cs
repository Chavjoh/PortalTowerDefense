﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PortalRush.Entity.Towers
{
    /// <summary>
    /// Archer tower
    /// </summary>
    public class ArcherTower : Tower
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ArcherTower() : base()
        {
            this.attackRange = 180;
            this.attackSpeed = 60;
            this.attackDamage = 15;
            GameEngine.GameManager.Instance.gainMoney(-1 * ArcherTower.getPrice(1));
        }

        /// <summary>
        /// Get image path of the current tower
        /// </summary>
        /// <returns>Image path</returns>
        public override string image()
        {
            return "archer.png";
        }

        /// <summary>
        /// Get the price of archer tower at given level
        /// </summary>
        /// <param name="level">Level of wanted tower</param>
        /// <returns>Price of an archer tower</returns>
        public static int getPrice(int level)
        {
            switch (level)
            {
                case 1:
                    return 60;
                case 2:
                    return 100;
                case 3:
                    return 200;
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
                GameEngine.GameManager.Instance.gainMoney(-1 * ArcherTower.getPrice(this.level));
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
            damageStrengh = this.attackDamage + (int)(this.attackDamage * 0.3 * (level - 1));
            damageMagic = 0;
            damageRange = 0;
            image = "archerBullet.png";
        }
    }
}
