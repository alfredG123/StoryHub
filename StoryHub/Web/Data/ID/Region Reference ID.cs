using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class RegionReferenceID : BaseID
    {
        /// <summary>
        /// Create an ID for the region reference link item
        /// </summary>
        public RegionReferenceID()
               : base(DataIDType.RegionReferenceLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the region reference link item
        /// </summary>
        /// <param name="value"></param>
        public RegionReferenceID(int value)
            : base(value, DataIDType.RegionReferenceLinkItem)
        {
        }
    }
}
