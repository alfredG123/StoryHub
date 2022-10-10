using Web.Enumerations;

namespace Web.Data.ID
{
    public class PlotID : BaseID
    {
        /// <summary>
        /// Create an ID for the plot data
        /// </summary>
        public PlotID()
               : base(DataIDType.PlotData)
        {
        }

        /// <summary>
        /// Create an ID for the plot data
        /// </summary>
        /// <param name="value"></param>
        public PlotID(int value)
            : base(value, DataIDType.PlotData)
        {
        }
    }
}
