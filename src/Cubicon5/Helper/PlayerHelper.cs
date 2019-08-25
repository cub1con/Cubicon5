using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace Cubicon5.Helper
{
    public static class PlayerHelper
    {
        public static bool PlayerIsNotNull()
        {
            return (Game.Player != null && Game.Player.Character != null);
        }
        
    }
}
