using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PortalRush.Entity
{
    /// <summary>
    /// Generic tower entity, attacking spotted monsters
    /// </summary>
    public abstract class Tower : GameEngine.Tickable
    {
        /// <summary>
        /// Attack range, in screen units
        /// </summary>
        protected int attackRange;

        /// <summary>
        /// Attack speed, elapsed time between 2 bullets being thrown
        /// </summary>
        protected int attackSpeed;

        /// <summary>
        /// Damages made to monster targetted monster
        /// Subkind of tower decides wether this value concerns magic or non-magic damages
        /// </summary>
        protected int attackDamage;

        /// <summary>
        /// Ticks counter for triggering bullets launches
        /// </summary>
        private int attackCounter;

        /// <summary>
        /// Current tower level
        /// </summary>
        protected int level;

        /// <summary>
        /// List of monster currently in attack range (spotted)
        /// </summary>
        private List<Monster> attackedMonsters;

        /// <summary>
        /// Linked location, on which the tower is placed
        /// </summary>
        private Map.TowerLocation location;

        /// <summary>
        /// Linked bullets, thrown by tower and having not reached monster yet
        /// </summary>
        private List<Bullet> bullets;

        /// <summary>
        /// Linked bullets, to delete
        /// </summary>
        private List<Bullet> bulletsToDelete;

        /// <summary>
        /// Visual control, drawing the tower at its location
        /// </summary>
        protected View.Control.TowerControl control;

        /// <summary>
        /// Current tower level
        /// </summary>
        public int Level
        {
            get
            {
                return this.level;
            }
        }

        /// <summary>
        /// Linked location, on which the tower is placed
        /// </summary>
        public Map.TowerLocation Location
        {
            get
            {
                return this.location;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Tower()
        {
            // Set level
            this.level = 1;

            // Create lists
            this.attackedMonsters = new List<Monster>();
            this.bullets = new List<Bullet>();
            this.bulletsToDelete = new List<Bullet>();

            // No location linked at creation
            this.location = null;

            // Counter of ticks for attack at 0
            this.attackCounter = 0;

            // Prepare visual control
            String image = Map.TowerLocation.BaseFolder + this.image();

            // Add visual control to UI
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control = new View.Control.TowerControl(image);
                this.control.changeDelta(36, 50);
            });

            // Add to Tickable list
            GameEngine.GameManager.Instance.clockRegister(this);
        }

        /// <summary>
        /// Default destructor
        /// </summary>
        public void dispose()
        {
            GameEngine.GameManager.Instance.clockUnregister(this);
            this.control.dispose();
        }

        /// <summary>
        /// Get image path of the current tower
        /// </summary>
        /// <returns>Image path</returns>
        public abstract string image();

        /// <summary>
        /// Upgrade the tower to its next level
        /// </summary>
        public abstract void upgrade();

        /// <summary>
        /// Get params for building bullet for this tower
        /// </summary>
        /// <param name="damageStrengh">Damage done by pure strengh</param>
        /// <param name="damageMagic">Damage done by magic power</param>
        /// <param name="damageRange">Range of grouped attack</param>
        /// <param name="image">Image to display for bullet</param>
        protected abstract void getBulletParams(ref int damageStrengh, ref int damageMagic, ref int damageRange, ref String image);

        /// <summary>
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        public void tick()
        {
            // Actualize list of targettable monsters
            this.attackedMonsters.Clear();
            foreach (Monster monster in GameEngine.GameManager.Instance.Map.monstersInRange(this.location.X, this.location.Y, this.attackRange))
            {
                this.attackedMonsters.Add(monster);
            }

            // Move existing bullets
            foreach (Bullet bullet in this.bullets)
            {
                bullet.move();
            }

            // Delete bullets if needed
            foreach (Bullet bullet in this.bulletsToDelete)
            {
                this.bullets.Remove(bullet);
            }
            this.bulletsToDelete.Clear();

            // Check if bullet has to be thrown
            this.attackCounter--;
            if (this.attackedMonsters.Count > 0)
            {
                if (this.attackCounter <= 0)
                {
                    this.attackCounter = 1800 / this.attackSpeed;
                    this.attack();
                }
            }
            else
            {
                if (this.attackCounter <= 0)
                {
                    this.attackCounter = 0;
                }
            }
        }

        /// <summary>
        /// Place the tower on a map location
        /// </summary>
        /// <param name="location">Location on which place the tower</param>
        public void place(Map.TowerLocation location)
        {
            // Assign location
            this.location = location;

            // Move visual control
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control.move(this.location.X, this.location.Y);
                this.control.changeZIndex(this.location.LayerIndex);
            });
        }

        /// <summary>
        /// Cancel bullets targeting a specific monster, because he walked through a portal
        /// </summary>
        /// <param name="monster">Monster targeted by bullets</param>
        public void cancelBulletsTargeting(Monster monster)
        {
            foreach (Bullet bullet in this.bullets)
            {
                if (bullet.Target == monster)
                {
                    this.bulletsToDelete.Add(bullet);
                    bullet.dispose();
                }
            }
        }

        /// <summary>
        /// Throw a bullet to the nearest-from-target monster currently spotted
        /// </summary>
        private void attack()
        {
            // Determine which monster is the nearest from target
            Monster target = this.attackedMonsters[0];
            foreach (Monster monster in this.attackedMonsters)
            {
                if (monster.distanceToTarget() < target.distanceToTarget())
                {
                    target = monster;
                }
            }

            // Attack this monster
            int damageStrengh = 0;
            int damageMagic = 0;
            int damageRange = 0;
            String image = null;
            this.getBulletParams(ref damageStrengh, ref damageMagic, ref damageRange, ref image);
            this.bullets.Add(new Bullet(damageStrengh, damageMagic, damageRange, this, target, image));
        }
    }
}
