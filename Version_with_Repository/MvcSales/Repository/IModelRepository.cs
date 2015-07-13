using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSales.Repository
{
    public interface IModelRepository<T>:IDisposable
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        IEnumerable<T> Items { get; }
        void SaveChanges();
    }
}
