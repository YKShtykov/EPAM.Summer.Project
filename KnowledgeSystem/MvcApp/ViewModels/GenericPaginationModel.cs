using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApp.Infrastructure;

namespace MvcApp.ViewModels
{
    public class GenericPaginationModel<T>
    {
        public GenericPaginationModel()
        {
        }
        public GenericPaginationModel(int page, int pageSize, IEnumerable<T> entities)
        {
            Entities = entities.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pagination = new Pagination { PageNumber = page, PageSize = pageSize, TotalItems = entities.ToList().Count };
        }

        public Pagination Pagination { get; set; }
        public IList<T> Entities { get; set; }
    }
}