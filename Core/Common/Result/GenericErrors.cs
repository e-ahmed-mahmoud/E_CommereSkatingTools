namespace Core.Common.Result;

public class GenericErrors()
{
    public static Error OperationError => new("OperationError", "Operation error");
}