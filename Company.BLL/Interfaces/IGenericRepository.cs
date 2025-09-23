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
        t? Get(int id);
        int Add(t data);
        int Update(t data);
        int Delete(t data);
    }
}
