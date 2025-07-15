using EliteTest.Domain.Common;

namespace EliteTest.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
}
