using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WBDDemo.Models
{
    public class Employee
    {
       
        public int Id { get; set; }
        [Required (ErrorMessage = "phải nhập họ tên ")]
        [MaxLength(20,ErrorMessage = "ten qua dai")]
        public string Fullname { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage = "Email không đúng dạng ")]
        public string Email { get; set; }
        [Required]
        public int  DepartmentId { get; set; }
        public Department  Department { get; set; }
        public string AvatarPath { get; set; }
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }
        
    }
}
