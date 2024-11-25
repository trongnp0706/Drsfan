using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.Models.ViewModels
{
    public class RoleManagementVM
    {
        public ApplicationUser ApplicationUser{ get; set; }
        [Required(ErrorMessage = "Please select a role.")]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
