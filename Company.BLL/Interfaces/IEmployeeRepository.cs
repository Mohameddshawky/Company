using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? Get(int id);
        int Add(Employee departments);
        int Update(Employee departments);
        int Delete(Employee departments);
    }
}
