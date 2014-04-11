using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PortalRush.Entity
{
    /// <summary>
    /// Generic monster, going along the path
    /// </summary>
    public class Monster : GameEngine.Tickable
    {
        /// <summary>
        /// Base folder for monsters images
        /// </summary>
        private static String baseFolder;

        /// <summary>
        /// Base folder for monsters images
        /// </summary>
        public static String BaseFolder
        {
            set
            {
                baseFolder = value;
            }
        }

        /// <summary>
        /// Non-magic armor level
        /// </summary>
        protected int strenghArmor;

        /// <summary>
        /// Magic armor level
        /// </summary>
        protected int magicArmor;

        /// <summary>
        /// X position on screen
        /// </summary>
        private double x;

        /// <summary>
        /// Y position on screen
        /// </summary>
        private double y;

        /// <summary>
        /// Current health of monster
        /// </summary>
        private int health;

        /// <summary>
        /// Money got when monster dies
        /// </summary>
        private int money;

        /// <summary>
        /// Type of monster
        /// </summary>
        private MonsterType type;

        /// <summary>
        /// Linked MoveManager, which to call when having to move (1/30 sec)
        /// </summary>
        private GameEngine.MoveManager moveManager;

        /// <summary>
        /// Linked Location, which to have as a target when moving
        /// </summary>
        private Map.Location location;

        /// <summary>
        /// Current display orientation
        /// </summary>
        private MonsterOrientation orientation;

        /// <summary>
        /// Visual control, displaying the monster on the map
        /// </summary>
        private View.Control.MonsterControl control;

        /// <summary>
        /// Current walking step
        /// </summary>
        private int step;

        /// <summary>
        /// Tick counter for changing steps
        /// </summary>
        private int stepCounter;

        /// <summary>
        /// X position on screen
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Y position on screen
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Linked Location, which to have as a target when moving
        /// </summary>
        public Map.Location Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        /// <summary>
        /// Linked MoveManager, which to call when having to move (1/30 sec)
        /// </summary>
        public GameEngine.MoveManager MoveManager
        {
            set
            {
                this.moveManager = value;
                this.moveManager.Monster = this;
            }
        }

        /// <summary>
        /// Current display orientation
        /// </summary>
        public MonsterOrientation Orientation
        {
            set
            {
                this.orientation = value;
                this.updateStep();
            }
        }

        /// <summary>
        /// Z-Index on canvas of displayed element
        /// </summary>
        public int ZIndex
        {
            set
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate()
                {
                    this.control.changeZIndex(value);
                });
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="type">Type of monster to build</param>
        public Monster(MonsterType type, Map.Points.SpawnPoint spawnPoint)
        {
            // Assign type
            this.type = type;

            /// Default values
            this.moveManager = null;
            this.location = null;
            this.orientation = MonsterOrientation.NULL;
            this.step = 1;
            this.stepCounter = 1;

            // Basic location (spawn point)
            this.x = spawnPoint.X;
            this.y = spawnPoint.Y;

            // Type-dependant values
            String name;
            switch (type)
            {
                case MonsterType.CHELL:
                    this.strenghArmor = 20;
                    this.magicArmor = 5;
                    this.health = 100;
                    this.money = 10;
                    name = "Chell\\";
                    break;
                default:
                    this.strenghArmor = 0;
                    this.magicArmor = 0;
                    this.health = 1;
                    this.money = 0;
                    name = "";
                    break;
            }

            // Prepare visual control
            Dictionary<int,String> images = new Dictionary<int,string>();
            images.Add(11, Monster.baseFolder + name + "fl1.png");
            images.Add(12, Monster.baseFolder + name + "fl2.png");
            images.Add(13, Monster.baseFolder + name + "fl3.png");
            images.Add(14, Monster.baseFolder + name + "fl4.png");
            images.Add(21, Monster.baseFolder + name + "fr1.png");
            images.Add(22, Monster.baseFolder + name + "fr2.png");
            images.Add(23, Monster.baseFolder + name + "fr3.png");
            images.Add(24, Monster.baseFolder + name + "fr4.png");
            images.Add(31, Monster.baseFolder + name + "rl1.png");
            images.Add(32, Monster.baseFolder + name + "rl2.png");
            images.Add(33, Monster.baseFolder + name + "rl3.png");
            images.Add(34, Monster.baseFolder + name + "rl4.png");
            images.Add(41, Monster.baseFolder + name + "rr1.png");
            images.Add(42, Monster.baseFolder + name + "rr2.png");
            images.Add(43, Monster.baseFolder + name + "rr3.png");
            images.Add(44, Monster.baseFolder + name + "rr4.png");
            
            // Add visual control to UI
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control = new View.Control.MonsterControl(images,27,68);
                this.control.move(x, y);
                this.control.changeImage(11);
                this.control.changeZIndex(5);
            });

            // Associate linked location
            this.location = spawnPoint.getExactLocation();
            this.moveManager = null;

            // Add to Tickable list
            GameEngine.GameManager.Instance.clockRegister(this);
        }

        /// <summary>
        /// Default destructor
        /// </summary>
        public void dispose()
        {
            GameEngine.GameManager.Instance.clockUnregister(this);
            Application.Current.Dispatcher.BeginInvoke((Action)delegate(){
                this.control.dispose();
            });
            GameEngine.GameManager.Instance.Map.OldMonster = this;
        }

        /// <summary>
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        public void tick()
        {
            // Update step value
            this.stepCounter--;
            if (this.stepCounter == 0)
            {
                stepCounter = 5;
                this.step++;
                if (this.step > 4)
                {
                    this.step = 1;
                }
                this.updateStep();
            }

            // If point reached, head to next
            if (this.location.isReached(this.x, this.y))
            {
                this.location.Point.monsterArrived(this);
            }

            // Move to next point
            else
            {
                this.moveManager.move();
            }
        }

        /// <summary>
        /// Get distance to final target
        /// </summary>
        /// <returns>Distance to final TargetPoint</returns>
        public int distanceToTarget()
        {
            return (int)Math.Sqrt(Math.Pow(this.x - this.location.Point.X, 2) + Math.Pow(this.y - this.location.Point.Y, 2)) + this.location.Point.DistanceToTarget;
        }

        /// <summary>
        /// Move monster to specified location
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        public void move(double x, double y)
        {
            this.x = x;
            this.y = y;

            // Add visual control to UI
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control.move(x, y);
            });
            
        }

        /// <summary>
        /// Infligate brut damages, must be computed with local armors
        /// </summary>
        /// <param name="damageStrengh">Pure strengh damage</param>
        /// <param name="damageMagic">Magic power damage</param>
        public void infligate(int damageStrengh, int damageMagic)
        {
            damageStrengh -= (this.strenghArmor / 100) * damageStrengh;
            damageMagic -= (this.magicArmor / 100) * damageMagic;
            this.health -= (damageStrengh + damageMagic);
            if (this.health <= 0)
            {
                GameEngine.GameManager.Instance.gainMoney(this.money);
                this.dispose();
            }
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control.setLife(this.health);
            });
        }

        /// <summary>
        /// Update control image, based on current step and orientation
        /// </summary>
        private void updateStep()
        {
            int index = 0;
            switch (this.orientation)
            {
                case MonsterOrientation.FRONT_LEFT:
                    index = 10;
                    break;
                case MonsterOrientation.FRONT_RIGHT:
                    index = 20;
                    break;
                case MonsterOrientation.REAR_LEFT:
                    index = 30;
                    break;
                case MonsterOrientation.REAR_RIGHT:
                    index = 40;
                    break;
                default:
                    index = 0;
                    break;
            }
            index += this.step;
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.control.changeImage(index);
            });
        }
    }
}
