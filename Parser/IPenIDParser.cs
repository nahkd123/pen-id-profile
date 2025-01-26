using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Tablet;

namespace nahkd123.PenIDProfile.Parser
{
    public interface IPenIDParser
    {
        public bool Detect(TabletReference tablet, IDriver driver);

        public bool TryParseID(IDeviceReport report, out string penId);
    }
}
