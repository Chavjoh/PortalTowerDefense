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
    class Monster : GameEngine.Tickable
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
                baseFolder = AppDomain.CurrentDomain.BaseDirectory + value;
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
        private int x;

        /// <summary>
        /// Y position on screen
        /// </summary>
        private int y;

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
        /// Default constructor
        /// </summary>
        /// <param name="type">Type of monster to build</param>
        public Monster(MonsterType type, int x, int y)
        {
            // Assign type
            this.type = type;

            /// Default values
            this.moveManager = null;
            this.location = null;
            this.orientation = MonsterOrientation.NULL;

            // Basic location (spawn point)
            this.x = x;
            this.y = y;

            // Type-dependant values
            String name;
            switch (type)
            {
                case MonsterType.CHELL:
                    this.strenghArmor = 20;
                    this.magicArmor = 5;
                    this.health = 100;
                    this.money = 5;
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
        }

        /// <summary>
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        public void tick()
        {

        }

        /// <summary>
        /// Get distance to final target
        /// </summary>
        /// <returns>Distance to final TargetPoint</returns>
        public int distanceToTarget()
        {
            return 0;
        }
    }
}
