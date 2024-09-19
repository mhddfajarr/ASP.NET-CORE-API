
using Latihan.Models;
namespace Latihan.ViewModels
{
    public class AuthVM
    {
        public class RegisterVM
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string? Phone { get; set; }
            public string Email { get; set; }
            //public string Password { get; set; }
            public DateTime? BirthDate { get; set; }
            public string University_Id { get; set; }
            public float GPA { get; set; }
            public Degree Degree { get; set; } 
        }
        public class LoginVM
        {
            public string NIKOrEmail { get; set; }
            public string password { get; set; }
        }
    }
}
