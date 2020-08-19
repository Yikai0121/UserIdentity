using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserIdentityPr.ViewModels
{
    public class RoleAddViewModel
    {
        [Required]
        [Display(Name ="角色名稱")]
        public string RoleName { get; set; }
    }
}
