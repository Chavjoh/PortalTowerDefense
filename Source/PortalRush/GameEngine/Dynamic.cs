using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.GameEngine
{
    /// <summary>
    /// Dynamic interface, for views which have to be updated
    /// </summary>
    interface Dynamic
    {
        /// <summary>
        /// Change the image for the element
        /// </summary>
        /// <param name="index">Index of new image, referencing internal tab in each implementing class</param>
        void changeImage(int index);

        /// <summary>
        /// Move the element to a given location
        /// </summary>
        /// <param name="x">X position on screen</param>
        /// <param name="y">Y position on screen</param>
        void move(int x, int y);
    }
}
