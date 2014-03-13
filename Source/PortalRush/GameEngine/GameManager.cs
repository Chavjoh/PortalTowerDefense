using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.GameEngine
{
    /// <summary>
    /// General game manager, containing main game loop
    /// </summary>
    class GameManager
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        private static GameManager instance = null;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Current player money
        /// </summary>
        private int money;

        /// <summary>
        /// Current player remaining lifes
        /// </summary>
        private int lifes;

        /// <summary>
        /// Linked map, currently played
        /// </summary>
        private Map.Map map;

        /// <summary>
        /// List of registered tickable objects, having to be ticked each 1/30 sec
        /// </summary>
        private List<Tickable> tickables;

        /// <summary>
        /// Load configuration from fixed file
        /// Should be replaced in the complete version by the main menu
        /// </summary>
        public void loadConfig()
        {

        }

        /// <summary>
        /// Load the selected map
        /// </summary>
        /// <param name="index">ID of selected map</param>
        public void loadMap(int index)
        {

        }

        /// <summary>
        /// Main loop of the game engine
        /// </summary>
        public void play()
        {
            
        }

        /// <summary>
        /// Main loop trigger, called each 1/30 second
        /// </summary>
        private void tick()
        {

        }
    }
}
