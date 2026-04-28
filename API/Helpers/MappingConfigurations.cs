using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.Account;
using Core.Entities;
using Core.ValueObjects;
using Mapster;

namespace API.Helpers;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Address, AddressDto>().TwoWays();
        config.NewConfig<ApplicationUser, UserInfoResponse>().TwoWays();
    }
}
