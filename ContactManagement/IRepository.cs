using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement
{
    public interface IRepository<T>
    {
        void Add(T contact);
        void Update(T contact);
        void Delete(T contact);
        IEnumerable<T> GetAll();
        T GetByName (string name);
    }
}