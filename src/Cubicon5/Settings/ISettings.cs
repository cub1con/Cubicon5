using Config.Net;

namespace Cubicon5.Settings
{
    public interface ISettings
    {
        [Option(DefaultValue = true)]
        bool TurnLightsEnabled { get; set; }

        [Option(DefaultValue = true)]
        bool HeadlightFlasherEnabled { get; set; }

        [Option(DefaultValue = true)]
        bool SpeedometerEnabled { get; set; }

        [Option(DefaultValue = true)]
        bool TempomatEnabled { get; set; }

        [Option(DefaultValue = false)]
        bool TempomatIgnoreVehicleInAir { get; set; }
    }
}
