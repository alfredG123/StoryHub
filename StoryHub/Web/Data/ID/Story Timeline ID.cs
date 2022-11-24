using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class StoryTimelineID : BaseID
    {
        /// <summary>
        /// Create an ID for the story timeline link item
        /// </summary>
        public StoryTimelineID()
               : base(DataIDType.StoryTimelineLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the story timeline link item
        /// </summary>
        /// <param name="value"></param>
        public StoryTimelineID(int value)
            : base(value, DataIDType.StoryTimelineLinkItem)
        {
        }
    }
}
