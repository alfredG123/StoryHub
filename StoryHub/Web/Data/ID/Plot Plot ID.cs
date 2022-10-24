using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class PlotPlotID : BaseID
    {
        /// <summary>
        /// Create an ID for the plot plot link item
        /// </summary>
        public PlotPlotID()
               : base(DataIDType.PlotPlotLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the plot plot link item
        /// </summary>
        /// <param name="value"></param>
        public PlotPlotID(int value)
            : base(value, DataIDType.PlotPlotLinkItem)
        {
        }
    }
}
