namespace Catalog.Host.Models.Response;

public class BaseResponse<T>
{
    public T Id { get; set; } = default(T) !;
}