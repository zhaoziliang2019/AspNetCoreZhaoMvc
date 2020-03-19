using AspNetCoreZhaoMvc.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreZhaoMvc.Controllers
{
    public class AccountController : Controller
    {
        public readonly SignInManager<IdentityUser> signInManager;
        public readonly UserManager<IdentityUser> userManager;
        public AccountController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await userManager.FindByNameAsync(loginViewModel.UseName);
            if(user!=null)
            {
                var result = await signInManager.PasswordSignInAsync(user,loginViewModel.PassWord,false,false);
                if (result.Succeeded)
                {
                    RedirectToAction("Index","Home");
                }
            }
            ModelState.AddModelError("","用户名或密码错误！");
            return View(loginViewModel);
        }
        /// <summary>
        /// 注册的view
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 注册Model
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
       [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = registerViewModel.UseName
                };
                var result =  await userManager.CreateAsync(user,registerViewModel.PassWord);
                if (result.Succeeded)
                {
                    RedirectToAction("Index","Home");
                }
            }
            return View(registerViewModel);
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Loginout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}