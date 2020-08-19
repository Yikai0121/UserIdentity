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
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userCreateViewModel);
            }
            var user = _mapper.Map<ApplicationUser>(userCreateViewModel);
            var result = await _userManager.CreateAsync(user, userCreateViewModel.Password);
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
                return View(userCreateViewModel);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "刪除錯誤");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "沒有此用戶");
            }
            return View("Index", await _userManager.Users.ToListAsync());
        }
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(string id, UserEditViewModel userEditViewModel)
        {
            var user =await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
             _mapper.Map(userEditViewModel, user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "編輯錯誤");
            }
            return View("Index", await _userManager.Users.ToListAsync());

        }
    }
}
