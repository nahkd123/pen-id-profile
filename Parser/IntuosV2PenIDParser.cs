using OpenTabletDriver.Configurations.Parsers.Wacom.IntuosV2;
using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Tablet;

namespace nahkd123.PenIDProfile.Parser
{
    public class IntuosV2PenIDParser : IPenIDParser
    {
        public bool Detect(TabletReference tablet, IDriver driver)
        {
            return (from identifier in tablet.Identifiers
                    select driver.GetReportParser(identifier) into parser
                    where parser != null && parser is IntuosV2ReportParser
                    select parser).Any();
        }

        public bool TryParseID(IDeviceReport report, out string penId)
        {
            if (report is IntuosV2Report)
            {
                penId = BitConverter.ToString(report.Raw[17..25]);
                return true;
            }
            else
            {
                penId = string.Empty;
                return false;
            }
        }
    }
}
