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
        public async Task AddAsync(t data)
        {
           await _dbContext.Set<t>().AddAsync(data);
        }

        public void Delete(t data)
        {
            _dbContext.Set<t>().Remove(data);
        }

        public async Task<t?> GetAsync(int id)
        {
            if (typeof(t) == typeof(Employee))
            {
                return await _dbContext.Employees.Include(e => e.Departments).FirstOrDefaultAsync(e=>e.Id==id) as t;
            }
            return _dbContext.Set<t>().Find(id);
        }

        public async Task<IEnumerable<t>> GetAllAsync()
        {
            if (typeof(t) == typeof(Employee)) {
                return await _dbContext.Employees.Include(e=>e.Departments).ToListAsync()as IEnumerable<t>;
            }
             return await _dbContext.Set<t>().ToListAsync();
        }

        public async Task<IEnumerable<t>> SearchAsync(string name)
        {
            if (typeof(t) == typeof(Employee))
            {
                return await _dbContext.Employees.Include(e => e.Departments).Where(x=>x.Name.ToLower().Contains(name.ToLower())).ToListAsync() as IEnumerable<t>;
            }
            else
                return await _dbContext.Departments.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync() as IEnumerable<t>;
        }

        public void Update(t data)
        {
            _dbContext.Set<t>().Update(data);
          
        }
    }
}
