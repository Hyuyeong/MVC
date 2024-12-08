using System;
using MVC.Models;

namespace MVC.Repository.IRepository;

public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company obj);
}
