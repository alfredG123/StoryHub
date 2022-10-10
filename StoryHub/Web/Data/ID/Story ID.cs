using Web.Enumerations;

namespace Web.Data.ID
{
    public class StoryID : BaseID
    {
        /// <summary>
        /// Create an ID for the story data
        /// </summary>
        public StoryID()
               : base(DataIDType.StoryData)
        {
        }

        /// <summary>
        /// Create an ID for the story data
        /// </summary>
        /// <param name="value"></param>
        public StoryID(int value)
            : base(value, DataIDType.StoryData)
        {
        }
    }
}
