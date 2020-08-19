using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserIdentityPr.Models;
using UserIdentityPr.ViewModels;

namespace UserIdentityPr.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "刪除錯誤");
            }
            ModelState.AddModelError(string.Empty, "沒有此角色");
            return View("Index", await _roleManager.Roles.ToListAsync());
        }

        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleAddViewModel roleAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(roleAddViewModel);
            }
            var role = _mapper.Map<IdentityRole>(roleAddViewModel);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(roleAddViewModel);
            }
        }
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var roleEditViewModel = new RoleEditViewModel
            {
                Id = id,
                RoleName = role.Name,
                Users = new List<string>()
            };
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roleEditViewModel.Users.Add(user.UserName);
                }
            }
            return View(roleEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel roleEditViewModel)
        {
            var role = await _roleManager.FindByIdAsync(roleEditViewModel.Id);
            if (role != null)
            {
                role.Name = roleEditViewModel.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "編輯失敗");
                return View(roleEditViewModel);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var vm = new UserRoleViewModel
            {
                RoleId = role.Id
            };
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    vm.Users.Add(user);
                };
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);
            if (user != null && role != null)
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("EditRole", new { id = role.Id });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(userRoleViewModel);
            }
            ModelState.AddModelError(string.Empty, "用戶或角色未找到");
            return View(userRoleViewModel);
        }
        public async Task<IActionResult> DeleteUserFromRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var vm = new UserRoleViewModel
            {
                RoleId = role.Id
            };
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    vm.Users.Add(user);
                }
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);
            if (user != null && role != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("EditRole", new { id = role.Id });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(userRoleViewModel);
            }
            ModelState.AddModelError(string.Empty, "用戶或角色未找到");
            return View(userRoleViewModel);
        }
    }
}
