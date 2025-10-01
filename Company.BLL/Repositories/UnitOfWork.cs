using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly CompanyDbContext _companyDbContext;

        //apply lazy to create instanse from object when need it

        public UnitOfWork(
            
            CompanyDbContext companyDbContext
            )
        {
            _companyDbContext = companyDbContext;
            _employeeRepository = new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(companyDbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(()=>new DepartmentRepository(companyDbContext));
           
        }

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public async Task<int> SaveChangesAsync()
        {
            return await _companyDbContext.SaveChangesAsync();                             
        }
    }
}
