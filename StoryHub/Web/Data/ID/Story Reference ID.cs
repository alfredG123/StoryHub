using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class StoryReferenceID : BaseID
    {
        /// <summary>
        /// Create an ID for the story reference link item
        /// </summary>
        public StoryReferenceID()
               : base(DataIDType.StoryReferenceLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the story reference link item
        /// </summary>
        /// <param name="value"></param>
        public StoryReferenceID(int value)
            : base(value, DataIDType.StoryReferenceLinkItem)
        {
        }
    }
}
