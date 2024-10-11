using Latihan.Models;

namespace Latihan.ViewModels
{
    public class DataVM
    {
        public class GetAlldataEmpVM
        {
            public string? NIK { get; set; }
            public string? FullName { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public string? BirthDate { get; set; }
            public string? UniversityName { get; set; }
            public float? GPA { get; set; }
            public Degree? degree { get; set; }
        }

        public class GetByDegreeVM
        {
            public string? degree { get; set; }
            public int? total { get; set; }
        }

        public class GetAccountRoleVM
        {
            public string? NIK { get; set; }
            public string? AccountName { get; set; }
            public List<Role>? Roles { get; set; }
        }

        public class ProfileVM
        {
            public string NIK { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string? Phone { get; set; }
            public string Email { get; set; }
            public DateTime? BirthDate { get; set; }
            public string University_Id { get; set; }
            public string? University_Name { get; set; }
            public float GPA { get; set; }
            public Degree Degree { get; set; }
        }

        public class ChangePasswordVM
        {
            public string NIK { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }
    }
    

}
