using System.Collections.Generic;
using System.Threading.Tasks;

namespace UG.ORM.Base
{
    public interface IRegisteredService
    { 
    }

    public interface ISimpleCRUDService<T> : IRegisteredService where T : class
    {
        Task<IEnumerable<T>>  GetList();
        
        Task<T>  Get(object id);
        
        Task Add(T item);
        
        Task Update(T newItemState);
        
        Task Delete(object id);        
    }
}
