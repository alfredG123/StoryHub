using Web.Data.ID;

namespace Web.Data.General
{
    [Serializable()]
    public class SelectionList
        : List<SelectionData>
    {
        /// <summary>
        /// Create an empty list
        /// </summary>
        public SelectionList()
            : base()
        {
        }

        /// <summary>
        /// Add an entry to the list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        public void Add(BaseID id, string title)
        {
            this.Add(new SelectionData(id, title));
        }
    }

    [Serializable()]
    public class SelectionData
    {
        private readonly BaseID _id;
        private readonly string _title;

        /// <summary>
        /// Create a selection data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        public SelectionData(BaseID id, string title)
            : base()
        {
            _id = id;
            _title = title;
        }

        /// <summary>
        /// Return the ID of the selection data
        /// </summary>
        public BaseID ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Return the title of the selection data
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
        }
    }
}
