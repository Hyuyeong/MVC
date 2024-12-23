using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Repository.IRepository;

namespace MVC.Repository;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = db.Set<T>();

        _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Get(
        Expression<Func<T, bool>> filter,
        string? includeProperties = null,
        bool tracked = false
    )
    {
        if (tracked)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (
                    var property in includeProperties.Split(
                        new char[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries
                    )
                )
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }
        else
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (
                    var property in includeProperties.Split(
                        new char[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries
                    )
                )
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }
    }

    public IEnumerable<T> GetAll(
        Expression<Func<T, bool>>? filter,
        string? includeProperties = null
    )
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (
                var property in includeProperties.Split(
                    new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries
                )
            )
            {
                query = query.Include(property);
            }
        }
        return query.ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}
