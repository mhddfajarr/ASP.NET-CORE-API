using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Latihan.Models
{
    public class Employee
    {
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        [JsonIgnore]
        public  virtual Account? Account { get; set; }
    }
}
