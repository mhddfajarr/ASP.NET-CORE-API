using Latihan.Models;
using Latihan.ViewModels;

namespace Latihan.Repositories.Interfaces
{
    public interface IAccount
    {
        IEnumerable<IAccount> GetAllAccount();
        IEnumerable<DataVM.GetAlldataEmpVM> GetAllEmpData();
       
        int Register(AuthVM.RegisterVM registerVM);

        int Login(AuthVM.LoginVM login);

    }
}
