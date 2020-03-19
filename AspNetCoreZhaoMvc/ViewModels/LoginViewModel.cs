using System.ComponentModel.DataAnnotations;

namespace AspNetCoreZhaoMvc.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UseName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="密码")]
        public string PassWord { get; set; }
    }
}
