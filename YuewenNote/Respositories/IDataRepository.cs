using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace YuewenNote.Respositories
{
    public interface IDataRepository<T>
    {
        Task Add(T t);
        Task<ObservableCollection<T>> Load();

        Task<T> GetById(T t);

        Task Remove(T t);
        Task Update(T t);
    }
}
