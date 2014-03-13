using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.GameEngine
{
    interface Tickable
    {
        /// <summary>
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        void tick();
    }
}
