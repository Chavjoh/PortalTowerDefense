using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

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
        private List<Tickable> clocks;

        /// <summary>
        /// Game loop self-containing thread
        /// </summary>
        private Thread gameLoop = null;

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
        /// Default constructor
        /// </summary>
        public GameManager()
        {
            this.clocks = new List<Tickable>();
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
            this.gameLoop = new Thread(clock);
            this.gameLoop.Start();
        }

        /// <summary>
        /// Register an element to be ticked every 1/30 sec
        /// </summary>
        /// <param name="child">Element to register</param>
        public void clockRegister(Tickable child)
        {
            this.clocks.Add(child);
        }

        /// <summary>
        /// Stop main game loop and free resources
        /// </summary>
        public void quit()
        {
            if (this.gameLoop != null)
            {
                this.gameLoop.Abort();
            }
        }

        /// <summary>
        /// Clock function, managing clock calls each 1/30 second
        /// </summary>
        private void clock()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (true)
            {
                if (watch.ElapsedMilliseconds >= 16)
                {
                    watch.Restart();
                    this.tick();
                }
            }
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
