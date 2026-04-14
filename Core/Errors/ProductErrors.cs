using Core.Common.Result;

namespace Core.Errors;

public class ProductErrors
{
    public static Error NotDefinedProduct => new("NotDefined", "Not Defined Product");
}
