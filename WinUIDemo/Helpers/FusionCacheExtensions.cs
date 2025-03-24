namespace WinUIDemo.Helpers;

public static class FusionCacheExtensions
{
    public static int Duration = 10;
    public static int FailSafeThrottleDuration = 5;
    public static int FailSafeMaxDuration = 60;

    public static FusionCacheEntryOptions GetFusionCacheEntryOptions() =>
        new()
        {
            Duration = TimeSpan.FromSeconds(Duration),
            IsFailSafeEnabled = true,
            FailSafeThrottleDuration = TimeSpan.FromSeconds(FailSafeThrottleDuration),
            FailSafeMaxDuration = TimeSpan.FromSeconds(FailSafeMaxDuration),
        };

    public static FusionCacheOptions GetFusionCacheOptions() =>
        new()
        {
            DefaultEntryOptions = GetFusionCacheEntryOptions()
        };
}
