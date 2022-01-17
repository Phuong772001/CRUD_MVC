using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WBDDemo.Models
{
    public class SqlEmployeeRepository:IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public SqlEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Employee> Gest()
        {
            return _context.Employees;
        }

        public Employee Get(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Edit(Employee employee)
        {
            var editEmp = _context.Employees.Attach(employee);
            editEmp.State = EntityState.Modified;
            _context.SaveChanges();
            return employee;
        }

        public bool Delete(int id)
        {
            var delEmp = _context.Employees.Find(id);
            if (delEmp==null)
            {
                _context.Employees.Remove(delEmp);
               return _context.SaveChanges() >0;
            }

            return false;
        }
    }
}
