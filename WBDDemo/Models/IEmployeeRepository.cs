using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WBDDemo.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Gest();
        Employee Get(int id);
        Employee Create(Employee employee);
        Employee Edit(Employee employee);
        bool Delete(int id);
    }
}
