using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AspNetCoreZhaoMvc.ViewModels
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            users = new List<IdentityUser>();
        }
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public List<IdentityUser> users { get; set; }
    }
}
