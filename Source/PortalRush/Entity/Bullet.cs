using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PortalRush.Entity
{
    /// <summary>
    /// Bullet thrown by a tower to attack a monster
    /// </summary>
    public class Bullet
    {
        /// <summary>
        /// Damage level for non-magic attack
        /// </summary>
        private int damageStrengh;

        /// <summary>
        /// Damage level for a magic attack
        /// </summary>
        private int damageMagic;

        /// <summary>
        /// Range for group damages
        /// Should be set to 0 for non-ranged attack
        /// </summary>
        private int damageRange;

        /// <summary>
        /// X position on screen
        /// </summary>
        private double x;

        /// <summary>
        /// Y position on screen
        /// </summary>
        private double y;

        /// <summary>
        /// Main target monster
        /// </summary>
        private Monster target;

        /// <summary>
        /// Visual control, drawing the bullet at its current location
        /// </summary>
        private View.Control.BulletControl control;

        /// <summary>
        /// Main target monster
        /// </summary>
        public Monster Target
        {
            get
            {
                return this.target;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="damageStrengh">Damage done by normal strengh</param>
        /// <param name="damageMagic">Damage done by magic power</param>
        /// <param name="damageRange">Ranged damages, 0 for nothing</param>
        /// <param name="launcher">Tower which launched the bullet</param>
        /// <param name="target">Target of bullet</param>
        /// <param name="image">Image of bullet</param>
        public Bullet(int damageStrengh, int damageMagic, int damageRange, Tower launcher, Monster target, String image)
        {
            // Assign attack data
            this.damageStrengh = damageStrengh;
            this.damageMagic = damageMagic;
            this.damageRange = damageRange;
            this.x = launcher.Location.X;
            this.y = launcher.Location.Y;
            this.target = target;

            // Prepare visual control
            image = Map.TowerLocation.BaseFolder + image;

            // Add visual control to UI
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control = new View.Control.BulletControl(image);
                this.control.move(this.x, this.y);
                this.control.changeZIndex(10000);
            });
        }

        /// <summary>
        /// Default destructor
        /// </summary>
        public void dispose()
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control.dispose();
            });
        }

        /// <summary>
        /// Move the bullet to its next position (1/30 sec)
        /// </summary>
        public void move()
        {
            double dist = Math.Sqrt(Math.Pow(this.x - target.X, 2) + Math.Pow(this.y - target.Y + 15, 2));
            if (dist < 5)
            {
                this.targetReached();
            }
            else
            {
                double x = (target.X - this.x) / dist * 5;
                double y = (target.Y - this.y - 15) / dist * 5;
                this.x += x;
                this.y += y;
                Application.Current.Dispatcher.BeginInvoke((Action)delegate()
                {
                    this.control.move(this.x, this.y);
                });
            }
        }

        /// <summary>
        /// Bullet has reached its target and should damage concerned monsters
        /// </summary>
        private void targetReached()
        {
            this.target.infligate(this.damageStrengh, this.damageMagic);
            GameEngine.GameManager.Instance.Map.cancelBulletsTargeting(this.target);
        }
    }
}
