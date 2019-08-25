using System;

namespace Cubicon5.Helper
{
    public static class SpeedHelper
    {

        public static string GetSpeedInKmh(float vehicleSpeed = 0)
        {
            return Math.Floor((vehicleSpeed * 3600f) / 1000f).ToString("0 " + "KM\\H");
        }

    }
}
