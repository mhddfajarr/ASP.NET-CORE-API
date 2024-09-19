using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Latihan.Models
{
    public class Account
    {
        [Key]
        [ForeignKey("Employee")]
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual Employee? Employee { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public virtual Profiling? Profiling { get; set; }
    }
}
