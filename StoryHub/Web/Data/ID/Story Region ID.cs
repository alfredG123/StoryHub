using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class StoryRegionID : BaseID
    {
        /// <summary>
        /// Create an ID for the story region link item
        /// </summary>
        public StoryRegionID()
               : base(DataIDType.StoryRegionLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the story region link item
        /// </summary>
        /// <param name="value"></param>
        public StoryRegionID(int value)
            : base(value, DataIDType.StoryRegionLinkItem)
        {
        }
    }
}
