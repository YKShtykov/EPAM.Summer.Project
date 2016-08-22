using System;
using System.Collections.Generic;
using System.Linq;
using MvcApp.Infrastructure;

namespace MvcApp.ViewModels
{
    public class SkillModelPaging
    {
        public Pagination Pagination { get; set; }
        public IList<SkillsModel> UsersSkills { get; set; }
    }
}