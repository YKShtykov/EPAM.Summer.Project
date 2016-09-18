using System;
using System.Text;
using System.Web.Mvc;

namespace MvcApp.Infrastructure
{
    /// <summary>
    /// Class for storing pagination information
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Current page number
        /// </summary>
        public int PageNumber { get; set; } 
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } 
        /// <summary>
        /// Items count
        /// </summary>
        public int TotalItems { get; set; } 
        /// <summary>
        /// Pages count
        /// </summary>
        public int TotalPages 
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    /// <summary>
    /// Helper for creating pagination buttons
    /// </summary>
    public static class PagingSubmit
    {
        /// <summary>
        /// Creates pagination buttons
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public static MvcHtmlString PageSubmit(this HtmlHelper html,
            Pagination pageInfo)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("input");
                tag.MergeAttribute("type", "submit");
                tag.MergeAttribute("name", "page");
                tag.MergeAttribute("value", i.ToString());
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}