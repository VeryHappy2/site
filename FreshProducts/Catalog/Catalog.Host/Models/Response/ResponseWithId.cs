namespace Catalog.Host.Models.Response;

public class ResponseWithId<T>
{
    public int Id { get; set; }
    public T Data { get; set; }
}