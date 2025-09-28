using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class GenericRepository<t> : IGenericRepository<t> where t : BaseEntity
    {
        private readonly CompanyDbContext _dbContext;
        public GenericRepository(CompanyDbContext context)
        {
            _dbContext = context;
        }
        public void Add(t data)
        {
            _dbContext.Set<t>().Add(data);
        }

        public void Delete(t data)
        {
            _dbContext.Set<t>().Remove(data);
        }

        public t? Get(int id)
        {
            if (typeof(t) == typeof(Employee))
            {
                return _dbContext.Employees.Include(e => e.Departments).FirstOrDefault(e=>e.Id==id) as t;
            }
            return _dbContext.Set<t>().Find(id);
        }

        public IEnumerable<t> GetAll()
        {
            if (typeof(t) == typeof(Employee)) {
                return _dbContext.Employees.Include(e=>e.Departments).ToList()as IEnumerable<t>;
            }
             return _dbContext.Set<t>().ToList();
        }

        public IEnumerable<t> Search(string name)
        {
            if (typeof(t) == typeof(Employee))
            {
                return _dbContext.Employees.Include(e => e.Departments).Where(x=>x.Name.ToLower().Contains(name.ToLower())).ToList() as IEnumerable<t>;
            }
            else
                return _dbContext.Departments.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList() as IEnumerable<t>;
        }

        public void Update(t data)
        {
            _dbContext.Set<t>().Update(data);
          
        }
    }
}
