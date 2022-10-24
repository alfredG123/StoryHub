using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class PlotCharacterID : BaseID
    {
        /// <summary>
        /// Create an ID for the plot character link item
        /// </summary>
        public PlotCharacterID()
               : base(DataIDType.PlotCharacterLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the plot character link item
        /// </summary>
        /// <param name="value"></param>
        public PlotCharacterID(int value)
            : base(value, DataIDType.PlotCharacterLinkItem)
        {
        }
    }
}
