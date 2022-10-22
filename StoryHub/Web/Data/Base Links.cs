using Web.Data.ID;
using Web.Models;

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
        /// Load the model
        /// </summary>
        /// <param name="base_model"></param>
        /// <param name="db_context"></param>
        public BaseLinkItem(BaseModel base_model, ProgramDbContext db_context)
        {
        }

        /// <summary>
        /// Return the left ID
        /// </summary>
        protected abstract TLeftID LeftID { get; }

        /// <summary>
        /// Return the right ID
        /// </summary>
        protected abstract TRightID RightID { get; }
    }
}
