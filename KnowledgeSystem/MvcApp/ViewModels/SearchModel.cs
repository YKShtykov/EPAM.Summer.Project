using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp.ViewModels
{
    public class SearchModel
    {
        public SearchModel()
        {
            Profiles = new List<MvcProfile>();
        }

        public int Age { get; set; }
        public string StringKey { get; set; }        
        public string Gender { get; set; }
        public string City { get; set; }

        public List<MvcProfile> Profiles { get; set; }
    }
}