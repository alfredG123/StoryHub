using Web.Enumerations;

namespace Web.Data.ID
{
    public class ReferenceID : BaseID
    {
        /// <summary>
        /// Create an ID for the reference data
        /// </summary>
        public ReferenceID()
               : base(DataIDType.ReferenceData)
        {
        }

        /// <summary>
        /// Create an ID for the reference data
        /// </summary>
        /// <param name="value"></param>
        public ReferenceID(int value)
            : base(value, DataIDType.ReferenceData)
        {
        }
    }
}
