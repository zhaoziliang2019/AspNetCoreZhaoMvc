using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreZhaoMvc.Models;
using AspNetCoreZhaoMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreZhaoMvc.Controllers
{
    [Authorize(Policy= "编辑专辑4")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        /// <summary>
        /// 加载所有角色
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var roles=await roleManager.Roles.ToListAsync();
            return View(roles);
        }
        /// <summary>
        /// 编辑角色视图
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role==null)
            {
                return RedirectToAction("Index");
            }
            var roleEditViewModel = new RoleEditViewModel
            {
                Id=Id,
                RoleName = role.Name,
                Users = new List<string>(),
            };
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    roleEditViewModel.Users.Add(user.UserName);
                }
            }
            return View(roleEditViewModel);
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            var role =await roleManager.FindByIdAsync(model.Id);
            if (role!=null)
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty,"更新角色出错");
                return View(model);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveUserFormRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role!=null)
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty,"删除角色出错！");
            }
            ModelState.AddModelError(string.Empty,"没找到该角色");
            return RedirectToAction("Index",await roleManager.Roles.ToListAsync());
        }

        /// <summary>
        /// 添加用户到角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role==null)
            {
                return RedirectToAction("Index");
            }
            var UserRole = new UserRoleViewModel
            {
                RoleId = role.Id
            };
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (!await userManager.IsInRoleAsync(user,role.Name))
                {
                    UserRole.users.Add(user);
                }
            }
            return View(UserRole);
        }
        /// <summary>
        /// 远程验证接受get post
        /// </summary>
        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> CheckRoleExist([Bind("RoleName")]string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role!=null)
            {
                return Json("角色已经存在了");
            }
            return Json(true);
        }
    }
}