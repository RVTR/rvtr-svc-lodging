using System;
using System.Threading.Tasks;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Context.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ class
  /// </summary>
  public class UnitOfWork : IUnitOfWork
  {
    private readonly LodgingContext _context;

    public IRepository<LodgingModel> Lodging { get; }
    public IRepository<RentalModel> Rental { get; }
    public IRepository<ReviewModel> Review { get; }
    public IRepository<ImageModel> Image { get; }

    public UnitOfWork(LodgingContext context)
    {
      _context = context;

      Lodging = new Repository<LodgingModel>(context);
      Rental = new Repository<RentalModel>(context);
      Review = new Repository<ReviewModel>(context);
      Image = new Repository<ImageModel>(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
  }
}
