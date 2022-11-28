using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.ID;
using Web.Data.Stories;
using Web.Data.Timeline;
using Web.Miscellaneous;
using Web.Models;
using X.PagedList;

namespace Web.Controllers
{
    public class TimelineController : Controller
    {
        private readonly ProgramDbContext _db_context;
        private readonly MiscellaneousController _miscellaneous_controller;

        // Related story
        private static StoryID _story_id = new();

        // Lists
        private static TimelineDataList? _timeline_data_list = null;

        // Paging variables
        private static int _page_number_for_timeline_data_list_page = 1;

        public TimelineController(ProgramDbContext db_context)
        {
            _db_context = db_context;
            _miscellaneous_controller = new MiscellaneousController(db_context);
        }

        #region "Timeline List"
        public IActionResult Index(int? story_id, int? page_number)
        {
            TimelineDataList? timeline_data_list = _timeline_data_list;

            // Record the story
            if (story_id != null)
            {
                _story_id = new StoryID((int)story_id);

                // Reset the list for the new story
                timeline_data_list = null;
            }

            // If the timeline data list is not retrieve yet, load all the timeline data from the database
            if (timeline_data_list == null)
            {
                // Load all the timeline data from the database
                timeline_data_list = new(_story_id, _db_context);
            }

            // If the page number is not specified, use the last stored page number
            if (page_number == null)
            {
                page_number = _page_number_for_timeline_data_list_page;
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_timeline_data_list_page = GlobalMethods.GetValidPageNumber(page_number, timeline_data_list.Count);

            this.ViewBag.StoryID = _story_id.Value;

            return View(GlobalWebPages.TIMELINE_LIST_PAGE, timeline_data_list.ToPagedList(_page_number_for_timeline_data_list_page, GlobalMethods.PAGE_SIZE));
        }

        public IActionResult Delete(string? timeline_id)
        {
            // Retrieve the ID from the form
            TimelineID id = new(StringMethods.ParseTextAsInt(timeline_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Timeline ID is not valid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Load the timeline
            TimelineData timeline_data = new(id, _db_context);
            if (!timeline_data.IsSet)
            {
                ErrorData error_data = new("Timeline is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Delete the timeline from the database
            timeline_data.Delete(_db_context);

            // Reset lists
            _timeline_data_list = null;

            // Delete the timeline from story links
            StoryTimelineLinks story_timeline_links = new(_story_id, _db_context);
            int item_index = story_timeline_links.IndexOfID(id);
            story_timeline_links.Delete(item_index);
            story_timeline_links.Save(_db_context);

            _miscellaneous_controller.DisplaySuccessMessage("Timeline is deleted successfully.", TempData);

            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion

        #region "Timeline Detail"
        public IActionResult Create()
        {
            // Create a new timeline data to set up the form
            TimelineData timeline_data = new();

            return View(GlobalWebPages.TIMELINE_DETAIL_PAGE, TimelineDetailViewModel.ConvertToTimelineDetailViewModel(timeline_data));
        }

        public IActionResult Edit(string? timeline_id)
        {
            // Retrieve the ID from the form
            TimelineID id = new(StringMethods.ParseTextAsInt(timeline_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Timeline ID is not invalid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Retrieve the timeline data from the database
            TimelineData timeline_data = new(id, _db_context);
            if (!timeline_data.IsSet)
            {
                ErrorData error_data = new("Timeline is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            return View(GlobalWebPages.TIMELINE_DETAIL_PAGE, TimelineDetailViewModel.ConvertToTimelineDetailViewModel(timeline_data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveTimelineDetail(TimelineDetailViewModel timeline_detail_view_model)
        {
            TimelineData timeline_data = TimelineDetailViewModel.ConvertToTimelineData(timeline_detail_view_model, _db_context);

            // Verify the timeline is valid
            // If fail, reload the page
            if (!timeline_data.ValidateData(_db_context))
            {
                _miscellaneous_controller.RaiseError(timeline_data.ErrorMessage, ModelState, TempData);

                // Reload the detail page to enter the data again
                return View(GlobalWebPages.TIMELINE_DETAIL_PAGE, TimelineDetailViewModel.ConvertToTimelineDetailViewModel(timeline_data));
            }

            // Save the timeline
            timeline_data.Save(_db_context);

            // Reset lists
            _timeline_data_list = null;

            // Add the association between story and timeline
            StoryTimelineLinks story_timeline_links = new(_story_id, _db_context);
            story_timeline_links.Add(_story_id, timeline_data.TimelineID);
            story_timeline_links.Save(_db_context);

            // Create a pop-up message to notify the user
            _miscellaneous_controller.DisplaySuccessMessage("The timeline is saved!", TempData);

            // Return to the timeline list page
            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion
    }
}
