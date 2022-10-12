namespace Web.Enumerations
{
    public class DramaType
        : BaseEnumeration
    {
        public readonly static DramaType None = new(1, "Undefine");

        public readonly static DramaType Introduction = new(1, "Introduction");
        public readonly static DramaType Incident = new(2, "Incident");
        public readonly static DramaType Conflict = new(3, "Conflict");
        public readonly static DramaType Confront = new(4, "Confront");
        public readonly static DramaType Fall = new(5, "Fall");
        public readonly static DramaType Catastrophe = new(6, "Catastrophe");
        public readonly static DramaType Rise = new(7, "Rise");
        public readonly static DramaType Climax = new(8, "Climax");
        public readonly static DramaType Resolution = new(9, "Resolution");

        /// <summary>
        /// Create a new drama type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        public DramaType(int value, string text)
            : base(value, text)
        {
        }

        /// <summary>
        /// Return a list of available drama types
        /// </summary>
        /// <returns></returns>
        public static List<DramaType> GetAllDramaTypes()
        {
            return (BaseEnumeration.GetAll<DramaType>());
        }

        /// <summary>
        /// Return the drama type by the specified value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DramaType GetDramaType(int value)
        {
            DramaType? drama_type = GetItem<DramaType>(value);
            if (drama_type == null)
            {
                drama_type = DramaType.None;
            }

            return (drama_type);
        }
    }
}
