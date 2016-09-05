using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcApp.ViewModels
{
    public class MvcSkill
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string Name { get; set; }

        public int Level { get; set; }

        [Required(ErrorMessage ="Choose skill category")]
        public string CategoryName { get; set; }
    }

    public class MvcCategory
    {
        public MvcCategory()
        {
            Skills = new List<MvcSkill>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string Name { get; set; }

        public List<MvcSkill> Skills { get; set; }
    }
}