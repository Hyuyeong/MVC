using System;

namespace MVC.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }

    void Save();
}
