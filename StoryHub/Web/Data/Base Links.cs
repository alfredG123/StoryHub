using Web.Data.ID;

namespace Web.Data
{
    [Serializable()]
    public abstract class BaseLinks<TLeftID, TRightID>
        : List<BaseLinkItem<TLeftID, TRightID>>
        where TLeftID : BaseID
        where TRightID : BaseID
    {
        /// <summary>
        /// Retrieve all link items from database
        /// </summary>
        /// <param name="db_context"></param>
        protected abstract void RetrieveLinks(ProgramDbContext db_context);

        /// <summary>
        /// Retrieve all link items from database
        /// </summary>
        /// <param name="db_context"></param>
        public BaseLinks(ProgramDbContext db_context)
        {
            RetrieveLinks(db_context);
        }
    }

    [Serializable()]
    public abstract class BaseLinkItem<TLeftID, TRightID>
        where TLeftID : BaseID
        where TRightID : BaseID
    {
        /// <summary>
        /// Retrieve all link items from database
        /// </summary>
        /// <param name="left_id"></param>
        /// <param name="right_id"></param>
        /// <param name="db_context"></param>
        public BaseLinkItem(TLeftID left_id, TRightID right_id, ProgramDbContext db_context)
        {
            this.LeftID = left_id;
            this.RightID = right_id;
        }

        /// <summary>
        /// Return the left ID
        /// </summary>
        public TLeftID LeftID { get; }

        /// <summary>
        /// Return the right ID
        /// </summary>
        public TRightID RightID { get; }
    }
}
