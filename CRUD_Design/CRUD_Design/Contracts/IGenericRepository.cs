using System.Linq.Expressions;
using System.Data;

namespace CRUD_Design_Contracts { 
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(object id);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync (T entity);    
        Task<T> UpdateAsync (T entity); 
        Task DeleteAsync (object id);
        Task<bool> Exists(object id);
        Task<IDbTransaction> BeginTransaction(IsolationLevel isolation = IsolationLevel.Serializable);

        Object? ExecRawSql(string query);
    }
}
