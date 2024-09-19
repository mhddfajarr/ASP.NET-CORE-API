using Latihan.Context;
using Latihan.Models;
using Latihan.Repositories.Interfaces;

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
    }
}
