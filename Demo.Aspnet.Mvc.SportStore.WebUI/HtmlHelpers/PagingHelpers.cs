using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;

using Demo.Aspnet.Mvc.SportStore.WebUI.Models;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.HtmlHelpers
{
    /// <summary>
    /// Html helper for rendering a paging control
    /// </summary>
    public static class PagingHelpers
    {
        /// <summary>
        /// Helper method for building paging links.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pagingInfo">The paging info.</param>
        /// <param name="pageUrl">The page URL.</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks( this HtmlHelper htmlHelper, PagingInfo pagingInfo, Func<int, string> pageUrl )
        {
            StringBuilder stringBuilder = new StringBuilder();

            for( int i = 1; i <= pagingInfo.TotalPages; i++ )
            {
                // Build a page index's link
                TagBuilder anchorTagBuilder = new TagBuilder( "a" );
                anchorTagBuilder.MergeAttribute( "href", pageUrl( i ) );
                anchorTagBuilder.InnerHtml = string.Format( "{0}", i.ToString(CultureInfo.InvariantCulture) );
                // Define current page index's style
                if( i == pagingInfo.CurrentPageIndex )
                {
                    anchorTagBuilder.AddCssClass( "selected" );
                }
                stringBuilder.Append( anchorTagBuilder );
            }
            return MvcHtmlString.Create( stringBuilder.ToString() );
        }
    }
}