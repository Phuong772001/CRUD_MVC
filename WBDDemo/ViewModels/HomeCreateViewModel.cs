using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WBDDemo.Models;

namespace WBDDemo.ViewModels
{
    public class HomeCreateViewModel
    {
        [Required(ErrorMessage = "phải nhập họ tên ")]
        [MaxLength(20, ErrorMessage = "ten qua dai")]
        public string Fullname { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email không đúng dạng ")]
        public string Email { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
