using Web.Data.ID;
using Web.Models;

namespace Web.Data.Plots
{
    [Serializable()]
    public class PlotCharacterLinks
        : BaseLinks<PlotID, CharacterID>
    {
        /// <summary>
        /// Retrieve all plot character link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public PlotCharacterLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
            : base(link_type, id, db_context)
        {
        }

        /// <summary>
        /// Retrieve all link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        protected override void RetrieveLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.PlotCharacterLinkItem
            List<PlotCharacterLinkItemModel> plot_character_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                plot_character_link_item_model_list = db_context.PlotCharacterLinkItem.Where(list_item => list_item.PlotID == id.Value).ToList();
            }
            else
            {
                plot_character_link_item_model_list = db_context.PlotCharacterLinkItem.Where(list_item => list_item.CharacterID == id.Value).ToList();
            }

            foreach (PlotCharacterLinkItemModel plot_character_link_item_model in plot_character_link_item_model_list)
            {
                this.Add(new PlotCharacterLinkItem(plot_character_link_item_model));
            }
        }
    }

    [Serializable()]
    public class PlotCharacterLinkItem
         : BaseLinkItem<PlotID, CharacterID>
    {
        /// <summary>
        /// Load the plot character link item model
        /// </summary>
        /// <param name="plot_character_link_item_model"></param>
        public PlotCharacterLinkItem(PlotCharacterLinkItemModel plot_character_link_item_model)
            : base(new PlotCharacterID(plot_character_link_item_model.ID), plot_character_link_item_model)
        {
        }

        /// <summary>
        /// Return or set the plot ID
        /// </summary>
        public PlotID PlotID { get; set; } = new();

        /// <summary>
        /// Return or set the character ID
        /// </summary>
        public CharacterID CharacterID { get; set; } = new();

        /// <summary>
        /// Return or set the description of the link item
        /// </summary>
        public string Description { get; set; } = String.Empty;

        /// <summary>
        /// Update the data object for retrieving the data from the database
        /// </summary>
        protected override void UpdateDataObject()
        {
            PlotCharacterLinkItemModel plot_character_link_item_model = (PlotCharacterLinkItemModel)this.BaseModel;
            PlotCharacterID plot_character_id = new(plot_character_link_item_model.ID);

            this.ID = plot_character_id;
            this.PlotID = new PlotID(plot_character_link_item_model.PlotID);
            this.CharacterID = new CharacterID(plot_character_link_item_model.CharacterID);
            this.Description = plot_character_link_item_model.Description;
            this.IsSet = plot_character_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            PlotCharacterLinkItemModel plot_character_link_item_model = (PlotCharacterLinkItemModel)this.BaseModel;

            plot_character_link_item_model.ID = this.ID.Value;
            plot_character_link_item_model.PlotID = this.PlotID.Value;
            plot_character_link_item_model.CharacterID = this.CharacterID.Value;
            plot_character_link_item_model.Description = this.Description;
        }
    }
}
