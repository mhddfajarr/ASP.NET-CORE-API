using Latihan.Models;

namespace Latihan.Repositories.Interfaces
{
    public interface IGetAllData
    {
        IEnumerable<Account> GetAllAccount();
        IEnumerable<Employee> GetAllEmployee();
        IEnumerable<Profiling> GetAllProfiling();
        IEnumerable<Education> GetAllEducation();
    }
}
