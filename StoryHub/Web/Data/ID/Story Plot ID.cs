using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class StoryPlotID : BaseID
    {
        /// <summary>
        /// Create an ID for the story plot link item
        /// </summary>
        public StoryPlotID()
               : base(DataIDType.StoryPlotLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the story plot link item
        /// </summary>
        /// <param name="value"></param>
        public StoryPlotID(int value)
            : base(value, DataIDType.StoryPlotLinkItem)
        {
        }
    }
}
