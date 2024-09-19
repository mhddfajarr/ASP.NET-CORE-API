using Latihan.Helper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Latihan.Models
{
    public class Education
    {
        public string Id { get; set; }
        public Degree Degree { get; set; }
        public float GPA { get; set; }
        [JsonIgnore]
        public virtual University? University { get; set; }
        [ForeignKey("University")]
        public string University_Id { get; set; }
    }
    public enum Degree
    {
        D3,
        D4,
        S1,
        S2,
        S3
    }
}
