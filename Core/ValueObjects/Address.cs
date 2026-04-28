using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ValueObjects;

public record Address(string City, string St, string ZipCode, string PhoneNumber);


