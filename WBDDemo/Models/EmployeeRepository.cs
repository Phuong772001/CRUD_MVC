//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace WBDDemo.Models
//{
//    public class EmployeeRepository :IEmployeeRepository
//    {
//        private List<Employee> employees;
//        public EmployeeRepository()
//        {
//            employees = new List<Employee>()
//            {
//                new Employee()
//                {
//                    Id = 1,
//                    Fullname = "Nguyễn Văn Phương ",
//                    Department = Dept.IT,
//                    Email = "Phuong@Gmail.com",
//                    AvatarPath = "~/images/Avatar1.png"
                    
//                },
//                new Employee()
//                {
//                    Id = 1,
//                    Fullname = "Kiều Thị Bình  ",
//                    Department = Dept.HR,
//                    Email = "Binh@Gmail.com",
//                    AvatarPath = "~/images/Avatar1.png"

//                },
//            };
//        }
//        public IEnumerable<Employee> Gest()
//        {
//            return employees;
//        }

//        public Employee Get(int id)
//        {
//            return employees.FirstOrDefault(x => x.Id == id);
//        }

//        public Employee Create(Employee employee)
//        {
//            employee.Id = employees.Max(x => x.Id) + 1;
//            employee.AvatarPath = "~/images/Avatar1.png";
//            employees.Add(employee);
//            return employee;
//        }

//        public Employee Edit(Employee employee)
//        {
//            var editEmp = Get(employee.Id);
//            editEmp.Fullname = employee.Fullname;
//            editEmp.Email = employee.Email;
//            editEmp.Department = employee.Department;
//            return editEmp;
//        }

//        public bool Delete(int id)
//        {
//            var delEmp = Get(id);
//            if (delEmp!=null)
//            {
//                employees.Remove(delEmp);
//                return true;
//            }

//            return false;
//        }
//    }
//}
