using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Stories;
using Web.Miscellaneous;
using X.PagedList;

namespace Web.Controllers
{
    public class StoryController : Controller
    {
        private readonly ProgramDbContext _db_context;
        
        // Paging variables
        private static int _page_number_for_story_data_list_page = 1;

        public StoryController(ProgramDbContext db_context)
        {
            _db_context = db_context;
        }

        #region "Story List"
        public IActionResult Index(int? page_number)
        {
            StoryDataList? story_data_list = null;

            // If the story data list is not retrieve yet, load all the story data from the database
            if (story_data_list == null)
            {
                // Load all the story data from the database
                story_data_list = new(_db_context);
            }

            // If the page number is not specified, use the last stored page number
            if (page_number == null)
            {
                page_number = _page_number_for_story_data_list_page;
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_story_data_list_page = GlobalMethods.GetValidPageNumber(page_number, story_data_list.Count);

            return View("StoryList", story_data_list.ToPagedList(_page_number_for_story_data_list_page, GlobalMethods.PAGE_SIZE));
        }
        #endregion
    }
}
