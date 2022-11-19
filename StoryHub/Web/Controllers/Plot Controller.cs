using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.ID;
using Web.Data.Plots;
using Web.Miscellaneous;
using Web.Models;
using X.PagedList;

namespace Web.Controllers
{
    public class PlotController : Controller
    {
        private readonly ProgramDbContext _db_context;
        private readonly MiscellaneousController _miscellaneous_controller;
        private int _story_id;

        // Paging variables
        private static int _page_number_for_plot_data_list_page = 1;

        public PlotController(ProgramDbContext db_context)
        {
            _db_context = db_context;
            _miscellaneous_controller = new MiscellaneousController(db_context);
        }

        #region "Plot List"
        public IActionResult Index(int? story_id, int? page_number)
        {
            PlotDataList? plot_data_list = null;

            // If the plot data list is not retrieve yet, load all the plot data from the database
            if (plot_data_list == null)
            {
                // Load all the plot data from the database
                plot_data_list = new(_db_context);
            }

            // If the page number is not specified, use the last stored page number
            if (page_number == null)
            {
                page_number = _page_number_for_plot_data_list_page;
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_plot_data_list_page = GlobalMethods.GetValidPageNumber(page_number, plot_data_list.Count);

            if (story_id != null)
            {
                _story_id = (int)story_id;
            }

            this.ViewBag.StoryID = _story_id;

            return View(GlobalWebPages.PLOT_LIST_PAGE, plot_data_list.ToPagedList(_page_number_for_plot_data_list_page, GlobalMethods.PAGE_SIZE));
        }

        public IActionResult Delete(string? plot_id)
        {
            // Retrieve the ID from the form
            PlotID id = new(StringMethods.ParseTextAsInt(plot_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Plot ID is not valid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Load the plot
            PlotData plot_data = new(id, _db_context);
            if (!plot_data.IsSet)
            {
                ErrorData error_data = new("Plot is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Delete the plot from the database
            plot_data.Delete(_db_context);

            _miscellaneous_controller.DisplaySuccessMessage("Plot is deleted successfully.", TempData);

            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion

        #region "Plot Detail"
        public IActionResult Create()
        {
            // Create a new plot data to set up the form
            PlotData plot_data = new();

            return View(GlobalWebPages.PLOT_DETAIL_PAGE, PlotDetailViewModel.ConvertToPlotDetailViewModel(plot_data));
        }

        public IActionResult Edit(string? plot_id)
        {
            // Retrieve the ID from the form
            PlotID id = new(StringMethods.ParseTextAsInt(plot_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Plot ID is not invalid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Retrieve the plot data from the database
            PlotData plot_data = new(id, _db_context);
            if (!plot_data.IsSet)
            {
                ErrorData error_data = new("Plot is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            return View(GlobalWebPages.PLOT_DETAIL_PAGE, PlotDetailViewModel.ConvertToPlotDetailViewModel(plot_data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePlotDetail(PlotDetailViewModel plot_detail_view_model)
        {
            PlotData plot_data = PlotDetailViewModel.ConvertToPlotData(plot_detail_view_model, _db_context);

            // Verify the plot is valid
            // If fail, reload the page
            if (!plot_data.ValidateData(_db_context))
            {
                _miscellaneous_controller.RaiseError(plot_data.ErrorMessage, ModelState, TempData);

                // Reload the detail page to enter the data again
                return View(GlobalWebPages.PLOT_DETAIL_PAGE, PlotDetailViewModel.ConvertToPlotDetailViewModel(plot_data));
            }

            // Save the plot
            plot_data.Save(_db_context);

            // Create a pop-up message to notify the user
            _miscellaneous_controller.DisplaySuccessMessage("The plot is saved!", TempData);

            // Return to the plot list page
            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion
    }
}
