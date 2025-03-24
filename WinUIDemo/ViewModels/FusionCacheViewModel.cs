namespace WinUIDemo.ViewModels;

public sealed partial class FusionCacheViewModel : ObservableRecipient
{
    private readonly IFusionCache _fusionCacheDependency;
    private readonly IFusionCache _fusionCacheDirect;
    private readonly Random _random;
    private Stopwatch _stopwatchTimer
    {
        get; set;
    }

    [ObservableProperty]
    public partial string Elapsed
    {
        get; set;
    }
    [ObservableProperty]
    public partial int TestIntDependency
    {
        get; set;
    }
    [ObservableProperty]
    public partial int TestIntDirect
    {
        get; set;
    }
    [ObservableProperty]
    public partial string TestStringDependency { get; set; } = default!;
    [ObservableProperty]
    public partial string TestStringDirect { get; set; } = default!;
    [ObservableProperty]
    public partial Person TestPersonDependency { get; set; } = default!;
    [ObservableProperty]
    public partial Person TestPersonDirect { get; set; } = default!;

    public ICommand GetCommand
    {
        get;
    }
    public ICommand ResetCommand
    {
        get;
    }

    public FusionCacheViewModel(IFusionCache fusionCache)
    {
        _fusionCacheDependency = fusionCache;
        _fusionCacheDirect = new FusionCache(FusionCacheExtensions.GetFusionCacheOptions());
        _random = new Random();
        _stopwatchTimer = Stopwatch.StartNew();
        Elapsed = string.Empty;

        PopulateData();
        GetCommand = new AsyncRelayCommand(GetAsync);
        ResetCommand = new AsyncRelayCommand(ResetAsync);
    }

    ~FusionCacheViewModel()
    {
        _stopwatchTimer.Stop();
    }

    public async Task LoadAsync()
    {
        while (_stopwatchTimer.IsRunning)
        {
            Elapsed = _stopwatchTimer.Elapsed.ToString();
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
        await Task.CompletedTask;
    }

    private void PopulateData()
    {
        TestIntDependency = _fusionCacheDependency.GetOrSet(nameof(TestIntDependency), _random.Next());
        TestStringDependency = _fusionCacheDependency.GetOrSet(nameof(TestStringDependency), $"String {_random.Next()}");
        TestPersonDependency = _fusionCacheDependency.GetOrSet(nameof(TestPersonDependency), _ => new Person { Name = $"Name {_random.Next()}", BirthDay = new DateOnly(2000, 01, 01) });

        TestIntDirect = _fusionCacheDirect.GetOrSet(nameof(TestIntDirect), _random.Next());
        TestStringDirect = _fusionCacheDirect.GetOrSet(nameof(TestStringDirect), $"String {_random.Next()}");
        TestPersonDirect = _fusionCacheDirect.GetOrSet(nameof(TestPersonDirect), _ => new Person { Name = $"Name {_random.Next()}", BirthDay = new DateOnly(2022, 02, 02) });
    }

    public async Task GetAsync()
    {
        _ = TestIntDependency;
        _ = TestStringDependency;
        _ = TestPersonDependency;
        _ = TestIntDirect;
        _ = TestStringDirect;
        _ = TestPersonDirect;
        await Task.CompletedTask;
    }

    public async Task ResetAsync()
    {
        PopulateData();
        _stopwatchTimer.Restart();
        await Task.CompletedTask;
    }
}
