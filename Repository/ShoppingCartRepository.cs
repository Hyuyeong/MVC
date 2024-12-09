using System;
using System.Linq.Expressions;
using MVC.Data;
using MVC.Models;
using MVC.Repository.IRepository;

namespace MVC.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly ApplicationDbContext _db;

    public ShoppingCartRepository(ApplicationDbContext db)
        : base(db)
    {
        _db = db;
    }

    public void Update(ShoppingCart obj)
    {
        _db.ShoppingCarts.Update(obj);
    }
}
