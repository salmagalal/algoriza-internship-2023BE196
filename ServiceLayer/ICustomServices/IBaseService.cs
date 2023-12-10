using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(string Id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
