using Latihan.Context;
using Latihan.Helper;
using Latihan.Models;
using Latihan.Repositories.Interfaces;
using Latihan.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Latihan.ViewModels.AuthVM;

namespace Latihan.Repositories
{
    public class ProfileRepository : IProfile
    {
        private readonly MyContext _myContext;
        public ProfileRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public const int FAIL = 0;
        public const int SUCCESS = 1;
        public const int INVALID_PASSWORD = -1;

        public int UpdateProfile(DataVM.ProfileVM profile)
        {
            if(profile != null)
            {
                var employee = _myContext.Employees.SingleOrDefault(r => r.NIK == profile.NIK);
                if (employee != null)
                {
                    employee.FirstName = profile.FirstName;
                    employee.LastName = profile.LastName;
                    employee.Phone = profile.Phone;
                    employee.Email = profile.Email;
                    employee.BirthDate = profile.BirthDate;
                    _myContext.Entry(employee).State = EntityState.Modified;
                }
                var profiling = _myContext.Profilings.SingleOrDefault(r => r.NIK == profile.NIK);

                var educationId = profiling.Education_Id;

                var education = _myContext.Education.SingleOrDefault(r => r.Id == educationId);

                if (education != null)
                {
                    education.Degree = profile.Degree;
                    education.GPA = profile.GPA;
                    education.University_Id = profile.University_Id;
                    _myContext.Entry(education).State = EntityState.Modified;
                }
                return _myContext.SaveChanges();

            }
            else
            {
                return FAIL;
            }
        }

        public DataVM.ProfileVM GetProfile(string nik)
        {
            var employee = _myContext.Employees
                .Where(a => a.NIK == nik)
                .Include(a => a.Account.Profiling.Education.University)
                .Select(a => new DataVM.ProfileVM
                {
                    NIK = a.NIK,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email,
                    Phone = a.Phone,
                    BirthDate = a.BirthDate,
                    University_Id = a.Account.Profiling.Education.University.Id,
                    University_Name = a.Account.Profiling.Education.University.Name,
                    GPA = a.Account.Profiling.Education.GPA,
                    Degree = a.Account.Profiling.Education.Degree
                })
                .SingleOrDefault();

            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }

            return employee;
        }

        public int ChangePassword(DataVM.ChangePasswordVM changePasswordVM)
        {
            var data = _myContext.Accounts
            .FirstOrDefault(a => a.NIK == changePasswordVM.NIK);

            if (data == null)
            {
                return FAIL;
            }
            if (!Hashing.ValidatePassword(changePasswordVM.OldPassword, data.Password))
            { 
                return INVALID_PASSWORD;
            }

            data.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
            _myContext.Accounts.Update(data);
            return _myContext.SaveChanges();
        }
    }
}
