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
    public class DepartmentRepository : IDepartmentRepository
    {
        private CompanyDbContext Context;
        public DepartmentRepository()
        {
           Context = new CompanyDbContext();       
        }
        public int Add(Departments departments)
        {
             Context.Departments.Add(departments);
            return Context.SaveChanges();   

        }

        public int Delete(Departments departments)
        {
            Context.Departments.Remove(departments);
            return Context.SaveChanges();
        }

        public Departments? Get(int id)
        {
            return Context.Departments.Find(id);
        }

        public IEnumerable<Departments> GetAll()
        {
            return Context.Departments.ToList();
                 
        }

        public int Update(Departments departments)
        {
            Context.Departments.Update(departments);
            return Context.SaveChanges();
        }
    }
}
