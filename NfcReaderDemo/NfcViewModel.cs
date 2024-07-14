namespace NfcReaderDemo.ViewModels;

public sealed partial class NfcViewModel : ObservableRecipient
{
    public MyPcscHelper PcscHelper { get; private set; } = MyPcscHelper.Instance;
}