using EliteTest.Application.Interfaces;
using EliteTest.Domain.Common;
using EliteTest.Infrastructure.Persistence;
using EliteTest.Infrastructure.Repositories;

namespace EliteTest.Infrastructure;
public class UnitOfwork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private readonly Dictionary<string, object> _repositories = new();
    public UnitOfwork(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        var type = typeof(T).Name;

        if (_repositories.ContainsKey(type))
            return (IGenericRepository<T>)_repositories[type];

        var repository = new GenericRepository<T>(_context);
        _repositories.Add(type, repository);

        return repository;
    }
}