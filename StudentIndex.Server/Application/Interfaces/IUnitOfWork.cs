namespace StudentIndex.Server.Application.Interfaces
{
    /// <summary>
    /// Omogućava servisima da izvrše više upisa atomarno (u jednoj transakciji),
    /// bez direktne zavisnosti od EF Core-a.
    /// </summary>
    public interface IUnitOfWork
    {
        Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);
    }
}
