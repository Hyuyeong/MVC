using System;
using System.Linq.Expressions;

namespace MVC.Repository.IRepository;

public interface IRepository<T>
    where T : class
{
    IEnumerable<T> GetAll(string? includeProperties = null);
    T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);
}
