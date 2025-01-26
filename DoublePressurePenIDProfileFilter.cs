using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;

namespace nahkd123.PenIDProfile
{
    [PluginName("Pen ID profile - Double presure")]
    public class DoublePressurePenIDProfileFilter : BasePenIDProfileFilter
    {
        [Property("Pen IDs"), DefaultPropertyValue(""), ToolTip("A comma-separated list of pen IDs to match")]
        public string PenIDs { get; set; } = string.Empty;
        public string[] PenIDArray => PenIDs.Split(",");
        public bool DoublePressure { get; private set; } = false;

        public override event Action<IDeviceReport>? Emit;

        public override void Consume(string penId, IDeviceReport value)
        {
            if (value is ITabletReport tabletReport && DoublePressure) tabletReport.Pressure *= 2;
            Emit?.Invoke(value);
        }

        public override void OnPenIDChanged(string penId)
        {
            DoublePressure = PenIDArray.Contains(penId);
        }
    }
}
