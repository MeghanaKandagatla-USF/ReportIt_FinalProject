using ReportIt.Models;

namespace ReportIt.Data
{
    public interface ICrimeEventRepository
    {
        List<CrimeEvent> GetAll();
        CrimeEvent? GetById(int id);
        CrimeEvent Create(CrimeEvent crimeEvent);
        CrimeEvent Update(CrimeEvent crimeEvent);
        CrimeEvent DeleteById(int id);
        List<CrimeEvent> Search(string queryString);
    }
}
