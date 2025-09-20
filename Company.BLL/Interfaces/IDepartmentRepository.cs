using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Departments> GetAll();
        Departments? Get(int id);   
        int Add(Departments departments);   
        int Update(Departments departments);
        int Delete(Departments departments );

    }
}
