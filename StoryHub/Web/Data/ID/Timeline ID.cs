using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class TimelineID : BaseID
    {
        /// <summary>
        /// Create an ID for the timeline data
        /// </summary>
        public TimelineID()
               : base(DataIDType.TimelineData)
        {
        }

        /// <summary>
        /// Create an ID for the timeline data
        /// </summary>
        /// <param name="value"></param>
        public TimelineID(int value)
            : base(value, DataIDType.TimelineData)
        {
        }
    }
}
