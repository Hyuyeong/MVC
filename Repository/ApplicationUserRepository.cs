using System;
using System.Linq.Expressions;
using MVC.Data;
using MVC.Models;
using MVC.Repository.IRepository;

namespace MVC.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    private readonly ApplicationDbContext _db;

    public ApplicationUserRepository(ApplicationDbContext db)
        : base(db)
    {
        _db = db;
    }
}
