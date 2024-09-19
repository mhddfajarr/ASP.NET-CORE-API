using Latihan.Models;

namespace Latihan.Repositories.Interfaces
{
    public interface IUniversity
    {
        IEnumerable<University> GetAllUniversities();
        University GetUniveristyById(string id);
        int AddUniversity(University university);
        int UpdateUniversity(University university);
        int DeleteUniversity(string id);


    }
}
