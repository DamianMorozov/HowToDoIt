namespace NfcReaderDemo.NfcDevice;

public class MyNotifyBase : ObservableRecipient, INotifyPropertyChanged
{
    #region INotifyPropertyChanged

    public async void OnPropertyChangedUWPAsync([CallerMemberName] string memberName = "")
    {
        await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            OnPropertyChanged(new PropertyChangedEventArgs(memberName));
        });
    }

    public void OnPropertyChangedWinUI([CallerMemberName] string memberName = "")
    {
        var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        if (dispatcherQueue is null) return;
        // This calls comes off-thread, hence we will need to dispatch it to current app's thread
        dispatcherQueue.TryEnqueue(() =>
        {
            OnPropertyChanged(new PropertyChangedEventArgs(memberName));
        });
    }

    #endregion
}
