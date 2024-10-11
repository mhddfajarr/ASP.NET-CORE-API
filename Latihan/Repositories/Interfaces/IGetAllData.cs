using Latihan.Models;
using Latihan.ViewModels;
using static Latihan.ViewModels.DataVM;

namespace Latihan.Repositories.Interfaces
{
    public interface IGetAllData
    {
        IEnumerable<Account> GetAllAccount();
        IEnumerable<Employee> GetAllEmployee();
        IEnumerable<Profiling> GetAllProfiling();
        IEnumerable<AccountRole> GetAllAccountRole();
        IEnumerable<DataVM.GetByDegreeVM> GetByDegree();
        Employee GetEmployeeByNIKOrEmail(string nikOrEmail);
        List<string> GetRoles(string nikOrEmail);
        IEnumerable<GetAccountRoleVM> GetAllAccountRoles();
    }
}
