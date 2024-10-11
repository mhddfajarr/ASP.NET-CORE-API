using Latihan.Context;
using Latihan.Models;
using Latihan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Latihan.Repositories
{
    public class RoleRepository : IRole
    {
        private readonly MyContext _myContext;
        public RoleRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public const int fail = 0;

        public int AddRole(Role role)
        {
            var checkId = _myContext.Roles.OrderByDescending(d => d.RoleId).FirstOrDefault();

            if (checkId != null)
            {
                int lastId = int.Parse(checkId.RoleId.Substring(1));
                role.RoleId = "R" + (lastId + 1).ToString("D2");
            }
            else
            {
                role.RoleId = "R01";
            }
            _myContext.Roles.Add(role);
            return _myContext.SaveChanges();
        }

        public int DeleteRole(string roleId)
        {
            var data = _myContext.Roles.Find(roleId);
            if (data != null)
            {
                _myContext.Roles.Remove(data);
                return _myContext.SaveChanges();
            }
            return fail;
        }

        public IEnumerable<Role> GetAllRole()
        {
            return _myContext.Roles.ToList();
        }

        public int UpdateRole(Role role)
        {

            var exists = _myContext.Roles.Any(r => r.RoleId == role.RoleId);

            if (exists)
            {
                _myContext.Entry(role).State = EntityState.Modified;
                return _myContext.SaveChanges();
            }

            return fail;
        }

        public Role GetRoleById(string roleId)
        {
            return _myContext.Roles.Find(roleId);
        }
    }
}
