namespace bluestone.guests.data.Repositories.Interfaces
  {
  public interface IUnitOfWork : IDisposable
    {
    IGuestRepository Guests { get; }
    IReviewRepository Reviews { get; }



    /// <summary>
    ///     Saves all changes made in this UnitOfWork to the database.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method is basically a wrapper around DbContext.SaveChangesAsync
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous save operation. The task result contains the
    ///     number of state entries written to the database.
    /// </returns>
    /// <exception cref="DbUpdateException">
    ///     An error is encountered while saving to the database.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    ///     A concurrency violation is encountered while saving to the database.
    ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
    ///     This is usually because the data in the database has been modified since it was loaded into memory.
    /// </exception>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
  }
