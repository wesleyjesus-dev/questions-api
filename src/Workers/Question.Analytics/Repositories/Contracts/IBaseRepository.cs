using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question.Analytics.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> Get();
        Task<T> Get(int id);

        Task<T> Create(T book);

        Task Update(int id, T bookIn);

        Task Remove(T bookIn);

        Task Remove(int id);
    }
}
