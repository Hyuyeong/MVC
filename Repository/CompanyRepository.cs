using System;
using MVC.Data;
using MVC.Models;
using MVC.Repository.IRepository;

namespace MVC.Repository;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private readonly ApplicationDbContext _db;

    public CompanyRepository(ApplicationDbContext db)
        : base(db)
    {
        _db = db;
    }

    public void Update(Company obj)
    {
        _db.companies.Update(obj);
    }
}
