using Web.Data.ID;
using Web.Models;

namespace Web.Data.References
{
    [Serializable()]
    public class ReferenceDataList
    : BaseDataList<ReferenceID, ReferenceData>
    {
        /// <summary>
        /// Retrieve all reference data from the database
        /// </summary>
        /// <param name="db_context"></param>
        public ReferenceDataList(ProgramDbContext db_context)
            : base(db_context)
        {
        }

        /// <summary>
        /// Return the reference data by the specified ID
        /// </summary>
        /// <param name="reference_id"></param>
        /// <returns></returns>
        public override ReferenceData? GetListItem(ReferenceID reference_id)
        {
            return (this.Where(list_item => list_item.ReferenceID == reference_id).SingleOrDefault());
        }

        /// <summary>
        /// Retrieve all reference data from database
        /// </summary>
        /// <param name="db_context"></param>
        protected override void RetrieveData(ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.ReferenceData
            List<ReferenceModel> reference_data_model_list = db_context.ReferenceData.ToList();

            foreach (ReferenceModel reference_data_model in reference_data_model_list)
            {
                this.Add(new ReferenceData(reference_data_model));
            }
        }
    }
}
