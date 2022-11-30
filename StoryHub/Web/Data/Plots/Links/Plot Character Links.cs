using Web.Data.ID;
using Web.Models;

namespace Web.Data.Plots
{
    [Serializable()]
    public class PlotCharacterLinks
        : BaseLinks<PlotID, CharacterID>
    {
        /// <summary>
        /// Retrieve all plot character link items from database for the specified plot
        /// </summary>
        /// <param name="plot_id"></param>
        /// <param name="db_context"></param>
        public PlotCharacterLinks(PlotID plot_id, ProgramDbContext db_context)
            : base(LinkType.ByLeft, plot_id, db_context)
        {
        }

        /// <summary>
        /// Retrieve all plot character link items from database for the specified character
        /// </summary>
        /// <param name="character_id"></param>
        /// <param name="db_context"></param>
        public PlotCharacterLinks(CharacterID character_id, ProgramDbContext db_context)
            : base(LinkType.ByRight, character_id, db_context)
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

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="plot_id"></param>
        /// <param name="character_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<PlotID, CharacterID> CreateLinkItem(PlotID plot_id, CharacterID character_id)
        {
            PlotCharacterLinkItemModel link_item_model = new();
            link_item_model.PlotID = plot_id.Value;
            link_item_model.CharacterID = character_id.Value;

            return (new PlotCharacterLinkItem(link_item_model));
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
            : base(new PlotCharacterID(plot_character_link_item_model.ID), new PlotID(plot_character_link_item_model.PlotID), new CharacterID(plot_character_link_item_model.CharacterID), plot_character_link_item_model)
        {
        }

        /// <summary>
        /// Return or set the plot ID
        /// </summary>
        public PlotID PlotID
        {
            get
            {
                return (base.LeftID);
            }

            set
            {
                base.LeftID = value;
            }
        }

        /// <summary>
        /// Return or set the character ID
        /// </summary>
        public CharacterID CharacterID
        {
            get
            {
                return (base.RightID);
            }

            set
            {
                base.RightID = value;
            }
        }

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
