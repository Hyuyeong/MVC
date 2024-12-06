using System;
using MVC.Models;

namespace MVC.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
}
