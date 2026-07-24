using AquaticFishECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<IEnumerable<Address>> GetUserAddressesAsync(Guid userId);

    }
}
