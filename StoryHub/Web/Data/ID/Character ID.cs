using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class CharacterID : BaseID
    {
        /// <summary>
        /// Create an ID for the character data
        /// </summary>
        public CharacterID()
               : base(DataIDType.CharacterData)
        {
        }

        /// <summary>
        /// Create an ID for the character data
        /// </summary>
        /// <param name="value"></param>
        public CharacterID(int value)
            : base(value, DataIDType.CharacterData)
        {
        }
    }
}
