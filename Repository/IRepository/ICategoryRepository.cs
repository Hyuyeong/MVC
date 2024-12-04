using System;
using MVC.Models;

namespace MVC.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);
}
