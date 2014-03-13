using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        /// Main canvas for drawing game, into MainWindow
        /// </summary>
        private Canvas canvas;

        /// <summary>
        /// Linked map, currently played
        /// </summary>
        private Map.Map map;

        /// <summary>
        /// List of registered tickable objects, having to be ticked each 1/30 sec
        /// </summary>
        private List<Tickable> tickables;

        /// <summary>
        /// Main canvas for drawing game, into MainWindow
        /// </summary>
        public Canvas Canvas
        {
            get
            {
                return this.canvas;
            }
            set
            {
                this.canvas = value;
            }
        }

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
            // TEMP CODE, WAITING FOR XML PARSERS TO BE IN PLACE
            if (index == -1)
            {
                this.TEMP_LOAD_MAP();
            }
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

        /// <summary>
        /// TEMP FUNCTION, WAITING FOR XML PARSERS TO BE IN PLACE
        /// </summary>
        private void TEMP_LOAD_MAP()
        {
            this.map = new Map.Map(null);
        }
    }
}
