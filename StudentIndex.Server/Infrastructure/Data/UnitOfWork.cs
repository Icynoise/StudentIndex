using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Application.Interfaces;

namespace StudentIndex.Server.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly StudentAplikacijaContext _context;

    public UnitOfWork(StudentAplikacijaContext context)
    {
        _context = context;
    }

    public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            await action();
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
