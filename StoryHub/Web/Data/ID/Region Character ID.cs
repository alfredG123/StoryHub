using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class RegionCharacterID : BaseID
    {
        /// <summary>
        /// Create an ID for the region character link item
        /// </summary>
        public RegionCharacterID()
               : base(DataIDType.RegionCharacterLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the region character link item
        /// </summary>
        /// <param name="value"></param>
        public RegionCharacterID(int value)
            : base(value, DataIDType.RegionCharacterLinkItem)
        {
        }
    }
}
