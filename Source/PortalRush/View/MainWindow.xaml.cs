﻿using PortalRush.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PortalRush
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// Main view window containing application
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Register canvas in GameManager
            GameEngine.GameManager.Instance.Canvas = this.mainCanvas;

            // Launch default configuration
            GameEngine.GameManager.Instance.loadConfig();

            try
            {
                // Load default map
                GameEngine.GameManager.Instance.loadMap("Static");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Map loading error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Set window size using map size
            this.Width = GameEngine.GameManager.Instance.Map.Width;
            this.Height = GameEngine.GameManager.Instance.Map.Height + 121; // 121 = Header size
        }

        /// <summary>
        /// Clear all remaining menus on MainWindow
        /// </summary>
        public void clearAllMenus()
        {
            // Delete remaining menu(s) of towers
            List<UIElement> listDelete = new List<UIElement>();
            foreach (UIElement uiElement in this.mainGrid.Children)
            {
                if (uiElement is View.Menu.NoTowerMenu)
                {
                    listDelete.Add(uiElement);
                }
                else if (uiElement is View.Menu.TowerMenu)
                {
                    listDelete.Add(uiElement);
                }
            }
            foreach (UIElement uiElement in listDelete)
            {
                this.mainGrid.Children.Remove(uiElement);
            }
        }

        /// <summary>
        /// Closing procedure, must stop subthreads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            GameEngine.GameManager.Instance.quit();
        }

        /// <summary>
        /// Click event on canvas, managing for child tower locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Delete remaining menu(s) of towers
            this.clearAllMenus();

            // Search for clicked tower location
            foreach (UIElement uiElement in this.mainCanvas.Children)
            {
                View.Control.TowerLocationControl control = uiElement as View.Control.TowerLocationControl;
                if (control != null)
                {
                    Point position = e.GetPosition(control);
                    if (position.X >= 0 && position.Y >= 0 && position.X < control.ActualWidth && position.Y < control.ActualHeight)
                    {
                        control.click();
                        return;
                    }
                }
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Launch game
            GameEngine.GameManager.Instance.play();

            // Remove button
            this.mainGrid.Children.Remove(this.startButton);
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Window aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void startImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Patte");
        }
    }
}
