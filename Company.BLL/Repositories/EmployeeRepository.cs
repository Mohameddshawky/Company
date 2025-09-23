using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDbContext Context;
        public EmployeeRepository(CompanyDbContext c)
        {
            Context = c;
        }
        public int Add(Employee employee)
        {
            Context.Employees.Add(employee);
            return Context.SaveChanges();

        }

        public int Delete(Employee employee)
        {
            Context.Employees.Remove(employee);
            return Context.SaveChanges();
        }

        public Employee? Get(int id)
        {
            return Context.Employees.Find(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return Context.Employees.ToList();

        }

        public int Update(Employee employee)
        {
            Context.Employees.Update(employee);
            return Context.SaveChanges();
        }
    }
}
