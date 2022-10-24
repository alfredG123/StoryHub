using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class PlotRegionID : BaseID
    {
        /// <summary>
        /// Create an ID for the plot region link item
        /// </summary>
        public PlotRegionID()
               : base(DataIDType.PlotRegionLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the plot region link item
        /// </summary>
        /// <param name="value"></param>
        public PlotRegionID(int value)
            : base(value, DataIDType.PlotRegionLinkItem)
        {
        }
    }
}
