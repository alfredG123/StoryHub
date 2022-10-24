using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class StoryCharacterID : BaseID
    {
        /// <summary>
        /// Create an ID for the story character link item
        /// </summary>
        public StoryCharacterID()
               : base(DataIDType.StoryCharacterLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the story character link item
        /// </summary>
        /// <param name="value"></param>
        public StoryCharacterID(int value)
            : base(value, DataIDType.StoryCharacterLinkItem)
        {
        }
    }
}
