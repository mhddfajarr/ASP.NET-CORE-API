using Latihan.Models;
using Latihan.ViewModels;

namespace Latihan.Repositories.Interfaces
{
    public interface IProfile
    {
        int UpdateProfile(DataVM.ProfileVM registerVM);
        int ChangePassword(DataVM.ChangePasswordVM changePasswordVM);
        DataVM.ProfileVM GetProfile(string nik);
    }
}
