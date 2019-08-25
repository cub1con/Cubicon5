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
