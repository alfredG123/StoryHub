using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Regions;
using Web.Miscellaneous;
using X.PagedList;

namespace Web.Controllers
{
    public class RegionController : Controller
    {
        private readonly ProgramDbContext _db_context;
        private readonly MiscellaneousController _miscellaneous_controller;

        // Paging variables
        private static int _page_number_for_region_data_list_page = 1;

        public RegionController(ProgramDbContext db_context)
        {
            _db_context = db_context;
            _miscellaneous_controller = new MiscellaneousController(db_context);
        }

        #region "Region List"
        public IActionResult Index(int? page_number)
        {
            RegionDataList? region_data_list = null;

            // If the region data list is not retrieve yet, load all the region data from the database
            if (region_data_list == null)
            {
                // Load all the region data from the database
                region_data_list = new(_db_context);
            }

            // If the page number is not specified, use the last stored page number
            if (page_number == null)
            {
                page_number = _page_number_for_region_data_list_page;
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_region_data_list_page = GlobalMethods.GetValidPageNumber(page_number, region_data_list.Count);

            return View(GlobalWebPages.REGION_LIST_PAGE, region_data_list.ToPagedList(_page_number_for_region_data_list_page, GlobalMethods.PAGE_SIZE));
        }
        #endregion
    }
}
