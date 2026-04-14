namespace Core.Common.Result;

public record Error(string ErrorCode, string ErrorMessage)
{
    public static Error None => new("", "");

}
