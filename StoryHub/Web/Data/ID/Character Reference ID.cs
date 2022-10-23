using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class CharacterReferenceID : BaseID
    {
        /// <summary>
        /// Create an ID for the character reference link item
        /// </summary>
        public CharacterReferenceID()
               : base(DataIDType.CharacterReferenceLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the character reference link item
        /// </summary>
        /// <param name="value"></param>
        public CharacterReferenceID(int value)
            : base(value, DataIDType.CharacterReferenceLinkItem)
        {
        }
    }
}
