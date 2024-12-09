using System;
using MVC.Models;

namespace MVC.Repository.IRepository;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    void Update(OrderDetail obj);
}
