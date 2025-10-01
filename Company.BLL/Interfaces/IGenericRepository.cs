using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenericRepository<t>where t :BaseEntity
    {
        Task<IEnumerable<t>> GetAllAsync();
        Task<IEnumerable<t>> SearchAsync(string name);

        Task<t?> GetAsync(int id);
        Task AddAsync(t data);
        void Update(t data);
        void Delete(t data);
    }
}
