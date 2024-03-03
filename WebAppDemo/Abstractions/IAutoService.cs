namespace WebAppDemo.Abstractions;

public interface IAutoService
{
    IEnumerable<AutoModel> GetAll();
}
