using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Product;

public record ProductRespons(string Name, string Description, decimal Price, string PictureUrl, string Type, string Brand, int QuantityInStock);
