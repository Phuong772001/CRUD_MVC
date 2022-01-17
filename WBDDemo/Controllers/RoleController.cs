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
    [Authorize(Roles = "Admin")]
    public class RoleController:Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            var model = new List<Role>();
            model = roles.Select(x => new Role()
            {
                RoleId = x.Id,
                RoleName = x.Name
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = model.RoleName,

                });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role =await _roleManager.FindByIdAsync(id);
            if (role!=null)
            {
                var model = new EditRoleViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Edit(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role!=null)
                {
                    role.Name = model.RoleName;
                  var result =  await _roleManager.UpdateAsync(role);
                  if (result.Succeeded)
                  {
                      return RedirectToAction("Index","Role");
                  }

                  foreach (var error in result.Errors)
                  {
                      ModelState.AddModelError("",error.Description);
                  }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var delRole = await _roleManager.FindByIdAsync(id);
            if (delRole != null)
            {
                var result = await _roleManager.UpdateAsync(delRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
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
