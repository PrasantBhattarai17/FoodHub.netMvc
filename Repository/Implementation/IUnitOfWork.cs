using System;
using YetiMunch.Repository.IRepository;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;
    IAuthRepository auth { get; }
    Task SaveAsync();
}
