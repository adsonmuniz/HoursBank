using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursBank.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<bool> FindAsync(int id);
        Task<T> SelectAsync(int id);
        Task<IEnumerable<T>> SelectAsync();
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
    }
}
