using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address> , IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context) { }

    }
}
