using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRush.Entity
{
    abstract class Monster : GameEngine.Tickable
    {
        /// <summary>
        /// Inherited from Tickable
        /// Called by GameEngine at each game tick (1/30 sec.)
        /// </summary>
        public void tick()
        {

        }
    }
}
