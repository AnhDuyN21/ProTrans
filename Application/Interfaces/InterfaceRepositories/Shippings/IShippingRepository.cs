using Domain.Entities;

namespace Application.Interfaces.InterfaceRepositories.Shippings
{
	public interface IShippingRepository : IGenericRepository<Shipping>
	{
		Task<List<Shipping>> GetByShipperIdAsync(Guid id);
	}
}
