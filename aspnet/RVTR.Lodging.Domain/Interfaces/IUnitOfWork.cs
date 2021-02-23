using System.Threading.Tasks;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Interfaces
{
  public interface IUnitOfWork
  {
    IRepository<LodgingModel> Lodging { get; }
    IRepository<RentalModel> Rental { get; }
    IRepository<ReviewModel> Review { get; }
    IRepository<ImageModel> Image { get; }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<int> CommitAsync();
  }
}
