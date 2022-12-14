using Web.Data.ID;
using Web.Enumerations;
using Web.Models;

namespace Web.Data.Plots
{
    [Serializable()]
    public class PlotData : BaseDatabaseData
    {
        #region "Constructors"
        /// <summary>
        /// Create a new plot data
        /// </summary>
        public PlotData()
            : base(new PlotID(), new PlotDataModel())
        {
        }

        /// <summary>
        /// Retrieve the plot data from the database by the plot ID
        /// </summary>
        /// <param name="plot_id"></param>
        /// <param name="db_context"></param>
        public PlotData(PlotID plot_id, ProgramDbContext db_context)
            : base(plot_id, db_context)
        {
        }

        /// <summary>
        /// Create a plot data using the model
        /// </summary>
        /// <param name="plot_data_model"></param>
        public PlotData(PlotDataModel plot_data_model)
            : base(new PlotID(plot_data_model.ID), plot_data_model)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return the plot ID
        /// </summary>
        public PlotID PlotID
        {
            get
            {
                return (PlotID)this.ID;
            }
        }

        /// <summary>
        /// Return the plot type
        /// </summary>
        public PlotType PlotType { get; set; } = PlotType.None;

        /// <summary>
        /// Return the drama type
        /// </summary>
        public DramaType DramaType { get; set; } = DramaType.None;

        /// <summary>
        /// Return the title of the plot
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Return the goal of the plot
        /// </summary>
        public string Goal { get; set; } = string.Empty;

        /// <summary>
        /// Return the background setting of the plot
        /// </summary>
        public string Scene { get; set; } = string.Empty;

        /// <summary>
        /// Return the content of the plot
        /// </summary>
        public string Content { get; set; } = string.Empty;
        #endregion

        /// <summary>
        /// Compare the types and proprties except ID
        /// Return true, if the properties are equal
        /// Otherwise, return false
        /// </summary>
        /// <param name="other_data"></param>
        /// <returns></returns>
        public override bool CompareContent(BaseDatabaseData other_data)
        {
            bool is_equal = true;

            // If the passed object is not set, return the flag to indicate it is not the same as the current object
            if (other_data is null)
            {
                return (false);
            }

            // If the passed object is not BaseID, return the flag to indicate is is not the same as the current object
            if (other_data is not PlotData plot_data)
            {
                return (false);
            }

            // Compare each property
            if (is_equal)
            {
                if (this.PlotType != plot_data.PlotType)
                {
                    is_equal = false;
                }
            }
            if (is_equal)
            {
                if (this.DramaType != plot_data.DramaType)
                {
                    is_equal = false;
                }
            }
            if (is_equal)
            {
                if (this.Title != plot_data.Title)
                {
                    is_equal = false;
                }
            }
            if (is_equal)
            {
                if (this.Goal != plot_data.Goal)
                {
                    is_equal = false;
                }
            }
            if (is_equal)
            {
                if (this.Scene != plot_data.Scene)
                {
                    is_equal = false;
                }
            }
            if (is_equal)
            {
                if (this.Content != plot_data.Content)
                {
                    is_equal = false;
                }
            }

            return (is_equal);
        }

        /// <summary>
        /// Validate the data is valid to save
        /// </summary>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public override bool ValidateData(ProgramDbContext db_context)
        {
            bool is_valid = true;

            this.ErrorMessage = String.Empty;

            // Validate the plot type
            if (is_valid)
            {
                if (this.PlotType == PlotType.None)
                {
                    is_valid = false;

                    this.ErrorMessage = "The plot type is not set.";
                }
            }

            // Validate the drama type
            if (is_valid)
            {
                if (this.DramaType == DramaType.None)
                {
                    is_valid = false;

                    this.ErrorMessage = "The drama type is not set.";
                }
            }

            // Validate the title
            if (is_valid)
            {
                if (string.IsNullOrEmpty(this.Title))
                {
                    is_valid = false;

                    this.ErrorMessage = "The title of the plot is not set.";
                }
            }

            // Validate the goal
            if (is_valid)
            {
                if (string.IsNullOrEmpty(this.Goal))
                {
                    is_valid = false;

                    this.ErrorMessage = "The goal of the plot is not set.";
                }
            }

            // Validate the scene
            if (is_valid)
            {
                if (string.IsNullOrEmpty(this.Scene))
                {
                    is_valid = false;

                    this.ErrorMessage = "The scene of the plot is not set.";
                }
            }

            // Validate the content
            if (is_valid)
            {
                if (string.IsNullOrEmpty(this.Content))
                {
                    is_valid = false;

                    this.ErrorMessage = "The content of the plot is not set.";
                }
            }

            return is_valid;
        }

        /// <summary>
        /// Create a model object
        /// </summary>
        /// <returns></returns>
        protected override BaseModel CreateEmptyModelObject()
        {
            return (new PlotDataModel());
        }

        /// <summary>
        /// Retrieve the model by the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        protected override BaseModel? RetrieveModelObject(BaseID id, ProgramDbContext db_context)
        {
            // Find the plot from the database by the specifed ID
            return (db_context.PlotData.Where(plot_item => plot_item.ID == id.Value).FirstOrDefault());
        }

        /// <summary>
        /// Set up the global variables with the data in the model
        /// </summary>
        protected override void UpdateDataObject()
        {
            PlotDataModel plot_data_model = (PlotDataModel)this.BaseModel;
            PlotID plot_id = new(plot_data_model.ID);

            this.ID = plot_id;
            this.PlotType = PlotType.GetPlotType(plot_data_model.PlotType);
            this.DramaType = DramaType.GetDramaType(plot_data_model.DramaType);
            this.Title = plot_data_model.Title;
            this.Goal = plot_data_model.Goal;
            this.Scene = plot_data_model.Scene;
            this.Content = plot_data_model.Content;
            this.IsSet = plot_id.IsSet;
        }

        /// <summary>
        /// Update the plot model with the data from the global variables
        /// </summary>
        protected override void UpdateModelObject()
        {
            PlotDataModel plot_data_model = (PlotDataModel)this.BaseModel;

            plot_data_model.ID = this.PlotID.Value;
            plot_data_model.PlotType = this.PlotType.Value;
            plot_data_model.DramaType = this.DramaType.Value;
            plot_data_model.Title = this.Title;
            plot_data_model.Goal = this.Goal;
            plot_data_model.Scene = this.Scene;
            plot_data_model.Content = this.Content;
        }
    }
}
