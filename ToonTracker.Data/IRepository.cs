// Author: Echipa ToonTracker
// Functionalitate: Contract generic pentru persistenta datelor.
namespace ToonTracker.Data;

public interface IRepository<T>
{
    IReadOnlyList<T> GetAll();
    void SaveAll(IEnumerable<T> items);
}
