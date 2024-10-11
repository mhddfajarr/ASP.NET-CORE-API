using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Latihan.Models
{
    public class AccountRole
    {
        public int AccountRoleId { get; set; }
        public string NIK { get; set; }
        [ForeignKey("NIK")]
        public virtual Account Account { get; set; }
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
