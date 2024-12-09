using System;
using System.Linq.Expressions;
using MVC.Data;
using MVC.Models;
using MVC.Repository.IRepository;

namespace MVC.Repository;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private readonly ApplicationDbContext _db;

    public OrderDetailRepository(ApplicationDbContext db)
        : base(db)
    {
        _db = db;
    }

    public void Update(OrderDetail obj)
    {
        _db.OrderDetails.Update(obj);
    }
}
