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

        Loaded += this.CameraPreviewSample_Loaded;
        Unloaded += this.CameraPreviewSample_Unloaded;

        semaphoreSlim = new SemaphoreSlim(1);
    }

    private void CameraPreviewSample_Unloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= this.CameraPreviewSample_Unloaded;

        _softwareBitmapSource?.Dispose();
    }

    private void CameraPreviewSample_Loaded(object sender, RoutedEventArgs e)
    {
        Loaded -= this.CameraPreviewSample_Loaded;

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

        if (CameraPreviewControl != null)
        {
            CameraPreviewControl.PreviewFailed += CameraPreviewControl_PreviewFailed!;
            await CameraPreviewControl.StartAsync(cameraHelper!);
            CameraPreviewControl.CameraHelper.FrameArrived += CameraPreviewControl_FrameArrived!;
        }

        _softwareBitmapSource = new SoftwareBitmapSource();
        CurrentFrameImage.Source = _softwareBitmapSource;

        semaphoreSlim.Release();
    }

    //    protected override void OnNavigatedTo(NavigationEventArgs e)
    //    {
    //        base.OnNavigatedTo(e);
    //#if !WINAPPSDK
    //        Application.Current.Suspending += Application_Suspending;
    //        Application.Current.Resuming += Application_Resuming;
    //#endif
    //    }

    //    protected async override void OnNavigatedFrom(NavigationEventArgs e)
    //    {
    //        base.OnNavigatedFrom(e);
    //#if !WINAPPSDK
    //        Application.Current.Suspending -= Application_Suspending;
    //        Application.Current.Resuming -= Application_Resuming;
    //#endif
    //        await CleanUpAsync();
    //    }

    //    private async void Application_Suspending(object sender, SuspendingEventArgs e)
    //    {
    //        if (Frame?.CurrentSourcePageType == typeof(CameraPreviewSample))
    //        {
    //            var deferral = e.SuspendingOperation.GetDeferral();
    //            await CleanUpAsync();
    //            deferral.Complete();
    //        }
    //    }

    //    private async void Application_Resuming(object sender, object e)
    //    {
    //        if (CameraPreviewControl != null)
    //        {
    //            var cameraHelper = CameraPreviewControl.CameraHelper;
    //            CameraPreviewControl.PreviewFailed += CameraPreviewControl_PreviewFailed!;
    //            await CameraPreviewControl.StartAsync(cameraHelper);
    //            CameraPreviewControl.CameraHelper.FrameArrived += CameraPreviewControl_FrameArrived!;
    //        }
    //    }

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

        if (softwareBitmap != null)
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
        if (CameraPreviewControl.CameraHelper != null)
        {
            CameraPreviewControl.CameraHelper.FrameArrived -= CameraPreviewControl_FrameArrived!;
        }

        CameraPreviewControl.PreviewFailed -= CameraPreviewControl_PreviewFailed!;
    }

    //private async Task CleanUpAsync()
    //{
    //    UnsubscribeFromEvents();

    //    CameraPreviewControl.Stop();
    //    await CameraPreviewControl.CameraHelper.CleanUpAsync();
    //}
}
