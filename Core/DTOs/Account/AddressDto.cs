using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Account;

public record AddressDto(string City, string St, string ZipCode, string PhoneNumber);
