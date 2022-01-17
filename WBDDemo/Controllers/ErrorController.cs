using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WBDDemo.Controllers
{
    public class ErrorController:Controller
    {
        [Route("Error/{StatusCode}")]
        public ViewResult PageNotFound(int StatusCode)
        {
            ViewBag.ErrorMessage = $"Error{StatusCode}:truyen sai tham so id ";
            return View();
        }
        [Route("Error")]
        public ViewResult Error()
        {
            return View("Exception");
        }
    }
}
