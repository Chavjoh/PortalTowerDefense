﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity.Towers
{
    /// <summary>
    /// Artillery tower
    /// </summary>
    class ArtilleryTower : Tower
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ArtilleryTower() : base()
        {
            // TODO : set this.attackRange, this.attackSpeed, this.attackDamage
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

        }
    }
}
