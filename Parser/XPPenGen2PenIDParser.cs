using OpenTabletDriver.Configurations.Parsers.XP_Pen;
using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Tablet;

namespace nahkd123.PenIDProfile.Parser
{
    public class XPPenGen2PenIDParser : IPenIDParser
    {
        public bool Detect(TabletReference tablet, IDriver driver)
        {
            return (from identifier in tablet.Identifiers
                    select driver.GetReportParser(identifier) into parser
                    where parser != null && parser is XP_PenGen2ReportParser
                    select parser).Any();
        }

        public bool TryParseID(IDeviceReport report, out string penId)
        {
            if (report.Raw[1].IsBitSet(7))
            {
                penId = BitConverter.ToString(report.Raw[1..13]);
                return true;
            } else
            {
                penId = string.Empty;
                return false;
            }
        }
    }
}
