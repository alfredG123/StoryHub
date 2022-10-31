namespace Web.Miscellaneous
{
    public static class GlobalMethods
    {
        public const int PAGE_SIZE = 8;

        /// <summary>
        /// Return the page number
        /// If the page number is not specified, return the first page
        /// </summary>
        /// <param name="page_number"></param>
        /// <param name="total_item_count"></param>
        /// <returns></returns>
        public static int GetValidPageNumber(int? page_number, int total_item_count)
        {
            // Set up the default value for the page number
            int valid_page_number = GetPageNumberForFirstPage();

            // If the page number is specified, update return value
            if ((page_number != null) && (page_number > 0))
            {
                if (total_item_count > 0)
                {
                    // Calculate the items on the last page
                    int reminder = total_item_count % PAGE_SIZE;

                    // Calculate the maximum number of the possible pages
                    int max_number_page = (total_item_count - reminder) / PAGE_SIZE;
                    if (reminder > 0)
                    {
                        max_number_page += 1;
                    }

                    // If the page number is greater than the maximum number of the possible pages, update the page number
                    // Otherwise, keep the page number
                    valid_page_number = Math.Min(max_number_page, (int)page_number);
                }
            }

            return (valid_page_number);
        }

        /// <summary>
        /// Return the page number for the first page
        /// </summary>
        /// <returns></returns>
        public static int GetPageNumberForFirstPage()
        {
            return (1);
        }

        /// <summary>
        /// Return the paging text, X of Y
        /// </summary>
        /// <param name="page_number"></param>
        /// <param name="page_count"></param>
        /// <returns></returns>
        public static string GetPagingText(int? page_number, int? page_count)
        {
            // Set the default values
            page_number ??= 1;
            page_count ??= 1;

            // Build the text for the paging info
            string paging_text = "Page " + page_number.ToString() + " of " + page_count.ToString();

            return (paging_text);
        }

        /// <summary>
        /// Return the row number
        /// </summary>
        /// <param name="current_row_number"></param>
        /// <param name="page_number"></param>
        /// <returns></returns>
        public static int GetRowNumberForPageNumber(int? current_row_number, int? page_number)
        {
            // Set up default values
            current_row_number ??= 1;
            page_number ??= 1;

            // Throw the error, if the row number is invalid
            if (current_row_number > PAGE_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(current_row_number), "Row number is greater than the page size.");
            }

            // Calculate the row number
            int row_number = (((int)page_number) - 1) * PAGE_SIZE + (int)current_row_number;

            return (row_number);
        }
    }
}
