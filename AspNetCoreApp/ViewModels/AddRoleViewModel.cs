using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.ViewModels
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}
