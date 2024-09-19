using Latihan.Context;
using Latihan.Models;
using Latihan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Latihan.Repositories
{
    public class UniversityRepository : IUniversity
    {
        private readonly MyContext _myContext;

        public const int fail = 0;

        public UniversityRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public int AddUniversity(University university)
        {
            var checkId = _myContext.Universities.OrderByDescending(d => d.Id).FirstOrDefault();
            if (checkId != null)
            {
                int lastId = int.Parse(checkId.Id.Substring(1));
                university.Id = "U" + (lastId + 1).ToString("D3");
            }
            else
            {
                university.Id = "U001";
            }
            _myContext.Universities.Add(university);
            return _myContext.SaveChanges();

        }

        public int DeleteUniversity(string id)
        {
            var data = _myContext.Universities.Find(id);
            if (data != null)
            {
                _myContext.Universities.Remove(data);
                return _myContext.SaveChanges();
            }
            return fail;
        }


        public IEnumerable<University> GetAllUniversities()
        {
            return _myContext.Universities.ToList();
        }


        public University GetUniveristyById(string id)
        {
            return _myContext.Universities.Find(id);
        }


        public int UpdateUniversity(University university)
        {
            var Check = GetUniveristyById(university.Id);

            if (Check != null)
            {
                _myContext.Entry(Check).State = EntityState.Detached;
                _myContext.Entry(university).State = EntityState.Modified;
                return _myContext.SaveChanges();
            }

            return fail;
        }

    }
}
