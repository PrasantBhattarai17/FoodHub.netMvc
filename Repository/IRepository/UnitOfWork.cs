using System;
using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Repository.Implementation;
using YetiMunch.Repository.IRepository;


public class UnitOfWork : IUnitOfWork
{

    private readonly FoodContext _db;
    private readonly Dictionary<Type, object> _repositories = new();
    public IAuthRepository auth { get; private set; }
    public UnitOfWork(FoodContext db)
    {
        _db = db;
        auth = new AuthRepository(_db);
    }
    public IRepository<T> Repository<T>() where T : class
    {
        if (!_repositories.ContainsKey(typeof(T)))
        {
            _repositories[typeof(T)] = new Repository<T>(_db);
        }
        return (IRepository<T>)_repositories[typeof(T)];
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}
