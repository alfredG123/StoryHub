using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class TimelinePlotID : BaseID
    {
        /// <summary>
        /// Create an ID for the timeline plot link item
        /// </summary>
        public TimelinePlotID()
               : base(DataIDType.TimelinePlotLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the timeline plot link item
        /// </summary>
        /// <param name="value"></param>
        public TimelinePlotID(int value)
            : base(value, DataIDType.TimelinePlotLinkItem)
        {
        }
    }
}
