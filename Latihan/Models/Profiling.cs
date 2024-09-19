using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Latihan.Models
{
    public class Profiling
    {
        [Key]
        [ForeignKey("Account")]
        public string NIK { get; set; }
        [JsonIgnore]
        
        public virtual Account? Account { get; set; }
        [JsonIgnore]
        public virtual Education? Education { get; set; }

        [ForeignKey("Education")]
        public string Education_Id { get; set; }
    }
}
