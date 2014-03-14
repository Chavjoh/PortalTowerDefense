﻿using System;
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

namespace PortalRush.View.Control
{
    /// <summary>
    /// Logique d'interaction pour TrapControl.xaml
    /// Visual control for a trap
    /// </summary>
    public partial class TrapControl : UserControl, GameEngine.Dynamic
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TrapControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Change the image for the element
        /// </summary>
        /// <param name="index">Index of new image, referencing internal tab</param>
        public void changeImage(int index)
        {

        }

        /// <summary>
        /// Change the Z Index of the element
        /// </summary>
        /// <param name="index">Z-index of element in canvas</param>
        public void changeZIndex(int index)
        {

        }

        /// <summary>
        /// Move the element to a given location
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        public void move(int x, int y)
        {

        }
    }
}
