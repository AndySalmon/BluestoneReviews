using bluestone.guests.data.Repositories.Interfaces;

namespace bluestone.guests.data.Repositories
  {

  /// <summary>
  /// Abstracts access to the enclosed repositories allowing for coordinated change tracking and saving.
  /// </summary>
  /// 
  public class UnitOfWork : IUnitOfWork
    {
    private readonly GuestReviewsDbContext _context;
    private GuestRepository _guestRepository;
    private ReviewRepository _reviewRepository;

    public UnitOfWork()
      {
      }

    public UnitOfWork(GuestReviewsDbContext context)
      {
      this._context = context;
      }

    public IReviewRepository Reviews => _reviewRepository = _reviewRepository ?? new ReviewRepository(_context);

    public IGuestRepository Guests => _guestRepository = _guestRepository ?? new GuestRepository(_context);



    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
      {
      return await _context.SaveChangesAsync(cancellationToken);
      }




    public void Dispose()
      {
      _context.Dispose();
      }
    }
  }
