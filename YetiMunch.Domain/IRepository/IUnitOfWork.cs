using System;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using YetiMunch.Repository.IRepository;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;
    IAuthRepository auth { get; }
    Task SaveAsync();

    Task<IDbContextTransaction> BeginTransaction();
}
