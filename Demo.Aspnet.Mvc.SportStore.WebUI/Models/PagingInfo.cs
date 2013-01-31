using System;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.Models
{
    /// <summary>
    /// Describe the Specification of Page links control that should be rendered on screen
    /// </summary>
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// Gets the total pages, a number of page links fit in the Page Links control
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int TotalPages
        {
            get { return (int)Math.Ceiling( (decimal)TotalItems / ItemsPerPage ); }
        }
    }
}