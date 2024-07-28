namespace WinUIDemo.Views;

public sealed partial class CameraPreviewPage : Page
{
    private static SemaphoreSlim? semaphoreSlim;
    private VideoFrame _currentVideoFrame = default!;
    private SoftwareBitmapSource _softwareBitmapSource = default!;
    public bool ShowCamera { get; set; }

    public CameraPreviewViewModel ViewModel { get; }

    public CameraPreviewPage()
    {
        ViewModel = App.GetService<CameraPreviewViewModel>();
        InitializeComponent();

        Loaded += CameraPreviewSample_Loaded;
        Unloaded += CameraPreviewSample_Unloaded;

        semaphoreSlim = new SemaphoreSlim(1);
    }

    private void CameraPreviewSample_Unloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= CameraPreviewSample_Unloaded;

        _softwareBitmapSource?.Dispose();
    }

    private void CameraPreviewSample_Loaded(object sender, RoutedEventArgs e)
    {
        Loaded -= CameraPreviewSample_Loaded;
        Load();
    }

    private async void Load()
    {
        // Using a semaphore lock for synchronization.
        // This method gets called multiple times when accessing the page from Latest Pages
        // and creates unused duplicate references to Camera and memory leaks.
        await semaphoreSlim!.WaitAsync();

        var cameraHelper = CameraPreviewControl?.CameraHelper;
        UnsubscribeFromEvents();

        if (CameraPreviewControl is not null)
        {
            CameraPreviewControl.PreviewFailed += CameraPreviewControl_PreviewFailed!;
            await CameraPreviewControl.StartAsync(cameraHelper!);
            CameraPreviewControl.CameraHelper.FrameArrived += CameraPreviewControl_FrameArrived!;
        }

        _softwareBitmapSource = new SoftwareBitmapSource();
        CurrentFrameImage.Source = _softwareBitmapSource;

        semaphoreSlim.Release();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        //Window.Current.Activated += Current_Activated;
        //Window.Current.Deactivated += Current_Deactivated;
    }

    protected async override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        //Window.Current.Activated -= Window_Activated;
        //Application.Deactivated -= Window_Deactivated;
        await CleanUpAsync();
    }

    private async void Current_Activated(object sender, WindowActivatedEventArgs args)
    {
        if (CameraPreviewControl is not null)
        {
            var cameraHelper = CameraPreviewControl.CameraHelper;
            CameraPreviewControl.PreviewFailed += CameraPreviewControl_PreviewFailed!;
            await CameraPreviewControl.StartAsync(cameraHelper);
            CameraPreviewControl.CameraHelper.FrameArrived += CameraPreviewControl_FrameArrived!;
        }
    }

    private async void Current_Deactivated(object sender, WindowEventArgs args)
    {
        if (Frame?.CurrentSourcePageType == typeof(CameraPreviewPage))
        {
            //var deferral = args.SuspendingOperation.GetDeferral();
            await CleanUpAsync();
            //deferral.Complete();
        }
    }

    private void CameraPreviewControl_FrameArrived(object sender, FrameEventArgs e)
    {
        _currentVideoFrame = e.VideoFrame;
    }

    private void CameraPreviewControl_PreviewFailed(object sender, PreviewFailedEventArgs e)
    {
        ErrorBar.Message = e.Error;
        ErrorBar.IsOpen = true;
    }


    private async void CaptureButton_Click(object sender, RoutedEventArgs e)
    {
        var softwareBitmap = _currentVideoFrame?.SoftwareBitmap;

        if (softwareBitmap is not null)
        {
            if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 || softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
            {
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }

            await _softwareBitmapSource!.SetBitmapAsync(softwareBitmap);
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (CameraPreviewControl.CameraHelper is not null)
        {
            CameraPreviewControl.CameraHelper.FrameArrived -= CameraPreviewControl_FrameArrived!;
        }

        CameraPreviewControl.PreviewFailed -= CameraPreviewControl_PreviewFailed!;
    }

    private async Task CleanUpAsync()
    {
        UnsubscribeFromEvents();

        CameraPreviewControl.Stop();
        await CameraPreviewControl.CameraHelper.CleanUpAsync();
    }
}
