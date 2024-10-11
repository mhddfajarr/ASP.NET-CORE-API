using Latihan.Context;
using Latihan.Models;
using Latihan.Repositories.Interfaces;
using Latihan.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Latihan.ViewModels.DataVM;

namespace Latihan.Repositories
{
    public class GetAllDataRepository : IGetAllData

    {
        private readonly MyContext _myContext;
        public GetAllDataRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public IEnumerable<Account> GetAllAccount()
        {
            return _myContext.Accounts.ToList();
        }

        public IEnumerable<AccountRole> GetAllAccountRole()
        {
            return _myContext.AccountRoles.ToList();
        }

        public IEnumerable<GetAccountRoleVM> GetAllAccountRoles()
        {
            var result = from ar in _myContext.AccountRoles
                         join a in _myContext.Accounts on ar.NIK equals a.NIK
                         join e in _myContext.Employees on ar.NIK equals e.NIK 
                         join r in _myContext.Roles on ar.RoleId equals r.RoleId
                         group r by new { a.NIK, e.FirstName, e.LastName } into groupedRoles
                         select new GetAccountRoleVM
                         {
                             NIK = groupedRoles.Key.NIK,
                             AccountName = $"{groupedRoles.Key.FirstName} {groupedRoles.Key.LastName}", 
                             Roles = groupedRoles.ToList() // 
                         };

            return result.ToList();
        }

        public IEnumerable<Education> GetAllEducation()
        {
            return _myContext.Education.ToList();
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _myContext.Employees.ToList();
        }

        public IEnumerable<Profiling> GetAllProfiling()
        {
            return _myContext.Profilings.ToList();
        }


        public IEnumerable<DataVM.GetByDegreeVM> GetByDegree()
        {
            var result = _myContext.Education
                .GroupBy(e => e.Degree)
                .Select(g => new DataVM.GetByDegreeVM
                {
                    degree = g.Key.ToString(), 
                    total = g.Count() 
                })
                .ToList(); 

            return result;
        }

        public Employee GetEmployeeByNIKOrEmail(string nikOrEmail)
        {
            var employee = _myContext.Employees
            .Include(a => a.Account.Profiling.Education.University)
            .FirstOrDefault(e => e.NIK == nikOrEmail || e.Email == nikOrEmail);

            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }

            return employee;
        }

        public List<string> GetRoles(string nikOrEmail)
        {
            var employee = _myContext.Employees
           .Where(e => e.NIK == nikOrEmail || e.Email == nikOrEmail)
           .FirstOrDefault();

            if (employee == null)
            {
                return null;
            }

            var roles = _myContext.AccountRoles
                .Where(ar => ar.NIK == employee.NIK)
                .Select(ar => ar.Role.RoleName)
                .ToList();

            return roles;
        }
    }
}
