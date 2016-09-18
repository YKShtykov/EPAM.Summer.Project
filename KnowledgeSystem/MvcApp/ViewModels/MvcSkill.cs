using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcApp.ViewModels
{
    /// <summary>
    /// Class-model for mvc layout skill
    /// </summary>
    public class MvcSkill
    {
        /// <summary>
        /// Skill identify number
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Skill caption
        /// </summary>
        [Display(Name = "Skill Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string Name { get; set; }

        /// <summary>
        /// Skill level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Category caption wich consists the skill
        /// </summary>
        [Display(Name = "Category Name")]
        [Required(ErrorMessage ="Choose skill category")]
        public string CategoryName { get; set; }
    }

    /// <summary>
    /// Class-model for mvc layout category
    /// </summary>
    public class MvcCategory
    {
        /// <summary>
        /// Create Mvc category
        /// </summary>
        public MvcCategory()
        {
            Skills = new List<MvcSkill>();
        }

        /// <summary>
        /// Category identify number
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Category caption
        /// </summary>
        [Display(Name = "Category Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string Name { get; set; }

        /// <summary>
        /// Category skills
        /// </summary>
        public List<MvcSkill> Skills { get; set; }
    }
}