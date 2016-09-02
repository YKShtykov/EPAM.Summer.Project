using System.ComponentModel.DataAnnotations;

namespace MvcApp.ViewModels
{
    public class SearchModel
    {
        [Display(Name = "Max age")]
        [RegularExpression(@"\d*", ErrorMessage = "Enter correct maximum age, please")]
        public string Age { get; set; }

        public string StringKey { get; set; }        
        public string Gender { get; set; }
        public string City { get; set; }

        public GenericPaginationModel<MvcProfile> Profiles { get; set; }
    }
}