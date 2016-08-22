using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApp.Infrastructure;

namespace MvcApp.ViewModels
{
    public class MvcUsersPaging
    {
        public Pagination Pagination { get; set; }
        public IList<MvcUser> Users { get; set; }
    }
}