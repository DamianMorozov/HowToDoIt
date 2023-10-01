namespace WebAppDemo.Services;

public class UidService
{
    protected internal IGuidService Guid { get; }

    public UidService(IGuidService guidService)
    {
        Guid = guidService;
    }
}