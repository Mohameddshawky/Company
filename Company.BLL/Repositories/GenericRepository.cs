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
    public class GenericRepository<t> : IGenericRepository<t> where t : BaseEntity
    {
        private readonly CompanyDbContext _dbContext;
        public GenericRepository(CompanyDbContext context)
        {
            _dbContext = context;
        }
        public int Add(t data)
        {
            _dbContext.Set<t>().Add(data);
            return _dbContext.SaveChanges();
        }

        public int Delete(t data)
        {
            _dbContext.Set<t>().Remove(data);
            return _dbContext.SaveChanges();
        }

        public t? Get(int id)
        {
            return _dbContext.Set<t>().Find(id);
        }

        public IEnumerable<t> GetAll()
        {
             return _dbContext.Set<t>().ToList();
        }

        public int Update(t data)
        {
            _dbContext.Set<t>().Update(data);
            return _dbContext.SaveChanges();
        }
    }
}
