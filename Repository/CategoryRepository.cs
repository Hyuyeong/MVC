using System;
using System.Linq.Expressions;
using MVC.Data;
using MVC.Models;
using MVC.Repository.IRepository;

namespace MVC.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
        : base(db)
    {
        _db = db;
    }

    public void Update(Category obj)
    {
        _db.Categories.Update(obj);
    }
}
