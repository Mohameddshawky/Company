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
        IEnumerable<t> GetAll();
        IEnumerable<t> Search(string name);

        t? Get(int id);
        void Add(t data);
        void Update(t data);
        void Delete(t data);
    }
}
