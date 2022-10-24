using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class StoryCustomFieldID : BaseID
    {
        /// <summary>
        /// Create an ID for the story custom field link item
        /// </summary>
        public StoryCustomFieldID()
               : base(DataIDType.StoryCustomFieldLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the story custom field link item
        /// </summary>
        /// <param name="value"></param>
        public StoryCustomFieldID(int value)
            : base(value, DataIDType.StoryCustomFieldLinkItem)
        {
        }
    }
}
