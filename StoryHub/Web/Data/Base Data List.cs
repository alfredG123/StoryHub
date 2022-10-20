using Web.Data.ID;

namespace Web.Data
{
    [Serializable()]
    public abstract class BaseDataList<TID, TData>
    : List<TData>
           where TID : BaseID
        where TData : BaseDatabaseData
    {
        /// <summary>
        /// Return the data by the specified ID
        /// </summary>
        /// <param name="data_id"></param>
        /// <returns></returns>
        public abstract TData? GetListItem(TID data_id);

        /// <summary>
        /// Retrieve all data from database
        /// </summary>
        /// <param name="db_context"></param>
        protected abstract void RetrieveData(ProgramDbContext db_context);

        /// <summary>
        /// Create an empty list
        /// </summary>
        public BaseDataList()
        {
        }

        /// <summary>
        /// Retrieve all data from the database
        /// </summary>
        /// <param name="db_context"></param>
        public BaseDataList(ProgramDbContext db_context)
        {
            RetrieveData(db_context);
        }
    }
}
