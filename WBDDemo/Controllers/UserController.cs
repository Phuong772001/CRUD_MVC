using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WBDDemo.Models;
using WBDDemo.ViewModels;

namespace WBDDemo.Controllers
{
    [Authorize(Roles = "Admin,A")]
    
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singSignInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> singSignInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _singSignInManager = singSignInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users;
            if (users != null && users.Any())
            {
                var model = new List<UserViewModel>();
                model = users.Select(x => new UserViewModel()
                {
                    UserId = x.Id,
                    Address = x.Address,
                    City = x.City,
                    Email = x.Email
                }).ToList();
                foreach (var user in model)
                {
                    user.RoleName = GetRolesName(user.UserId);
                }
                return View(model);
            }

            return View();
        }

        public string GetRolesName(string userId)
        {
            var user = Task.Run(async () => await _userManager.FindByIdAsync(userId)).Result;
            var roles = Task.Run(async () => await _userManager.GetRolesAsync(user)).Result;
            return roles != null ? string.Join(",", roles) : string.Empty;
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = _roleManager.Roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    City = model.City,
                    Address = model.Address
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.RoleId))
                    {
                        var role = await _roleManager.FindByIdAsync(model.RoleId);
                        var addRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
                        if (addRoleResult.Succeeded)
                        {
                            return RedirectToAction("Index", "User");
                        }

                        foreach (var error in addRoleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }


                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }


            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var model = new EditUserViewModel()
                {
                    Address = user.Address,
                    City = user.City,
                    Email = user.Email,
                    UserId = user.Id

                };
                var rolesName  = await _userManager.GetRolesAsync(user);
                if (rolesName != null && rolesName.Any())
                {
                    var role = await _roleManager.FindByNameAsync(rolesName.FirstOrDefault());
                    model.RoleId = role.Id;
                }
                ViewBag.Roles = _roleManager.Roles;
                return View(model);
            }
            ViewBag.Roles = _roleManager.Roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Id = model.UserId;
                    user.City = model.City;
                    user.Address = model.Address;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var rolesName = await _userManager.GetRolesAsync(user);
                        var delRole = await _userManager.RemoveFromRolesAsync(user,rolesName);
                        if (!string.IsNullOrEmpty(model.RoleId))
                        {
                            var role = await _roleManager.FindByIdAsync(model.RoleId);
                            var addRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
                            if (addRoleResult.Succeeded)
                            {
                                return RedirectToAction("Index", "User");
                            }

                            foreach (var error in addRoleResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                        return RedirectToAction("Index", "User");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user!=null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }
    }
}
