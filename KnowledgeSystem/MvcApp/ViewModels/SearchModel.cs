using System.ComponentModel.DataAnnotations;

namespace MvcApp.ViewModels
{
    public class SearchModel
    {
        public string StringKey { get; set; }        
        public string City { get; set; }

        public GenericPaginationModel<MvcProfile> Profiles { get; set; }
    }
}