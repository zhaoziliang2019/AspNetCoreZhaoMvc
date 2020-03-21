using AspNetCoreZhaoMvc.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreZhaoMvc.ViewModels
{
    public class RoleEditViewModel
    {
        [Display(Name ="编号")]
        public string Id { get; set; }
        [Display(Name ="角色名")]
        [Remote("CheckRoleExist","Role",ErrorMessage ="角色已存在")]
        public string RoleName { get; set; }
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<string> Users { get; set; }
        [Display(Name ="封面地址")]
        [ValidUrl(ErrorMessage ="这个Url不正确")]
        public string ConvertUrl { get; set; }
    }
}
