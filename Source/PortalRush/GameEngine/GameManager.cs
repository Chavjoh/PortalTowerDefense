﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PortalRush.GameEngine
{
    /// <summary>
    /// General game manager, containing main game loop
    /// </summary>
    public class GameManager
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
        /// Label for displaying lifes in
        /// </summary>
        private Label labelLifes;

        /// <summary>
        /// Label for displaying money in
        /// </summary>
        private Label labelMoney;

        /// <summary>
        /// Linked map, currently played
        /// </summary>
        private Map.Map map;

        /// <summary>
        /// List of registered tickable objects, having to be ticked each 1/30 sec
        /// </summary>
        private List<Tickable> clocks;

        /// <summary>
        /// List of newly registered tickable objects
        /// </summary>
        private List<Tickable> clocksNew;

        /// <summary>
        /// List of soon deleted tickable objects
        /// </summary>
        private List<Tickable> clocksOld;

        /// <summary>
        /// Game loop self-containing thread
        /// </summary>
        private Thread gameLoop = null;

        /// <summary>
        /// Marker for preventing quitting in first loop
        /// </summary>
        private bool justStarted;

        /// <summary>
        /// Game maps folder path
        /// </summary>
        private static string mapFolder = AppDomain.CurrentDomain.BaseDirectory + "Maps\\";

        /// <summary>
        /// Game maps configuration file extension
        /// </summary>
        private static string mapExtension = ".xml";

        /// <summary>
        /// Linked map, currently played
        /// </summary>
        public Map.Map Map
        {
            get
            {
                return this.map;
            }
        }

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

                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

                labelLifes = new Label();
                labelLifes.Width = 100;
                labelLifes.FontSize = 14;
                mainWindow.mainGrid.Children.Add(labelLifes);
                Grid.SetRow(labelLifes, 0);
                Grid.SetColumn(labelLifes, 0);
                labelLifes.VerticalAlignment = VerticalAlignment.Top;
                labelLifes.HorizontalAlignment = HorizontalAlignment.Left;
                labelLifes.Margin = new Thickness(130, 15, 0, 0);

                labelMoney = new Label();
                labelMoney.Width = 100;
                labelMoney.FontSize = 14;
                mainWindow.mainGrid.Children.Add(labelMoney);
                Grid.SetRow(labelMoney, 0);
                Grid.SetColumn(labelMoney, 0);
                labelMoney.VerticalAlignment = VerticalAlignment.Top;
                labelMoney.HorizontalAlignment = HorizontalAlignment.Left;
                labelMoney.Margin = new Thickness(130, 75, 0, 0);

                this.updateLabels();
            }
        }

        /// <summary>
        /// Current player remaining lifes
        /// </summary>
        public int Lifes
        {
            get { return this.lifes; }
            set { this.lifes = value; }
        }

        /// <summary>
        /// Current player money
        /// </summary>
        public int Money
        {
            get { return this.money; }
            set { this.money = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameManager()
        {
            this.clocks = new List<Tickable>();
            this.clocksNew = new List<Tickable>();
            this.clocksOld = new List<Tickable>();
            this.justStarted = true;
        }

        /// <summary>
        /// Load configuration from fixed file
        /// Should be replaced in the complete version by the main menu
        /// </summary>
        public void loadConfig()
        {
            // Nothing here for this version
        }

        /// <summary>
        /// Load the selected map
        /// </summary>
        /// <param name="index">ID of selected map</param>
        public void loadMap(string name)
        {
            this.map = new Map.Map(mapFolder + name + "\\" + name + mapExtension);
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
        /// Stop the loop of the game engine
        /// </summary>
        public void stop()
        {
            this.gameLoop.Abort();
        }

        /// <summary>
        /// Register an element to be ticked every 1/30 sec
        /// </summary>
        /// <param name="child">Element to register</param>
        public void clockRegister(Tickable child)
        {
            this.clocksNew.Add(child);
        }

        /// <summary>
        /// Unregister an ticked element.
        /// Used because it's impossible to delete an array element during array iteration.
        /// </summary>
        /// <param name="child">Element to unregister</param>
        public void clockUnregister(Tickable child)
        {
            this.clocksOld.Add(child);
        }

        /// <summary>
        /// Check if player can buy a thing or not
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public bool canBuy(int cost)
        {
            return cost <= this.money;
        }

        /// <summary>
        /// Gain (+) or lose (-) money
        /// </summary>
        /// <param name="money"></param>
        public void gainMoney(int money)
        {
            this.money += money;
            this.updateLabels();
        }

        /// <summary>
        /// Lose a life
        /// </summary>
        public void lifeMinus()
        {
            this.lifes--;
            this.updateLabels();
            if (this.lifes <= 0)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate()
                {
                    this.stop();
                    MessageBox.Show("Vous ferez mieux la prochaine fois...", "Game Over", MessageBoxButton.OK);
                    Application.Current.MainWindow.Close();
                });
            }
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
        /// Update money and life labels content
        /// </summary>
        public void updateLabels()
        {
            // Update labels
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                this.labelMoney.Content = "Money : " + this.money;
                this.labelLifes.Content = "Lifes : " + this.lifes;
            });
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
                if (watch.ElapsedMilliseconds >= 33)
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
            bool gameFinished = !justStarted;
            foreach (Tickable tickable in this.clocks)
            {
                tickable.tick();
                if (!tickable.gameFinished())
                {
                    gameFinished = false;
                }
            }
            foreach (Tickable newTickable in this.clocksNew)
            {
                this.clocks.Add(newTickable);
            }
            this.clocksNew.Clear();
            foreach (Tickable oldTickable in this.clocksOld)
            {
                this.clocks.Remove(oldTickable);
            }
            this.clocksOld.Clear();
            this.updateLabels();
            if (gameFinished)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate()
                {
                    this.stop();
                    MessageBox.Show("Félicitations, allez prendre un peu de repos", "Game Finished", MessageBoxButton.OK);
                    Application.Current.MainWindow.Close();
                });
            }
            justStarted = false;
        }
    }
}
