using Latihan.Context;
using Latihan.Models;
using Latihan.Repositories.Interfaces;
using Latihan.ViewModels;
using Latihan.Helper;
using Microsoft.EntityFrameworkCore;

namespace Latihan.Repositories
{
    public class AccountRepository : IAccount
    {
        private readonly MyContext _myContext;
        public AccountRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public const int SUCCESS = 1;
        public const int INVALID_PASSWORD = -1;
        public const int ACCOUNT_NOT_FOUND = 0;

        public IEnumerable<IAccount> GetAllAccount()
        {
            throw new NotImplementedException();
        }

        public bool CheckEmail (string email)
        {
           return _myContext.Employees.Any(e => e.Email == email);

        }

        public string GenerateEducationId()
        {
            var checkId = _myContext.Education
                .OrderByDescending(d => d.Id)
                .FirstOrDefault();

            if (checkId != null)
            {
                int lastId = int.Parse(checkId.Id.Substring(1));
                return "E" + (lastId + 1).ToString("D3");
            }
            else
            {
                return "E001";
            }
        }

        public string GenerateNIK()
        {
            var now = DateTime.Now;
            string yearMonthPrefix = now.ToString("yyyyMM");

            var lastEmployee = _myContext.Employees
                .OrderByDescending(d => d.NIK)
                .FirstOrDefault();

            string newEmployeeNik;
            if (lastEmployee != null)
            {
                string lastId = lastEmployee.NIK.Substring(6);
                int lastNumber = int.Parse(lastId);
                int newNumber = lastNumber + 1;
                newEmployeeNik = yearMonthPrefix + newNumber.ToString("D4");
            }
            else
            {
                newEmployeeNik = yearMonthPrefix + "0001";
            }

            return newEmployeeNik;
        }

        public int Register(AuthVM.RegisterVM registerVM)
        {
            string newEmployeeNik = GenerateNIK();
            var newEmployee = new Employee
            {
                NIK = newEmployeeNik,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Phone = registerVM.Phone,
                Email = registerVM.Email
            };

            _myContext.Employees.Add(newEmployee);

            string educationId = GenerateEducationId();

            var newProfiling = new Profiling
            {
                NIK = newEmployeeNik,
                Education_Id = educationId,
            };
            _myContext.Profilings.Add(newProfiling);

            var newEducation = new Education
            {
                Id = educationId,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                University_Id = registerVM.University_Id
            };

            _myContext.Education.Add(newEducation);

            string hashedPassword = Hashing.HashPassword("12345");
            var newAccount = new Account
            {
                NIK = newEmployeeNik,
                Password = hashedPassword
            };

            _myContext.Accounts.Add(newAccount);
            var save = _myContext.SaveChanges();
            return save;
        }

        public IEnumerable<Data.GetAlldataEmpVM> GetAllEmpData()
        {
            return _myContext.Employees
             .Include(a => a.Account.Profiling.Education.University)
             .Select(a => new Data.GetAlldataEmpVM
             {
                 NIK = a.NIK,
                 FullName = $"{a.FirstName} {a.LastName}",
                 Email = a.Email,
                 Phone = a.Phone,
                 BirthDate = a.BirthDate.HasValue ? a.BirthDate.Value.ToString("d-MMMM-yyyy") : "N/A",
                 UniversityName = a.Account.Profiling.Education.University.Name,
                 GPA = a.Account.Profiling.Education.GPA,
                 degree = a.Account.Profiling.Education.Degree.ToString(),
             })
             .ToList();
        }

        public int Login(AuthVM.LoginVM login)
        {
            // Cek login berdasarkan username di tabel Account
            var data = _myContext.Accounts
                .FirstOrDefault(a => a.NIK == login.NIKOrEmail || a.Employee.Email == login.NIKOrEmail);

            if (data == null)
            {
                return ACCOUNT_NOT_FOUND;
            }
            bool isValid = Hashing.ValidatePassword(login.password, data.Password);
            return isValid ? SUCCESS : INVALID_PASSWORD;
        }
    }
}
