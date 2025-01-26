using nahkd123.PenIDProfile.Parser;
using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.DependencyInjection;
using OpenTabletDriver.Plugin.Output;
using OpenTabletDriver.Plugin.Tablet;

namespace nahkd123.PenIDProfile
{
    public abstract class BasePenIDProfileFilter : IPositionedPipelineElement<IDeviceReport>
    {
        [Property("Print pen ID in console"), DefaultPropertyValue(false)]
        public bool PrintPenIDInConsole { get; set; } = false;

        public PipelinePosition Position => PipelinePosition.Raw;
        public abstract event Action<IDeviceReport>? Emit;
        [TabletReference]
        public TabletReference? Tablet { get; set; } = null;
        [Resolved]
        public IDriver? Driver { get; set; } = null;
        public IPenIDParser? Parser { get; set; } = null;
        private bool findParser = false;
        public string LastPenID { get; set; } = string.Empty;

        public virtual void Consume(IDeviceReport value)
        {
            if (Driver != null && Tablet != null)
            {
                if (Parser == null && !findParser)
                {
                    IPenIDParser[] parsers =
                    [
                        new IntuosV2PenIDParser(),
                        new XPPenGen2PenIDParser()
                    ];
                    Parser = (from parser in parsers where parser.Detect(Tablet, Driver) select parser).FirstOrDefault();
                    findParser = true;
                    if (Parser == null) Log.Write("Pen ID profile", $"{Tablet.Properties.Name} does not have parser to parse pen ID");
                }

                if (Parser != null && Parser.TryParseID(value, out string newPenId) && newPenId != LastPenID)
                {
                    OnPenIDChanged(newPenId);
                    if (PrintPenIDInConsole) Log.Write("Pen ID profile", $"Pen ID changed from \"{LastPenID}\" to \"{newPenId}\"");
                    LastPenID = newPenId;
                }
            }

            Consume(LastPenID, value);
        }

        public abstract void Consume(string penId, IDeviceReport report);

        /// <summary>
        /// Called when discovered a pen ID that is different than previous one.
        /// </summary>
        /// <param name="penId">The ID of the pen</param>
        public virtual void OnPenIDChanged(string penId) { }
    }
}
