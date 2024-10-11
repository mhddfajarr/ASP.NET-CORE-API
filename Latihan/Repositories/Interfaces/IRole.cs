using Latihan.Models;

namespace Latihan.Repositories.Interfaces
{
    public interface IRole
    {
        IEnumerable<Role> GetAllRole();
        Role GetRoleById(string roleId);
        int AddRole(Role role);
        int UpdateRole(Role role);
        int DeleteRole(string roleId);
    }
}
