using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WBDDemo.Models;
using WBDDemo.ViewModels;

namespace WBDDemo.Controllers
{
    //[Authorize]
    public class HomeController:Controller
    {
        private IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeController(IEmployeeRepository _employeeRepository,IWebHostEnvironment _webHostEnvironment)
        {
           employeeRepository = _employeeRepository;
           webHostEnvironment = _webHostEnvironment;
        }
        //[Route("~/")]
        //[Route("Home")]
        //[Route("Home/Index")]
        [AllowAnonymous]
        public ViewResult Index()
        {
           //Dung: ViewData
        //   ViewData["employees"] = employeeRepository.Gest();
            //Dung: ViewBag
           // ViewBag.Employess = employeeRepository.Gest();
           var employees= employeeRepository.Gest();
            return View(employees);
        }

        public ViewResult Details(int? id)
        {
            try
            {
                int.Parse(id.Value.ToString());
                var employee = employeeRepository.Get(id.Value);
                if (employee == null)
                {
                    //ViewBag.Id = id.Value;
                    return View("~/Views/Error/EmployeeNotFound.cshtml", id.Value);
                }
                //ViewBag.Employee = employeeRepository.Get(id);
                //var employee = employeeRepository.Get(id);
                //ViewBag.TitleName = "Employee Details";
                var detailViewModel = new HomeDetailViewModel()
                {
                    Employee = employeeRepository.Get(id ?? 1),
                    TitleName = "Employee Details"

                };
                return View(detailViewModel);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
        //[Authorize]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        //[Authorize]
        [HttpPost]
        public IActionResult Create(HomeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Fullname = model.Fullname,
                    Email = model.Email,
                    DepartmentId = model.DepartmentId
                };
                var fileName = string.Empty;
                if (model.Avatar!=null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = model.Avatar.FileName;
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs =new FileStream(filePath,FileMode.Create))
                    {
                        model.Avatar.CopyTo(fs);
                    }
                }

                employee.AvatarPath = fileName;
                var newEmp = employeeRepository.Create(employee);
                return RedirectToAction("Details", new { id = newEmp.Id });
            }

            return View();
        }
        //[Authorize]
        public ViewResult Edit(int id)
        {
            var employees = employeeRepository.Get(id);
            if (employees == null)
            {
                //ViewBag.Id = id.Value;
                return View("~/Views/Error/EmployeeNotFound.cshtml", id);
            }
            var employee = employeeRepository.Get(id);
            return View(employee);
        }
        //[Authorize]
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            var employee = employeeRepository.Edit(model);
            if (employeeRepository.Edit(model)!=null)
            {
                return RedirectToAction("Details",new{id=employee.Id});
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            if (employeeRepository.Delete(id))
            {
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
