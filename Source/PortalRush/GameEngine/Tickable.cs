using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.GameEngine
{
    /// <summary>
    /// Interface for elements which contains a tick() method, called every 1/30 sec
    /// </summary>
    public interface Tickable
    {
        /// <summary>
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        void tick();
    }
}
