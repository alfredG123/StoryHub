using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.ID;
using Web.Data.Regions;
using Web.Data.Stories;
using Web.Miscellaneous;
using Web.Models;
using X.PagedList;

namespace Web.Controllers
{
    public class RegionController : Controller
    {
        private readonly ProgramDbContext _db_context;
        private readonly MiscellaneousController _miscellaneous_controller;

        // Related story
        private static StoryID _story_id = new();

        // Paging variables
        private static int _page_number_for_region_data_list_page = 1;

        public RegionController(ProgramDbContext db_context)
        {
            _db_context = db_context;
            _miscellaneous_controller = new MiscellaneousController(db_context);
        }

        #region "Region List"
        public IActionResult Index(int? story_id, int? page_number)
        {
            RegionDataList? region_data_list = null;

            // Record the story
            if (story_id != null)
            {
                _story_id = new StoryID((int)story_id);
            }

            // If the region data list is not retrieve yet, load all the region data from the database
            if (region_data_list == null)
            {
                // Load all the region data from the database
                region_data_list = new(_story_id, _db_context);
            }

            // If the page number is not specified, use the last stored page number
            if (page_number == null)
            {
                page_number = _page_number_for_region_data_list_page;
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_region_data_list_page = GlobalMethods.GetValidPageNumber(page_number, region_data_list.Count);

            this.ViewBag.StoryID = _story_id.Value;

            return View(GlobalWebPages.REGION_LIST_PAGE, region_data_list.ToPagedList(_page_number_for_region_data_list_page, GlobalMethods.PAGE_SIZE));
        }

        public IActionResult Delete(string? region_id)
        {
            // Retrieve the ID from the form
            RegionID id = new(StringMethods.ParseTextAsInt(region_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Region ID is not valid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Load the region
            RegionData region_data = new(id, _db_context);
            if (!region_data.IsSet)
            {
                ErrorData error_data = new("Region is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Delete the region from the database
            region_data.Delete(_db_context);

            // Delete the region from story links
            StoryRegionLinks story_region_links = new(_story_id, _db_context);
            int item_index = story_region_links.IndexOfID(id);
            story_region_links.Delete(item_index);
            story_region_links.Save(_db_context);

            _miscellaneous_controller.DisplaySuccessMessage("Region is deleted successfully.", TempData);

            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion

        #region "Region Detail"
        public IActionResult Create()
        {
            // Create a new region data to set up the form
            RegionData region_data = new();

            return View(GlobalWebPages.REGION_DETAIL_PAGE, RegionDetailViewModel.ConvertToRegionDetailViewModel(region_data));
        }

        public IActionResult Edit(string? region_id)
        {
            // Retrieve the ID from the form
            RegionID id = new(StringMethods.ParseTextAsInt(region_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Region ID is not invalid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Retrieve the region data from the database
            RegionData region_data = new(id, _db_context);
            if (!region_data.IsSet)
            {
                ErrorData error_data = new("Region is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            return View(GlobalWebPages.REGION_DETAIL_PAGE, RegionDetailViewModel.ConvertToRegionDetailViewModel(region_data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveRegionDetail(RegionDetailViewModel region_detail_view_model)
        {
            RegionData region_data = RegionDetailViewModel.ConvertToRegionData(region_detail_view_model, _db_context);

            // Verify the region is valid
            // If fail, reload the page
            if (!region_data.ValidateData(_db_context))
            {
                _miscellaneous_controller.RaiseError(region_data.ErrorMessage, ModelState, TempData);

                // Reload the detail page to enter the data again
                return View(GlobalWebPages.REGION_DETAIL_PAGE, RegionDetailViewModel.ConvertToRegionDetailViewModel(region_data));
            }

            // Save the region
            region_data.Save(_db_context);

            // Add the association between story and region
            StoryRegionLinks story_region_links = new(_story_id, _db_context);
            story_region_links.Add(_story_id, region_data.RegionID);
            story_region_links.Save(_db_context);

            // Create a pop-up message to notify the user
            _miscellaneous_controller.DisplaySuccessMessage("The region is saved!", TempData);

            // Return to the region list page
            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion
    }
}
