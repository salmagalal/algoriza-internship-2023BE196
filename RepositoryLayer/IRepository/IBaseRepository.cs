using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IBaseRepository <T> where T : class
    {
        T GetById(string id);

        T GetById(int id);

        IEnumerable<T> GetAll();
  
        T Add(T entity);
    
        T Update(T entity);

        void Delete(T entity);

        void Save();

        Task SaveAsync();
    }
}
